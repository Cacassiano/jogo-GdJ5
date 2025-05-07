using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene PlayerScene;

    [Export]
    public PackedScene EnemyScene;
    Player player;
    Inimigo enemy;
    public override void _Ready()
    {
        player = (Player) PlayerScene.Instantiate();
        AddChild(player);
        player.GlobalPosition = GetNode<Marker2D>("Player/SpawnPoint").GlobalPosition;
        GetNode<Area2D>("Chaos/Sala chao2").Name ="Sala2";
        GetNode<Area2D>("Sala1/Sala chao").Name = "Sala1";
        player.ZIndex = 10;
        enemy = (Inimigo) EnemyScene.Instantiate();
    }
    
    public void _SwapSala(Area2D room)
    {
        if(room.Name == "Sala1" && !player.salasVencidas.Contains(room.Name))
        {
            GD.Print(room.Name);
            
            enemy._SetPlayerScene(player);
            AddChild(enemy);
            enemy.GlobalPosition = GetNode<Marker2D>("Sala1/inimigoPoint1").GlobalPosition;
            // player.salasVencidas.Add(room.Name);
        }
        
    }
}
