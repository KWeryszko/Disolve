using Godot;
using System;

public partial class ArmourTestLabel : Label
{
    public override void _Ready()
    {
        base._Ready();
        armour = GetNode<Armour>("/root/ArmourTest/Armour");

    }
    public override void _Process(double delta)
    {
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");


        str = "armour: "+armour.getcurrentArmour().ToString()+"; has armour:"+armour.HasArmour().ToString() + "; spillover: "+spilloverdamage.ToString();
        Text = str;
        if (direction.X > 0)
        {
            spilloverdamage+=armour.Damage(1);
        }
        else if (direction.X < 0)
        {
            armour.AddArmour(2);
        }
        if (direction.Y > 0)
        {
            armour.ClearArmour();
        }
        else if(direction.Y < 0)
        {
            spilloverdamage = 0;
        }

    }

    private Armour armour;
    private String str;
    private int spilloverdamage=0;
}
