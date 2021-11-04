using BookSearcher.Domain.Entities;
using BookSearcher.Domain.Repositories;
using BookSearcher.Domain.Services;
using BookSearcher.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookSearcher.Tests
{
    public class BookServiceTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IBookService _bookService;
        public BookServiceTest()
        {
            List<Book> books = new List<Book>()
                {new Book()
                    {
                        Id = "B07",
                        Author = "Thurman, Paula",
                        Title = "Splish Splash",
                        Genre =  "Romance",
                        Price = 4.95,
                        Published = new DateTime(2000,11,2),
                        Description = "A deep sea diver finds true love twenty thousand leagues beneath the sea."
                    },
                    new Book()
                    {
                        Id = "B01",
                        Author = "Kutner, Joe",
                        Title = "Deploying with JRuby",
                        Genre =  "Computer",
                        Price = 33,
                        Published = new DateTime(2012,8,15),
                        Description = "Deploying with JRuby is the missing link between enjoying JRuby and using it in the real world to build high-performance, scalable applications."
                    },
                    
                    new Book()
                    {
                        Id = "B09",
                        Author = "Kress, Peter",
                        Title = "Paradox Lost",
                        Genre =  "Science Fiction",
                        Price = 6.95,
                        Published = new DateTime(2000,11,2),
                        Description = "After an inadvertant trip through a Heisenberg Uncertainty Device, James Salway discovers the problems of being quantum."
                    }
                };
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookRepositoryMock.Setup(r => r.GetAsync()).Returns(Task.FromResult(books.AsQueryable()));
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public void GetProperty_ReturnsNonNull()
        {
            PropertyInfo property = _bookService.GetProperty("genre");

            //Assert  
            Assert.NotNull(property);
        }

            [Fact]
        public void GetAllBooksSortedByFieldAsync_SortsAccurately()
        {
            PropertyInfo property = _bookService.GetProperty("id");
            var books = _bookService.GetAllBooksSortedByFieldAsync(property).Result;

            List<string> expectedOrder = new List<string>() { "B01", "B07", "B09" };

            //Assert  
            Assert.NotNull(books);
            Assert.True(books.Select(b => b.Id).SequenceEqual(expectedOrder)); // Don't worry, Select preserves order
        }
    }
}
