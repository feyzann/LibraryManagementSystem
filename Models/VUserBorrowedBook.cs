using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VUserBorrowedBook
{
    public string FullName { get; set; } = null!;

    public string BookName { get; set; } = null!;

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? MaskedPhoneNumber { get; set; }
}
