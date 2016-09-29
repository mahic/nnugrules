using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace nnugrules
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BloggingContext _context;
        public BlogRepository(BloggingContext context)
        {
            _context = context;
            _context.Blogs.Add(new Blog() {BlogId = 1, Url = "abc"});
            _context.SaveChanges();
        }

        public IEnumerable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public void Add(Blog item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public Blog Find(string key)
        {
            return _context.Blogs.AsNoTracking().FirstOrDefault(c => c.BlogId.ToString() == key);
        }

        public Blog Remove(string key)
        {
            var entryToDelete = Find(key);
            var entry = _context.Blogs.Remove(entryToDelete);
            _context.SaveChanges();
            return entry.Entity;
        }

        public void Update(Blog item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}