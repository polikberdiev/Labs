using System;
using System.Linq;
using Lab2.BL.Models;
using Lab2.BL.Services;

namespace Lab2
{
    internal class Program
    {
        private const string StudentFilePath = "students.dat";


        public static int Main(string[] args)
        {
            var studentStorage = new Storage<StudentModel>(new FileStorageIO<StudentModel>(StudentFilePath));

            while (true)
            {
                Console.WriteLine("Please, choose the action: ");
                Console.WriteLine(" F1 - add new student");
                Console.WriteLine(" F2 - show students");
                Console.WriteLine(" F3 - save");
                Console.WriteLine("Esc - exit the program");

                var key = Console.ReadKey();
                Console.WriteLine();

                try
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.F1:
                            AddStudent(studentStorage);
                            break;
                        case ConsoleKey.F2:
                            ShowStudents(studentStorage);
                            break;
                        case ConsoleKey.F3:
                            Save(studentStorage);
                            break;
                        case ConsoleKey.Escape:
                            return 0;
                        default:
                            throw new NotSupportedException("No action bound to this key.");
                    }
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            }
        }


        private static void AddStudent(IStorage<StudentModel> studentStorage)
        {
            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();
            if (String.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("The person should have first name.");
            }

            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();
            if (String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("The person should have last name.");
            }

            Console.Write("Enter test title: ");
            var testTitle = Console.ReadLine();
            if (String.IsNullOrEmpty(testTitle))
            {
                throw new ArgumentException("The test title should be provided.");
            }

            Console.Write("Enter rating: ");
            var ratingString = Console.ReadLine();
            if (!Int32.TryParse(ratingString, out int ratingNum))
            {
                throw new ArgumentException("The rating should be a number.");
            }
            if (ratingNum < 0 || ratingNum > 10)
            {
                throw new ArgumentException("The rating should be in range 0-10.");
            }

            var student = new StudentModel
            {
                FirstName = firstName,
                LastName = lastName,
                TestTitle = testTitle,
                Rating = ratingNum,
                Timestamp = DateTime.UtcNow
            };
            studentStorage.Add(student);
            studentStorage.Save();
        }

        private static void ShowStudents(IStorage<StudentModel> studentStorage)
        {
            Console.WriteLine("Select property to sort:");
            var properties = typeof(StudentModel).GetProperties().Select(p => p.Name).ToList();
            var sortPropertiesShowData = properties
                .Select((p, i) => $"{i + 1}. {p} asc")
                .Concat(properties
                    .Select((p, i) => $"{i + properties.Count + 1}. {p} desc"));
            foreach (var showData in sortPropertiesShowData)
            {
                Console.WriteLine(showData);
            }
            var sortString = Console.ReadLine();
            if (!Int32.TryParse(sortString, out int sortNum))
            {
                throw new ArgumentException("Should be a number.");
            }
            if (sortNum <= 0 || sortNum > properties.Count * 2)
            {
                throw new ArgumentException("Wrong type selected.");
            }

            Console.Write("Enter amount of showing elements: ");
            var maxCountString = Console.ReadLine();
            if (!Int32.TryParse(maxCountString, out int maxCount))
            {
                throw new ArgumentException("Should be a number.");
            }
            if (maxCount <= 0)
            {
                throw new ArgumentException("Amount should be more than 0.");
            }

            var propertyNum = sortNum % properties.Count;
            var asc = propertyNum == sortNum;
            var propertyName = properties[propertyNum];

            var data = studentStorage.GetSortedBy(propertyName, asc, maxCount).ToList();
            if (data.Count == 0)
            {
                throw new Exception("Not found.");
            }

            const int firstNameCellSize = 22;
            const int lastNameCellSize = 22;
            const int testTitleCellSize = 22;
            const int ratingCellSize = 8;
            const int timestampCellSize = 24;
            const string cellDivider = "|";
            var rowDivider = $"+{new string('-', firstNameCellSize)}+{new string('-', lastNameCellSize)}+{new string('-', testTitleCellSize)}+{new string('-', ratingCellSize)}+{new string('-', timestampCellSize)}+";

            void ConsoleWriteAndSkip(string message, int totalSymbols)
            {
                Console.Write($"{message}{new string(' ', totalSymbols - message.Length)}");
            }

            Console.Write(cellDivider);
            ConsoleWriteAndSkip(" First name ", firstNameCellSize);
            Console.Write(cellDivider);
            ConsoleWriteAndSkip(" Last name ", lastNameCellSize);
            Console.Write(cellDivider);
            ConsoleWriteAndSkip(" Test title ", testTitleCellSize);
            Console.Write(cellDivider);
            ConsoleWriteAndSkip(" Rating ", ratingCellSize);
            Console.Write(cellDivider);
            ConsoleWriteAndSkip(" Timestamp ", timestampCellSize);
            Console.WriteLine(cellDivider);
            Console.WriteLine(rowDivider);

            foreach (var studentModel in data)
            {
                Console.Write(cellDivider);
                ConsoleWriteAndSkip($" {studentModel.FirstName} ", firstNameCellSize);
                Console.Write(cellDivider);
                ConsoleWriteAndSkip($" {studentModel.LastName} ", lastNameCellSize);
                Console.Write(cellDivider);
                ConsoleWriteAndSkip($" {studentModel.TestTitle} ", testTitleCellSize);
                Console.Write(cellDivider);
                ConsoleWriteAndSkip($" {studentModel.Rating} ", ratingCellSize);
                Console.Write(cellDivider);
                ConsoleWriteAndSkip($" {studentModel.Timestamp:G} ", timestampCellSize);
                Console.WriteLine(cellDivider);
            }
            Console.WriteLine(rowDivider);
        }

        private static void Save(IStorage<StudentModel> studentStorage)
        {
            studentStorage.Save();
            Console.WriteLine("Succesfully saved.");
        }
    }
}