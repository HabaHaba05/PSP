using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryUsage.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public string Surname { get; set; }

        [Required]
        [MinLength(1)]
        public string PhoneNr { get; set; }

        [Required]
        [MinLength(1)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Address { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }
    }
}
