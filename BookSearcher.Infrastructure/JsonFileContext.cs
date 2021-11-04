using BookSearcher.Domain;
using BookSearcher.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSearcher.Infrastructure
{
    public class JsonFileContext<T> : IContext<T> where T : class
    {

        public async Task<IQueryable<T>> ReadAllAsync()
        {
            return await ReadJsonAsync();
        }

        private async Task<IQueryable<T>> ReadJsonAsync()
        {
            IQueryable<T> books;
            using (var reader = File.OpenText(Configuration.DataSourcePath))
            {
                string jsonString = await reader.ReadToEndAsync();
                books = JsonConvert.DeserializeObject<List<T>>(jsonString).AsQueryable();
            }

            return books;
        }
    }
}
