using BookSearcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookSearcher.Domain.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetAllBooksSortedByFieldAsync(PropertyInfo propertyToSortBy);
        Task<List<Book>> SearchBooksByIdAsync(string searchString);
        Task<List<Book>> SearchBooksByAuthorAsync(string searchString);
        Task<List<Book>> SearchBooksByTitleAsync(string searchString);
        Task<List<Book>> SearchBooksByGenreAsync(string searchString);
        Task<List<Book>> SearchBooksByDescriptionAsync(string searchString);
        Task<List<Book>> SearchBooksByPriceAsync(double price);
        Task<List<Book>> SearchBooksByPriceRangeAsync(double minPrice, double maxPrice);
        Task<List<Book>> SearchBooksByPublishedAsync(int year, int? month, int? day);
        PropertyInfo GetProperty(string propertyOnBook);
    }
}
