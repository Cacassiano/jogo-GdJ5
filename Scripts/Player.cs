using Godot;
using System;
using System.Collections.Generic;



public partial class Player : CharacterBody2D
{

    
    public void _Trocar(Area2D area)
    {
        if (area is WarpE w)
        {
            GlobalPosition = w.markerGbP;
        }
        else if(area is Dinheiro din)
        {
            getHit(din.Damage);
        }
    }

    [Export]
    public int Speed = 50000;
    [Export]
    public float RunMulti = 2f;
    [Export]
    public int MaxHp = 8;

    private int _currentHp;
    private bool imortal = false;
    private bool podeAtacar = true;
    private bool atirando = false;
    public AnimatedSprite2D animacao;
    private Vector2 gbBixo;
    public override void _Ready()
    {
        _currentHp = MaxHp;
        animacao = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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

    public override void _PhysicsProcess(double delta)
    {
        var _velocity = Input.GetVector("MovEsquerda", "MovDireita", "MovCima", "MovBaixo");
        var mouPos = GetGlobalMousePosition();
        var direction = (mouPos - GlobalPosition).Normalized();
        var dirH = horizontalDir(direction);
        var dirV = verticalDir(direction);

        String animation = "";
        if (Input.IsActionPressed("atirar") && podeAtacar)
        {
            shoot();
            if(!atirando)
            {
                atirando = true;
                if(Math.Abs(direction.X) < Math.Abs(direction.Y))
                {
                    animacao.Animation = "ataque" + dirV;
                }
                else
                {
                    animacao.Animation = "ataque" + dirH;
                }
                animacao.Play();
            }
        }
        if(!atirando)
        {
            if(_velocity == Vector2.Zero)
            {
                if(Math.Abs(direction.X) < Math.Abs(direction.Y))
                {
                    if(dirV == "_cima")
                    {
                        animation = "paradoDeCostas";
                    }
                    else
                    {
                        animation = "paradoDeFrente";
                    } 
                } 
                else
                {
                    if(dirH == "_esq")
                    {
                        animation = "paradoDeEsquerda";
                    }
                    else
                    {
                        animation = "paradoDeDireita";
                    } 
                }
            } 
            else
            {
                if(Math.Abs(direction.X) < Math.Abs(direction.Y))
                {
                    animation = "andando" + dirV;
                }
                else
                {
                    animation = "andando" + dirH;
                }
            }
            animacao.Animation = animation;
            animacao.Play();
        }
 
        int mySpeed = Input.IsActionPressed("MovCorrer") ? (int)(Speed * RunMulti) : Speed;
        _velocity = _velocity.Normalized() * mySpeed;
        this.Velocity = _velocity * (float)delta;

        
        MoveAndSlide();
    }

    /*public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("atirar") && podeAtacar)
        {
            shoot();
            atirando = true;
            var direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            var dirH = horizontalDir(direction);
            var dirV = verticalDir(direction);

            
        }
    }*/

    public void TakeDamage(int amount)
    {
        _currentHp -= amount;
        GD.Print("HP do jogador: " + _currentHp);
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void _FimCoolDeAtack()
    {
        GD.Print("Fim do cooldown de ataque");
        podeAtacar = true;
    }
    private void shoot()
    {
        GD.Print("Atirou!");
        var projetil = (Projetil) GD.Load<PackedScene>("res://Cenas/Projetil.tscn").Instantiate();
        projetil.GlobalPosition = GlobalPosition;
        projetil.mousePos = GetGlobalMousePosition();
        GetParent().AddChild(projetil);
        podeAtacar = false;
        GetNode<Timer>("cooldownAtack").Start();
    }
    private void Die()
    {
        GD.Print("O jogador morreu!");
        QueueFree();
    }

    private void getHit(int Damage)
    {
        GetNode<Timer>("imortal").Start();
        SetCollisionLayerValue(6, false);
        SetCollisionMaskValue(6, false);

        GetNode<Area2D>("HITBOX").SetCollisionLayerValue(7, false);
        GetNode<Area2D>("HITBOX").SetCollisionMaskValue(7, false);
        GD.Print("Estou imortal");
        TakeDamage(Damage);
        imortal = true;
        GD.Print("O inimigo atacou o jogador!");
    }
    public void _hit(Node2D body)
    {
        if(!imortal && body is Inimigo inimigo)
        {
            getHit(inimigo.Damage);
        }
        else if(!imortal && body is Projetil projetil)
        {
            getHit(projetil.Damage);
        }
        
    }

    public void _on_imortal_timeout()
    {
        imortal = false;
        SetCollisionLayerValue(6, true);
        SetCollisionMaskValue(6, true);
        GetNode<Area2D>("HITBOX").SetCollisionLayerValue(7, true);
        GetNode<Area2D>("HITBOX").SetCollisionMaskValue(7, true);
        GD.Print("Não estou imortal");
    }

    public void _FimTIro()
    {
        atirando = false;
        GD.Print("Fim do tiro");
        animacao.Stop();
    }

}
