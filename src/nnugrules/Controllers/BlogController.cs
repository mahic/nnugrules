using Microsoft.AspNetCore.Mvc;

namespace nnugrules {
    
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private IBlogRepository _repo { get; set; }

        public BlogController(IBlogRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = _repo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}