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
        str = myATT.getcurrentAttribute().ToString()+"/"+myATT.getbaseAttribute()+" attname: "+myATT.getattributeName()+myATT.getattributeNameValue();
        Text = str;
        if (direction.X > 0)
        {
           myATT.BuffAttribute(1);
        }
        else if (direction.X < 0)
        {
            myATT.DebuffAttribute(1);
        }
        if (direction.Y > 0)
        {
            myATT.AddBaseAttribute(1);
        }
        else if (direction.Y < 0)
        {
            myATT.DecreaseBaseAttribute(1);
        }
        if (myATT.getattributeName() =="None")
        {
            Text = "grupa 21Inf-SP/b jest gnebiona przez uz i spolecznosc miedzynarodowa ma to gdzies";
        }
    }
    string str;
	Attribute myATT;
}
