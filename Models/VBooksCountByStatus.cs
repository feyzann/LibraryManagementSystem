using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class VBooksCountByStatus
{
    public string StatusDescription { get; set; } = null!;

    public long BookCount { get; set; }
}
