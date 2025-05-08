using Godot;
using System;

public partial class Dinheiro : Area2D
{
    public Vector2 pPos;
    public Vector2 direction;
    public int Damage = 1;
    public void _BateuParede(Area2D area)
    {
        if (area.Name == "Parede")
        {
            QueueFree();
        }
    }
    public override void _Ready()
    {
        direction = (pPos - GlobalPosition).Normalized();
    }


    public override void _Process(double delta)
    {
        
        this.Position += direction * 1000 * (float)delta;
    }


    public void _BateuEntidade(Node2D body)
    {
        if (body is Player)
        {
            QueueFree();
        }
    }

    public void _SaiuTela()
    {
        QueueFree();
    }

}