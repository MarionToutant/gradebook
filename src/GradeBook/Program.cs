using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Marion's Grade Book");
            book.GradeAdded += OnGradeAdded;

            while(true)
            {
                Console.WriteLine("Please enter a grade, or 'q' to quit:");
                var input = Console.ReadLine();
                if(input == "q")
                {
                    break;
                }

                try // We try the following statement, knowing it can throw an exception
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);  
                }
                catch(ArgumentException ex) // If it catches an Argument exception...
                {
                    Console.WriteLine(ex.Message);
                }
                catch(FormatException ex) // If it catches a Format exception...
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }

            var stats = book.GetStatistics();

            Console.WriteLine(Book.CATEGORY);
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The highest grade is {stats.High:N1}");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
