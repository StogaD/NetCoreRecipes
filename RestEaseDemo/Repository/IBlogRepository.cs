using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEase;
using RestEaseDemo.Model;

namespace RestEaseDemo.Repository
{
    public interface IBlogRepository
    {
        [Get("/posts/{id}")]
        Task<BlogPost> GetPost([Path] int id);

        [Get("/posts")]
        Task<IEnumerable<BlogPost>> GetPosts();
    }
}
