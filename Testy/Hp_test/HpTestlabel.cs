using Godot;
using Microsoft.VisualBasic;
using System;

public partial class HpTestlabel : Godot.Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		base._Ready();
        myHP = GetNode<HP2>("/root/HpTest/Hp2");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
        
        str = myHP.getcurrentHealth().ToString()+"/"+myHP.getmaxHealth().ToString()+"; is alive: "+myHP.IsAlive().ToString();
        Text = str;
        if (direction.X > 0)
        {
            myHP.Damage(1);
        }
        else if (direction.X < 0)
        {
            myHP.Heal(1);
        }
        if (direction.Y < 0)
        {
            myHP.AddmaxHealth(1);
        }
        else if (direction.Y > 0)
        {
            myHP.DecreasemaxHealth(1); 
        }

    }
	HP2 myHP;
	String str;
}
