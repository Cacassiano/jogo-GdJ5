using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    [Export]
    public int Speed = 50000;
    public List<string> salasVencidas;
    [Export]
    public float RunMulti = 2f;

    [Export]
    public int MaxHp = 8;

    private int _currentHp;
    private bool imortal = false;
    private bool podeAtacar = true;
    public override void _Ready()
    {
        _currentHp = MaxHp;
        salasVencidas = new List<string>();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("atirar") && podeAtacar)
        {
            shoot();
        }
        var _velocity = Input.GetVector("MovEsquerda", "MovDireita", "MovCima", "MovBaixo");

        int mySpeed = Input.IsActionPressed("MovCorrer") ? (int)(Speed * RunMulti) : Speed;
        _velocity = _velocity.Normalized() * mySpeed;
        this.Velocity = _velocity * (float)delta;
        
        
        MoveAndSlide();
    }

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

    public void _hit(Node2D body)
    {
        if(!imortal && body is Inimigo inimigo)
        {
            GetNode<Timer>("imortal").Start();
            GD.Print("Estou imortal");
            TakeDamage(inimigo.Damage);
            imortal = true;
            GD.Print("O inimigo atacou o jogador!");
        }
        
    }

    public void _on_imortal_timeout()
    {
        imortal = false;
        GD.Print("Não estou imortal");
    }


}
