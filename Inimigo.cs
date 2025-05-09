using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{

    [Signal]
    public delegate void MorriEventHandler();
    public int id = 1;
    public String sala;
    [Export]
    public float DistanciaMinimaParaMovimentar = 50f;
    [Export]
    public Vector2 knockback = Vector2.Zero;
    public int Speed = 575;
    [Export]
    public float RunMulti = 1f;
    public Player PlayerScene{ get; set; }
    public Vector2 direction;
    public AnimatedSprite2D animacao;
    public void _SetPlayerScene(Player player)
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
        animacao = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        

    }
    private String horizontalDir(Vector2 direction)
    {
        if (direction.X < 0)
        {
            return "_esq";
        }
        else
        {
            return "_dir";
        }
    }

    private String verticalDir(Vector2 direction)
    {
        if (direction.Y < 0)
        {
            return "_cima";
        }
        else
        {
            return "_bai";
        }
    }

    public override void _Process(double delta)
    {
        if (_player == null) return;

        float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);
    }
    public override void _PhysicsProcess(double delta)
    {
        if(!podeandar)
        {
            return;
        }
        
        agent.TargetPosition = PlayerScene.GlobalPosition;
        Vector2 direcao = GlobalPosition.DirectionTo(agent.GetNextPathPosition());
        
        if(knockback != Vector2.Zero)
        {
            this.Velocity = knockback * Speed * 1.5f;
            knockback = Vector2.Zero;
        }
        else
        {
            direction = direcao * Speed  * RunMulti;    
            this.Velocity = direction;
        }
        var dirH = horizontalDir(direcao);
        var dirV = verticalDir(direcao);

        String animation = "";
        if(Math.Abs(direction.X) < Math.Abs(direction.Y))
        {
            animation = "and" + dirV;
        }
        else
        {
            animation = "and" + dirH;
        }
        
        animacao.Animation = animation;
        animacao.Play();
        RunMulti = 1f;
        MoveAndSlide(); 
    } 

    

    public void _TrocaASala()
    {
        QueueFree();
    }

    bool podeandar = true;
    public void _TomeiBala(Area2D bala)
    {
        if (bala is Projetil projetil)
        {
            GD.Print("Tomei uma bala");
            vida -= projetil.Damage;
            GD.Print("Vida do inimigo: " + vida);
            if (vida <= 0)
            {
                EmitSignal(SignalName.Morri);
                QueueFree();               
            } 
            else
            {
                knockback = projetil.direction * 2;
            }
        }
        else if (bala.Name == "HITBOX")
        {
            GD.Print("Ataquei player");

            RunMulti = -4f;
        }

    }

    [Export]
    public int Damage = 1;

    private Node2D _player;
    [Export]
    public int vida = 5;

    

}