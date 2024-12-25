using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class Book
{
    public int BookId { get; set; }

    public int StatusId { get; set; }

    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int PublicationYear { get; set; }

    public int CategoryId { get; set; }

    public int StockCount { get; set; }

    public virtual ICollection<Borrowtransaction> Borrowtransactions { get; set; } = new List<Borrowtransaction>();

    public virtual Category Category { get; set; } = null!;

    public virtual Bookstatus Status { get; set; } = null!;
}
