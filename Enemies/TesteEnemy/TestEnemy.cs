using Godot;
using System;

public partial class TestEnemy : BaseEnemy
{
    public override void _Ready()
    {
        SelectEnemyCards(new int[] { 1, 2 });
        ConnectAttributesToChildren();
    }
}
