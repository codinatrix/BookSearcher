using BookSearcher.API.RequestObjects;
using BookSearcher.Domain.Entities;
using BookSearcher.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BookSearcher.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAsync()
        {
            return await _bookService.GetAllBooksAsync();
        }

        [HttpGet("{propertyToSortBy}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> GetSortedBooksAsync(string propertyToSortBy)
        {
            PropertyInfo property = _bookService.GetProperty(propertyToSortBy);
            if (property == null)
                return BadRequest("You are trying to sort by a field that does not exist on books.");

            return await _bookService.GetAllBooksSortedByFieldAsync(property);
        }

        [HttpGet("id/{searchString}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByIdAsync(string searchString)
        {
            return await _bookService.SearchBooksByIdAsync(searchString);
        }

        [HttpGet("author/{searchString}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByAuthorAsync(string searchString)
        {
            return await _bookService.SearchBooksByAuthorAsync(searchString);
        }

        [HttpGet("title/{searchString}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByTitleAsync(string searchString)
        {
            return await _bookService.SearchBooksByTitleAsync(searchString);
        }

        [HttpGet("genre/{searchString}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByGenreAsync(string searchString)
        {
            return await _bookService.SearchBooksByGenreAsync(searchString);
        }

        [HttpGet("description/{searchString}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByDescriptionAsync(string searchString)
        {
            return await _bookService.SearchBooksByDescriptionAsync(searchString);
        }

        [HttpGet("price/{**priceRequest}")]
        [Produces("application/json")]
        // GET: /api/books/price/30.0 searches for books with specific price
        // GET: /api/books/price/30.0&35.0 searches for books in price range
        // Routing to action method parameter is handled in PriceRequestBinder
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByPriceAsync(PriceRequest priceRequest)
        {
            if (priceRequest.isSinglePrice)
                return await _bookService.SearchBooksByPriceAsync(priceRequest.SinglePrice.Value);
            else if (priceRequest.isPriceRange)
                return await _bookService.SearchBooksByPriceRangeAsync(priceRequest.MinPrice.Value, priceRequest.MaxPrice.Value);

            // PriceRequestBinder would have thrown an exception before even entering this method if the action method
            // parameter formatting was invalid, so if we get here there is something wrong in the backend.
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("published/{year}/{month?}/{day?}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByPublishedAsync(int year, int? month, int? day)
        {
            int testmonth = month.HasValue ? month.Value : 1;
            int testday = day.HasValue ? day.Value : 1;

            try
            {
                new DateTime(year, testmonth, testday);
            } catch
            {
                return BadRequest("Invalid date");
            }

            return await _bookService.SearchBooksByPublishedAsync(year, month, day);
        }
    }
}
