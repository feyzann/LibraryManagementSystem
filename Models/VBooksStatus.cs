using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VBooksStatus
{
    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int PublicationYear { get; set; }

    public string StatusDescription { get; set; } = null!;
}
