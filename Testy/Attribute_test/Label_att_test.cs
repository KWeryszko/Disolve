using Godot;
using System;

public partial class Label_att_test : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        base._Ready();
        myATT = GetNode<Attribute>("/root/AttributeTest/Attribute");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        str = myATT.currentAttribute.ToString();
        Text = str;
        if (direction.X > 0)
        {
            myATT.AddAttribute();
        }
        else if (direction.X < 0)
        {
            myATT.RemoveAttribute();
        }
        if (direction.Y > 0)
        {
            myATT.BuffAttribute();
        }
        else if (direction.Y < 0)
        {
            myATT.DebuffAttribute();
        }
    }
    string str;
	Attribute myATT;
}
