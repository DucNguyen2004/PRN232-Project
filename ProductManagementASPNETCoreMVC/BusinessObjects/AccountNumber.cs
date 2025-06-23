using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class AccountNumber
{
    public string MemberId { get; set; } = null!;

    public string? MemberPassword { get; set; }

    public string? FullName { get; set; }

    public string? EmailAddress { get; set; }

    public int? MemberRole { get; set; }
}
