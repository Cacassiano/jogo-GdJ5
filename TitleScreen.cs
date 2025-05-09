using Godot;
using System;

public partial class TitleScreen : Control
{
    public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://Cenas/Main.tscn");
    }
   public void _on_creditos_pressed()
    {
        GD.Print("Creditos pressed");
    }
    public void _on_sair_pressed()
    {
        GetTree().Quit();
    }
}   
    