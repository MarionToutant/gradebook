using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);  // Delegate

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            // Section 1: Arrange
            WriteLogDelegate log = ReturnMessage;
            log += ReturnMessage;
            log += IncrementCount;
            // Section 2: Act
            var result = log("Hello!");
            // Section 3: Assert
            Assert.Equal(3, count);
        }
        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {   
            // Section 1: Arrange
            var x = GetInt();
            // Section 2: Act
            SetInt(ref x);
            // Section 3: Assert
            Assert.Equal(42, x);
        }
        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact] 
        public void CSharpCanPassByReference()
        {   
            // Section 1: Arrange
            var book1 = GetBook("Book 1");
            // Section 2: Act
            GetBookSetName(ref book1, "New Name");
            // Section 3: Assert
            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref Book book, string name) // parameter passed by reference
        {
            book = new Book(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {   
            // Section 1: Arrange
            var book1 = GetBook("Book 1");
            // Section 2: Act
            GetBookSetName(book1, "New Name");
            // Section 3: Assert
            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(Book book, string name) // book is a copy of the variable book1
        {
            book = new Book(name);
        }
        
        [Fact]
        public void CanSetNameFromReference()
        {   
            // Section 1: Arrange
            var book1 = GetBook("Book 1");
            // Section 2: Act
            SetName(book1, "New Name");
            // Section 3: Assert
            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            // Section 1: Arrange
            string name = "Marion";
            // Section 2: Act
            var upper = MakeUppercase(name);
            // Section 3: Assert
            Assert.Equal("Marion", name);
            Assert.Equal("MARION", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {            
            // Section 1: Arrange & Act
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            // Section 3: Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {   
            // Section 1: Arrange & Act
            var book1 = GetBook("Book 1");
            var book2 = book1;
            // Section 3: Assert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}