using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Lab2.BL.Core;

namespace Lab2.BL.Services
{
    // ReSharper disable once InconsistentNaming
    public class FileStorageIO<T> : IStorageIO<T>
        where T : new()
    {
        private const char Splitter = '|';
        private static readonly PropertyInfo[] Properties = typeof(T).GetProperties();

        private readonly string _filePath;


        public FileStorageIO(string filePath)
        {
            _filePath = filePath;
        }


        public IEnumerable<T> ReadAll()
        {
            if (!File.Exists(_filePath))
            {
                return Enumerable.Empty<T>();
            }

            var data = File.ReadAllBytes(_filePath);
            var dataString = Encoding.UTF8.GetString(data);
            var lines = dataString.Split(Splitter);
            var result = lines
                .Partition(Properties.Length)
                .Select(p => DeserializePartition(p.ToArray()));

            return result;
        }

        public void WriteAll(IEnumerable<T> source)
        {
            var dataStrings = source
                .SelectMany(SerializeToPartition);
            var completeDataString = String.Join(Splitter.ToString(), dataStrings);
            var dataBytes = Encoding.UTF8.GetBytes(completeDataString);
            File.WriteAllBytes(_filePath, dataBytes);
        }


        private static T DeserializePartition(IList<string> properties)
        {
            var creatingObject = new T();
            var countUntil = Math.Min(Properties.Length, properties.Count);
            for (var i = 0; i < countUntil; i++)
            {
                var value = Convert.ChangeType(properties[i], Properties[i].PropertyType);
                Properties[i].SetValue(creatingObject, value);
            }

            return creatingObject;
        }

        private static IEnumerable<string> SerializeToPartition(T target)
        {
            var result = Properties
                .Select(p => p.GetValue(target).ToString());

            return result;
        }
    }
}