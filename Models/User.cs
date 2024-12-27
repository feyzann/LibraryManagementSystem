using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleID { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Borrowtransaction> Borrowtransactions { get; set; } = new List<Borrowtransaction>();

    public virtual Role Role { get; set; } = null!;
}
