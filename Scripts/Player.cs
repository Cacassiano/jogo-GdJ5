using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed = 400;
	[Export]
	public float RunMulti = 2f;
	public override void _PhysicsProcess(double delta)
	{
		var _velocity = new Vector2();

		_velocity = Input.GetVector("MovEsquerda","MovDireita", "MovCima", "MovBaixo");

		int mySpeed = Input.IsActionPressed("MovCorrer") ? (int)(Speed*RunMulti) : Speed;
		_velocity = _velocity.Normalized() * mySpeed;

		this.Velocity = _velocity * (float)delta;

		MoveAndSlide();
		
	}
}
