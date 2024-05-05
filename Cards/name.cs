using Godot;
using System;

public partial class name : Label
{
    public override void _Ready()
    {
        bc = GetNode<BaseCard>(GetPathTo(GetParent().GetParent().GetParent()));
        string str = bc.Stats.Name;
        Text = str;
    }
    public override void _Process(double delta)
    {
        Update();
    }
    public void Update()
    {
        string str = bc.Stats.Name;
        Text = str;
    }
    BaseCard bc;
}
