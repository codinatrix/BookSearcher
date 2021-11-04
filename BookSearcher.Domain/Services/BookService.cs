using BookSearcher.Domain.Entities;
using BookSearcher.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookSearcher.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
           return (await _bookRepository.GetAsync()).ToList();
        }

        public async Task<List<Book>> GetAllBooksSortedByFieldAsync(PropertyInfo propertyToSortBy)
        {
            return SortByField(await _bookRepository.GetAsync(), propertyToSortBy);
        }

        public async Task<List<Book>> SearchBooksByIdAsync(string searchString)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => (b.Id ?? string.Empty).ToLower().Contains(searchString.ToLower()));
            return searchedBooks.OrderBy(b => b.Id).ToList(); // SortByField method isn't used here in order to avoid using strings unnecessarily
        }

        public async Task<List<Book>> SearchBooksByAuthorAsync(string searchString)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => (b.Author ?? string.Empty).ToLower().Contains(searchString.ToLower()));
            return searchedBooks.OrderBy(b => b.Author).ToList();
        }

        public async Task<List<Book>> SearchBooksByTitleAsync(string searchString)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => (b.Title ?? string.Empty).ToLower().Contains(searchString.ToLower()));
            return searchedBooks.OrderBy(b => b.Title).ToList();
        }

        public async Task<List<Book>> SearchBooksByGenreAsync(string searchString)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => (b.Genre ?? string.Empty).ToLower().Contains(searchString.ToLower()));
            return searchedBooks.OrderBy(b => b.Genre).ToList();
        }

        public async Task<List<Book>> SearchBooksByDescriptionAsync(string searchString)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => (b.Description ?? string.Empty).ToLower().Contains(searchString.ToLower()));
            return searchedBooks.OrderBy(b => b.Description).ToList();
        }

        public async Task<List<Book>> SearchBooksByPriceAsync(double price)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks.Where(b => b.Price == price);
            return searchedBooks.OrderBy(b => b.Price).ToList();
        }

        public async Task<List<Book>> SearchBooksByPriceRangeAsync(double minPrice, double maxPrice)
        {
            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks
                .Where(b => b.Price >= minPrice && b.Price <= maxPrice);
            return searchedBooks.OrderBy(b => b.Price).ToList();
        }

        public async Task<List<Book>> SearchBooksByPublishedAsync(int year, int? month, int? day)
        {
            if (!month.HasValue && day.HasValue)
                throw new ArgumentException("Received value for day but not year.");

            IQueryable<Book> allBooks = await _bookRepository.GetAsync();
            IQueryable<Book> searchedBooks = allBooks;

            if (month.HasValue && day.HasValue)
                searchedBooks = allBooks.Where(b => b.Published.Equals(new DateTime(year, month.Value, day.Value)));
            else if(month.HasValue)
                searchedBooks = allBooks.Where(b => b.Published.Year.Equals(year) && b.Published.Month.Equals(month));
            else
                searchedBooks = allBooks.Where(b => b.Published.Year.Equals(year));

            return searchedBooks.OrderBy(b => b.Published).ToList();
        }

        public PropertyInfo GetProperty(string propertyOnBook)
        {
            return typeof(Book).GetProperty(propertyOnBook, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        }

        private List<Book> SortByField(IQueryable<Book> books, PropertyInfo propertyToSortBy)
        {
            return books.OrderBy(b => propertyToSortBy.GetValue(b, null)).ToList();
        }
    }
}
