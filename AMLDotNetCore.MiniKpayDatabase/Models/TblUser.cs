using System;
using System.Collections.Generic;

namespace AMLDotNetCore.MiniKpayDatabase.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Balance { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
