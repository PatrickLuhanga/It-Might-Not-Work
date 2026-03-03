using System.ComponentModel.DataAnnotations;

namespace RePurpose.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Roles: "Buyer" or "Manager"
        [Required]
        public string Role { get; set; }
    }
}
