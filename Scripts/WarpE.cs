using Godot;
using System;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class WarpE : Area2D
{
    [Signal]
    public delegate void TrocaEventHandler(Marker2D mark);
    public Vector2 markerGbP;

    public override void _Ready()
    {
        markerGbP = GetNode<Marker2D>("Marker2D").GlobalPosition;
    }

    public void _Trocar(Node2D body)
    {
        GD.Print("Troca");
        if (body is Player)
        {
            EmitSignal(SignalName.Troca, GetNode<Marker2D>("Marker2D"));
        }
    }
}
