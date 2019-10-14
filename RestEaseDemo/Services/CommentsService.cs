using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestEaseDemo.Model;
using RestEaseDemo.Repository;

namespace RestEaseDemo.Services
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetCommentItemAsync(int id);
    }

    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentsService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
        public async Task<Comment> GetCommentItemAsync(int id)
        {
            var comment = await _commentRepo.GetComment(id);

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comment = await _commentRepo.GetComments();

            return comment;
        }
    }
}
