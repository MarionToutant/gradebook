using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook // Namespace
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);  // Delegate

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        
        public string Name 
        {        // Name property
            get; // public getters and setters
            set;
        }
    }

    public interface IBook 
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name) 
        {   
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name) 
        {   
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null) 
                {
                    GradeAdded(this, new EventArgs());
                }
            }

            writer.Dispose();
        }

        public override Statistics GetStatistics() 
        {
            var result = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;
        }

    }

    public class InMemoryBook : Book // Class InMemoryBook derived from Book
    {
        public InMemoryBook(string name) : base(name) 
        {   // Explicit Constructor, has to have the same name and no return type
            grades = new List<double>(); // Initialisation of grades
            Name = name; // Initialisation of the book name
        }

        public void AddGrade(char letter) // Method to add grades
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

        public override void AddGrade(double grade) // Method to add grades
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

        public override event GradeAddedDelegate GradeAdded; // Field, Event

        public override Statistics GetStatistics() // Method to get statistics
        {
            var result = new Statistics(); // Result in the Statistics class format (Average, High, Low)

            for(var index = 0; index < grades.Count; index += 1) 
            {
                result.Add(grades[index]);
            }

            return result;
        }

        private List<double> grades; // Field "grades"

        public const string CATEGORY = "Science";
    }
}