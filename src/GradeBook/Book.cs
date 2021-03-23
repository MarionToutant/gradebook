using System;
using System.Collections.Generic;

namespace GradeBook // Namespace
{
    class Book // Class
    {
        public Book(string name) // Explicit Constructor
        {
            grades = new List<double>(); // Initialisation of grades
            this.name = name; 
        }

        public void AddGrade(double grade) // Method
        {
            grades.Add(grade);
        }

        public void ShowStatistics()
        {
            var result = 0.0;
            var highGrade = double.MinValue;
            var lowGrade = double.MaxValue;

            foreach(double number in grades) 
            {
                highGrade = Math.Max(number, highGrade);
                lowGrade = Math.Min(number, lowGrade);
                result += number;
            }

            result /= grades.Count;
            
            Console.WriteLine($"The average grade is {result:N1}");
            Console.WriteLine($"The highest grade is {highGrade:N1}");
            Console.WriteLine($"The lowest grade is {lowGrade:N1}");
        }

        private List<double> grades; // Field
        private string name;
    }
}