using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
   
    [Export]
    public float DistanciaMinimaParaMovimentar = 50f;
    [Signal]
    public delegate void PlayerHitEventHandler(int Damage);
    [Export]
    public int Speed = 400;
    [Export]
    public float RunMulti = 2f;
    public CharacterBody2D PlayerScene{ get; set; }
    public void _SetPlayerScene(CharacterBody2D player)
    {
        PlayerScene = player;
    }
    
    private NavigationAgent2D agent;
    public override void _Draw()
    {
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
    }
    public override void _PhysicsProcess(double delta)
    {
        agent.TargetPosition = PlayerScene.GlobalPosition;
        this.Velocity = GlobalPosition.DirectionTo(agent.GetNextPathPosition()) * Speed;

        MoveAndSlide(); 
    } 
   [Export]
    public float AttackRange = 30f;

    [Export]
    public float AttackCooldown = 1.5f;

    [Export]
    public int Damage = 1;

    private float _attackTimer = 0;

    private Node2D _player;
    [Export]
    public int vida = 5;

    public override void _Ready()
    {
        _player = GetNode<Node2D>("res://Cenas/player.tscn");
    }

    public override void _Process(double delta)
    {
        if (_player == null) return;

        float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);
        /*
        _attackTimer -= (float)delta;

        if (distance <= AttackRange && _attackTimer <= 0)
        {
            AttackPlayer();
            _attackTimer = AttackCooldown;
        }
        */
    }

    public void _hit(Node2D body)
    {
        if (body is Player player)
        {
            EmitSignal(SignalName.PlayerHit,Damage);
            GD.Print("O inimigo atacou o jogador!");
        }
    }

}