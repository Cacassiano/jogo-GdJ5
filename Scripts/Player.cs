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

    public override void _Ready()
    {
        _currentHp = MaxHp;
        salasVencidas = new List<string>();
    }

    public override void _PhysicsProcess(double delta)
    {
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

    private void Die()
    {
        GD.Print("O jogador morreu!");
        QueueFree();
    }

    public void _on_inimigo_player_hit(int Damage)
    {
        if(!imortal)
        {
            TakeDamage(Damage);
            imortal = true;
            GD.Print("O inimigo atacou o jogador!");
            GetNode<Timer>("imortal").Start();
            GD.Print("Estou imortal");
        }
        
    }

    public void _on_imortal_timeout()
    {
        imortal = false;
        GD.Print("Não estou imortal");
    }


}
