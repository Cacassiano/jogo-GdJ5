using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Coletavel : Area2D
{
    [Signal]
    public delegate void ColetadoEventHandler(Coletavel coletavel);

    public void _on_body_entered(Node2D body)
    {
        if (body is Player)
        {
            EmitSignal(SignalName.Coletado, this);
            QueueFree();
        }
    }

}
