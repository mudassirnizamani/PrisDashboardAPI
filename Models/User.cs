using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PrisDashboardAPI.Models
{
    public class User : IdentityUser
    {

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; }


        public string CreatedAt { get; set; }

        [Required]
        public string ProfilePic { get; set; }

    }
}