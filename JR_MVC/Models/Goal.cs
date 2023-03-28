using System;
using System.Collections.Generic;

namespace JR_MVC.Models;

public partial class Goal
{
    public int IdGoal { get; set; }

    public int GoalBook { get; set; }

    public int Progress { get; set; }

    public int IdUser { get; set; }
}
