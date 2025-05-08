using Godot;
using System;
using System.Collections.Generic;

public partial class SalaChao : Area2D
{
    [Signal]
    public delegate void SwapRoomEventHandler(SalaChao room);
    [Signal]
    public delegate void MorreInimigoEventHandler(SalaChao room, int id);

    [Export]
    public int[] inimigoM = new int[10];

    [Export]
    public int[] inimigoR = new int[10];
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
        if(body is Inimigo inimigo)
        {
            GD.Print("Inimigo morreu");
            if (inimigo.vida <= 0)
            {
                GD.Print("Inimigo morreu");
                EmitSignal(SignalName.MorreInimigo, this, inimigo.id);
            }  
        } 
        else if( body is InimigoR inimigoR)
        {
            GD.Print("InimigoR morreu");
            if (inimigoR.vida <= 0)
            {
                GD.Print("InimigoR morreu");
                EmitSignal(SignalName.MorreInimigo, this, inimigoR.id);
            }
        }
    }
}
