using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Manager
{
    public int ManagerId { get; set; }

    public string? ManagerName { get; set; }

    public string? ManagerPassword { get; set; }

    public string? ManagerPhonoNo { get; set; }
}
