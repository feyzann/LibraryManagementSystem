using System.ComponentModel.DataAnnotations;

namespace IleriWebProject.Models
{
    public class VMemberList
    {
        [Key]
        public int UserID { get; set; } = 0!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public int DaysRegistered { get; set; } = 0!;
    }
}
