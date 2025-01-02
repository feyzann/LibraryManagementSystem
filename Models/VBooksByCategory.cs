using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VBooksByCategory
{
    public int BookId { get; set; } = 0!;
    public int PublicationYear { get; set; } = 0!;
    public int StockCount { get; set; } = 0!;
    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
}
