using Godot;
using System;

public partial class SalaChao : Area2D
{
    [Signal]
    public delegate void SwapRoomEventHandler(Area2D room);

    public override void _Ready()
    {
        GD.Print("Completed");
    }

    public void _PlayerEntered(Node2D body) 
    {
        EmitSignal(SignalName.SwapRoom, this);
        GD.Print("Tocandp no chao ");
    }
}
