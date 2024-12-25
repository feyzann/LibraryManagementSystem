using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VUsersBorrowedBooksStatus
{
    public string FullName { get; set; } = null!;

    public string BookName { get; set; } = null!;

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string StatusDescription { get; set; } = null!;
}
