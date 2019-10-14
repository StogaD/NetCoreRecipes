using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRDemo.Model
{
    public class InputModel
    {
        [Required]
        public string Message { get; set; }
    }
}
