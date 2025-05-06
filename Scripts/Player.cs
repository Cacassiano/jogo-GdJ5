using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed = 400;
	[Export]
	public float RunMulti = 2f;
	public override void _Process(double delta)
	{
		var _velocity = new Vector2();

		if (Input.IsActionPressed("MovDireita"))
		{
			_velocity.X += 1;
		}
		if (Input.IsActionPressed("MovEsquerda"))
		{
			_velocity.X -= 1;
		}
		if (Input.IsActionPressed("MovBaixo"))
		{
			_velocity.Y += 1;
		}
		if (Input.IsActionPressed("MovCima"))
		{
			_velocity.Y -= 1;
		}
		int mySpeed = (Input.IsActionPressed("MovCorrer")) ? (int)(Speed*RunMulti) : Speed;
		_velocity = _velocity.Normalized() * mySpeed;

		Position += _velocity * (float)delta;
		
	}
}
