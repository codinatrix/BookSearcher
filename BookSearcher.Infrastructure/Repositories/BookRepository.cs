using BookSearcher.Domain;
using BookSearcher.Domain.Entities;
using BookSearcher.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSearcher.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        // Generic context can be swapped out for EF or other datasource later
        private readonly IContext<Book> _context;

        public BookRepository(IContext<Book> context)
        {
            _context = context;
        }

        public async Task<IQueryable<Book>> GetAsync()
        {
            return await _context.ReadAllAsync();
        }
    }
}
