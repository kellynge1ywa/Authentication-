using System.ComponentModel.DataAnnotations;

namespace authentication;

public class User
{
     [Key]
        public Guid Id { get; set; }
        public string Fullname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Residence { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Role { get; set; } = "User";

}
