using System;
using System.Collections.Generic;

namespace BlazorToDoList.Models;

public partial class Cpr
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string CprNo { get; set; } = null!;
}
