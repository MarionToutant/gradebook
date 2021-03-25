using System;
using System.Collections.Generic;

namespace GradeBook // Namespace
{
    public class Book // Class
    {
        public Book(string name) // Explicit Constructor
        {
            grades = new List<double>(); // Initialisation of grades
            this.name = name; // Initialisation of the book name
        }

        public void AddGrade(double grade) // Method to add grades
        {
            grades.Add(grade);
        }

        public Statistics GetStatistics() // Method to get statistics
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach(var grade in grades) 
            {
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Average += grade;
            }

            result.Average /= grades.Count;

            return result; // Returns a result of the Statistics class format
        }

        private List<double> grades; // Field "grades"
        private string name; // Field "name"
    }
}