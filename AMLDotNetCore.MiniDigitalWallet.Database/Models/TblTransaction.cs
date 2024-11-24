using System;
using System.Collections.Generic;

namespace AMLDotNetCore.MiniDigitalWallet.Database.Models;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public string Sender { get; set; } = null!;

    public string Reciever { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
