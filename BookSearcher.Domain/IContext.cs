using BookSearcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSearcher.Domain
{
    public interface IContext<T> where T : class
    {
        public Task<IQueryable<T>> ReadAllAsync();
    }
}
