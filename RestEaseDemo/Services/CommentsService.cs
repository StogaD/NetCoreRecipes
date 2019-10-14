using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestEaseDemo.Model;

namespace RestEaseDemo.Services
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetCommentItemAsync(int id);
    }

    public class CommentsService : ICommentsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "https://jsonplaceholder.typicode.com/Comments";

        public CommentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Comment> GetCommentItemAsync(int id)
        {
            var ressponseString = await _httpClient.GetStringAsync($"{_remoteServiceBaseUrl}/{id}");
            var Comments = JsonConvert.DeserializeObject<Comment>(ressponseString);

            return Comments;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var ressponseString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl);
            var Comments = JsonConvert.DeserializeObject<IEnumerable<Comment>>(ressponseString);

            return Comments;
        }
    }
}
