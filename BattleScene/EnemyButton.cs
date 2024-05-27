using Godot;
using System;

public partial class EnemyButton : Button
{
    [Signal]
    public delegate int OnButtonPressedEventHandler();

    public override void _Ready()
    {
        Size = new Vector2(100, 200); //ustawic rozmiar przycisku
    }
    public override void _Draw()
    {
        DrawSetTransform(GlobalPosition = globalPosition);
    }
    public void SetGlobalPosition(Vector2 position) { globalPosition = position; }
    Vector2 globalPosition = new Vector2(800, 250);
}
