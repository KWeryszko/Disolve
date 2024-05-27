using Godot;
using System;

public partial class PlayerBattleScene : BaseEnemy
{
    public override void _Ready()
    {
        ConnectAttributesToChildren();
        SetHpBar();
    }
}
