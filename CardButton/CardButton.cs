using Godot;
using System;

public partial class CardButton : Button
{
    [Signal]
    public delegate int OnButtonPressedEventHandler(int parentCardIndex);
    public override void _Ready()
    {
        Size = new Vector2(100, 150); //ustawic rozmiar karty
    }
    public override void _Draw()
    {
        DrawSetTransform(GlobalPosition = globalPosition);
    }
    public void SetGlobalPosition(Vector2 position) { globalPosition = position; }
    Vector2 globalPosition;
    public void setParentCardIndex(int parentCardIndex) { this.parentCardIndex = parentCardIndex; }
    public int getParentCardIndex() { return this.parentCardIndex; }
    private int parentCardIndex;
}
