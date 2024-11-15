using System;
using System.Collections.Generic;

namespace AMLDotNetCore.MiniKpayDatabase.Models;

public partial class TblTranLog
{
    public int TransactionId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public string Amunt { get; set; } = null!;

    public string? Note { get; set; }

    public string Time { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
