using Godot;
using System;

public partial class Camera2d : Camera2D
{
    public void _SwapRoom(Area2D room)
    {
        GD.Print("Movendo a camera");
        Position = room.Position;
    }
}
