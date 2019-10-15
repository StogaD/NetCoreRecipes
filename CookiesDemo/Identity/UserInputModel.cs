using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookiesDemo.Identity
{
    public class UserInputModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
