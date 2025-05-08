using Godot;
using System;
using System.Collections.Generic;

public partial class SalaChao : Area2D
{
    [Signal]
    public delegate void SwapRoomEventHandler(SalaChao room);
    [Signal]
    public delegate void MorreInimigoEventHandler(SalaChao room);

    [Export]
    public int[] inimigoM{get;set;}
    public override void _Ready()
    {
        GD.Print("Completed");
        Connect("body_exited", new Callable(this, nameof(_InimigoSaiu)));
    }

    public void _PlayerEntered(Node2D body) 
    {
        if (body is Player)
        {
            GD.Print("Player entrou na sala");
            EmitSignal(SignalName.SwapRoom, this);
        }   
    }

    public void _InimigoSaiu(Node2D body)
    {
        GD.Print("Ele saiuu");
        if(body is Inimigo)
        {
            EmitSignal(SignalName.MorreInimigo, this);
        }
        
    }
}
