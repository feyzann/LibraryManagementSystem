﻿using System;
using System.Collections.Generic;

namespace IleriWebProject.Models;

public partial class Borrowtransaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
