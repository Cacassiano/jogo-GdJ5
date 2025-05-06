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

		_velocity = Input.GetVector("MovEsquerda","MovDireita", "MovCima", "MovBaixo");

		int mySpeed = Input.IsActionPressed("MovCorrer") ? (int)(Speed*RunMulti) : Speed;
		_velocity = _velocity.Normalized() * mySpeed;

		Position += _velocity * (float)delta;
		
	}

	
}
