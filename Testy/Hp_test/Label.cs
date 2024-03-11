using Godot;
using Microsoft.VisualBasic;
using System;

public partial class Label : Godot.Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		base._Ready();
        myHP = GetNode<Hp>("/root/HpTest/Hp");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
        
        str = myHP.currentHealth.ToString();
        Text = str;
        if (direction.X > 0)
        {
            myHP.Damage(2);
        }
        else if (direction.X < 0)
        {
            myHP.Heal(2);
        }

    }
	Hp myHP;
	String str;
}
