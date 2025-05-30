using Godot;
using System;

public partial class Projetil : Area2D
{
    public Vector2 mousePos;
    public Vector2 direction;
    private RayCast2D ray;

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
        direction = (mousePos - GlobalPosition).Normalized();

        ray = GetNode<RayCast2D>("RayCast2D");
        ray.TargetPosition = direction * 20;
        ray.Enabled = true;
    }


    public override void _Process(double delta)
    {
        
        this.Position += direction * 1000 * (float)delta;
        if (ray.IsColliding())
        {
            var collider = ray.GetCollider();
            if (collider is TileMapLayer)
            {
                QueueFree();
            }
        }
    }


    public void _BateuEntidade(Node2D body)
    {
        if (body is Inimigo)
        {
            QueueFree();
        }
    }

    public void _SaiuTela()
    {
        QueueFree();
    }

}
