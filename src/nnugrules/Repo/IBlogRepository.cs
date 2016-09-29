using System.Collections.Generic;

namespace nnugrules
{
    public interface IBlogRepository
    {
        void Add(Blog item);
        IEnumerable<Blog> GetAll();
        Blog Find(string key);
        Blog Remove(string key);
        void Update(Blog item);
    }
}