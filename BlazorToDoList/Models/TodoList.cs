using System;
using System.Collections.Generic;

namespace BlazorToDoList.Models;

public partial class TodoList
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string Item { get; set; } = null!;
}
