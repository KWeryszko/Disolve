using Godot;
using System;

public partial class rrr : ProgressBar
{
    public override void _Ready()
    {
        MaxValue = 100;
        Step = 1;
        Value = MaxValue;

    }
    public override void _Process(double delta)
    {
        Value -= 1;
        
    }
}
