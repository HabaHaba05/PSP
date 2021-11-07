using LibraryUsage.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryUsage.Mapper
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PhoneNr { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }

        public User ConvertToUser()
        {
            return new User()
            {
                Name = Name,
                Surname = Surname,
                PhoneNr = PhoneNr,
                Email = Email,
                Address = Address,
                Password = Password
            };
        }
    }
}
