using BookSearcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSearcher.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetAsync();
    }
}
