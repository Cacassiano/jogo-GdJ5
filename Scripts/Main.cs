using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene PlayerScene;

    [Export]
    public PackedScene EnemyScene;
    Player player;
    public override void _Ready()
    {
        player = (Player) PlayerScene.Instantiate();
        AddChild(player);
        player.GlobalPosition = GetNode<Marker2D>("Player/SpawnPoint").GlobalPosition;
        GetNode<Area2D>("SalaIni/Sala chao2").Name ="SalaPrincipal";
        GetNode<Area2D>("Sala2/Sala chao").Name ="Sala2";
        GetNode<Area2D>("Sala1/Sala chao").Name = "Sala1";
        player.ZIndex = 10;
        
    }
    
    public void _SwapSala(Area2D room)
    {
        if(room.Name == "Sala1" && !player.salasVencidas.Contains(room.Name))
        {
            GD.Print();
            var enemy = (Inimigo) EnemyScene.Instantiate();
            enemy._SetPlayerScene(player);
            enemy.sala = room.Name;
            AddChild(enemy);
            enemy.GlobalPosition = GetNode<Marker2D>("Sala1/inimigoPoint1").GlobalPosition;
            player.salasVencidas.Add(room.Name);
        }
        else if(room.Name == "Sala2" && !player.salasVencidas.Contains(room.Name))
        {
            GD.Print();
            var enemy = (Inimigo) EnemyScene.Instantiate();
            enemy._SetPlayerScene(player);
            enemy.sala = room.Name;
            AddChild(enemy);
            enemy.GlobalPosition = GetNode<Marker2D>("Sala2/inimigo1").GlobalPosition;
            player.salasVencidas.Add(room.Name);
            
            enemy = (Inimigo) EnemyScene.Instantiate();
            enemy._SetPlayerScene(player);
            enemy.sala = room.Name;
            AddChild(enemy);
            enemy.GlobalPosition = GetNode<Marker2D>("Sala2/inimigo2").GlobalPosition;
        }
        
    }
}
