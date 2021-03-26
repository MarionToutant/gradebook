using System;
using System.Collections.Generic;

namespace GradeBook // Namespace
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);  // Delegate
    
    public class Book // Class
    {
        public Book(string name) // Explicit Constructor
        {
            grades = new List<double>(); // Initialisation of grades
            Name = name; // Initialisation of the book name
        }

        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
               
                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }

        public void AddGrade(double grade) // Method to add grades
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public event GradeAddedDelegate GradeAdded; // Field, Event

        public Statistics GetStatistics() // Method to get statistics
        {
            var result = new Statistics(); // Result in the Statistics class format (Average, High, Low)
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            for(var index = 0; index < grades.Count; index += 1) 
            {
                if(grades[index] ==  42.1) 
                {
                    break;
                }

                result.High = Math.Max(grades[index], result.High);
                result.Low = Math.Min(grades[index], result.Low);
                result.Average += grades[index];
            }

            result.Average /= grades.Count;

            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;

                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;

                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;

                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;

                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

        private List<double> grades; // Field "grades"

        public string Name 
        {                       // Property "Name"
            get;
            set;
        }

        public const string CATEGORY = "Science";
    }
}