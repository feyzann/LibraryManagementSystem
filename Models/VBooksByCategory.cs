using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VBooksByCategory
{
    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
}
