using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
    public String sala;
    [Export]
    public float DistanciaMinimaParaMovimentar = 50f;
    [Export]
    public Vector2 knockback = Vector2.Zero;
    public int Speed = 575;
    [Export]
    public float RunMulti = 2f;
    public CharacterBody2D PlayerScene{ get; set; }
    public Vector2 direction;
    public void _SetPlayerScene(CharacterBody2D player)
    {
        PlayerScene = player;
    }
    
    private NavigationAgent2D agent;
    public override void _Draw()
    {
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
    }
    public override void _Ready()
    {
        _player = GetNode<Node2D>("res://Cenas/player.tscn");
    }

    public override void _Process(double delta)
    {
        if (_player == null) return;

        float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);
    }
    public override void _PhysicsProcess(double delta)
    {
        agent.TargetPosition = PlayerScene.GlobalPosition;
        if(knockback != Vector2.Zero)
        {
            this.Velocity = knockback * Speed;
            knockback = Vector2.Zero;
        }
        else
        {
            direction = GlobalPosition.DirectionTo(agent.GetNextPathPosition()) * Speed;
            this.Velocity = direction;
        }

        MoveAndSlide(); 
    } 

    public void _TrocaASala()
    {
        QueueFree();
    }

    public void _TomeiBala(Area2D bala)
    {
        if (bala is Projetil projetil)
        {
            GD.Print("Tomei uma bala");
            vida -= projetil.Damage;
            GD.Print("Vida do inimigo: " + vida);
            if (vida <= 0)
            {
                QueueFree();
            } 
            else
            {
                knockback = projetil.direction;
            }
        }
    }

    [Export]
    public int Damage = 1;

    private Node2D _player;
    [Export]
    public int vida = 5;

    

}