using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class Bookstatus
{
    public int StatusId { get; set; }

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
