using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		//OBRACANIE
		AnimatedSprite2D mysprite = GetNode<AnimatedSprite2D>("animacja_s");
		if(direction > Vector2.Zero)
		{
			mysprite.FlipH = true;
		}
		else if(direction < Vector2.Zero){
			mysprite.FlipH= false;	
		}
		//
        Velocity = velocity;
		MoveAndSlide();
	}
}
