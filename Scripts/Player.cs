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

		var animacao = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if(Input.IsActionPressed("MovBaixo"))
		{
			animacao.Animation = "andandoDeFrente";
		}
		else if(Input.IsActionPressed("MovDireita"))
		{
			animacao.Animation = "andandoDireita";
		}
		else if(Input.IsActionPressed("MovEsquerda"))
		{
			animacao.Animation = "andandoEsquerda";
		}
		else if(Input.IsActionPressed("MovCima"))
		{
			animacao.Animation = "andandoCima";
		}
		

		_velocity = Input.GetVector("MovEsquerda","MovDireita", "MovCima", "MovBaixo");

		if(_velocity==Vector2.Zero))
		{
			animacao.Animation="paradoDeFrente";
		}	
		int mySpeed = Input.IsActionPressed("MovCorrer") ? (int)(Speed*RunMulti) : Speed;
		_velocity = _velocity.Normalized() * mySpeed;

		this.Velocity = _velocity * (float)delta;
		animacao.Play();
		MoveAndSlide();
		
	}
}
