using CacheDemo.CahceDemo;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheDemo.Models
{
    public class Album
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        //* for cache inMemory Demo*/
        [JsonConverter(typeof(StringEnumConverter))] 
        public DataSourceEnum FromCacheOrService { get; set; }
    }
}
