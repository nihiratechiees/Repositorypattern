using System.ComponentModel.DataAnnotations;

namespace Repopattern.Model
{
    public class User
    {
        [Key]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string UserRole { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
