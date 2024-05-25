using Godot;
using System;

public partial class TurnCounter : Label
{
    public void AddToCounter()
    {
        _counter++;
    }
    public override void _Process(double delta)
    {
        Text = "turn: " + _counter.ToString();
        base._Process(delta);
    }
    private int _counter=0;
}
