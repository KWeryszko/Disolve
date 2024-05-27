using Godot;
using System;

public partial class GreenSlime : BaseEnemy
{
    public override void _Ready()
    {
        ConnectAttributesToChildren();
        SetHpBar();
    }
}
