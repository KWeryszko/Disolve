using Godot;
using System;

public partial class HP_label2 : Label
{
    public override void _Process(double delta)
    {
        Text = "HP: " + GetParent<BaseEnemy>().getCurrentHP().ToString() + "/" + GetParent<BaseEnemy>().getMaxHP().ToString();
    }
}
