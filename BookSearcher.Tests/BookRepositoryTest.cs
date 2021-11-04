using BookSearcher.Domain;
using BookSearcher.Domain.Entities;
using BookSearcher.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookSearcher.Tests
{
    public class BookRepositoryTest
    {
        [Fact]
        public void GetAsync_Returns_Books()
        {
            var contextMock = new Mock<IContext<Book>>();
            contextMock.Setup(c => c.ReadAllAsync()).Returns(Task.FromResult(new List<Book>().AsQueryable()));

            var bookRepository = new BookRepository(contextMock.Object);
            var books = bookRepository.GetAsync().Result;

            //Assert  
            Assert.NotNull(books);
            Assert.IsAssignableFrom<IQueryable<Book>>(books);
        }
    }
}
