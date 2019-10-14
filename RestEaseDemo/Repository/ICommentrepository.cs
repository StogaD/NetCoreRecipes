using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEase;
using RestEaseDemo.Model;

namespace RestEaseDemo.Repository
{

    public interface ICommentRepository
    {
        [Get("/comments")]
        Task<IEnumerable<Comment>> GetComments();

        [Get("/comments/{id}")]
        Task<Comment> GetComment([Path] int id);
    }
}
