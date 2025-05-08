using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    public PackedScene PlayerScene;

    [Export]
    public PackedScene EnemyScene;

    [Export]
    public PackedScene InimigoRScene;
    Player player;

    public void _InimigoMorre(SalaChao room, int id)
    {
        GD.Print("inimigo saiu da sala");
        for (int i = 0; i<room.inimigoM.Length;i++)
        {
            GD.Print("Procurando no vetor");
            if(room.inimigoM[i] == id)
            {
                GetNode<SalaChao>(room.Name+"/"+room.Name).inimigoM[i] = 0;
                GD.Print("Achei e apaguei");
                return;
            }
        }
        for (int i = 0; i<room.inimigoM.Length;i++)
        {
            GD.Print("Procurando no vetor");
            if(room.inimigoR[i] == id)
            {
                GetNode<SalaChao>(room.Name+"/"+room.Name).inimigoM[i] = 0;
                GD.Print("Achei e apaguei");
                return;
            }
        }
    }

    
    public void _Warp(Marker2D mark)
    {
        player.GlobalPosition = mark.GlobalPosition;
    }



    public override void _Ready()
    {
        player = (Player) PlayerScene.Instantiate();
        AddChild(player);
        player.GlobalPosition = GetNode<Marker2D>("Player/SpawnPoint").GlobalPosition;
        GetNode<Area2D>("SalaIni/Sala chao2").Name ="SalaPrincipal";
        GetNode<Area2D>("Sala2/Sala2").Name ="Sala2";
        GetNode<Area2D>("Sala3/Sala3").Name ="Sala3";
        GetNode<Area2D>("Sala1/Sala1").Name = "Sala1";
        player.ZIndex = 10;
        
    }
    
    public void _SwapSala(SalaChao room)
    {
        Inimigo[] enemy = new Inimigo[10];
        InimigoR[] enemyR = new InimigoR[10];
        if(room.Name == "Sala1")
        {
            for (int i =0 ;i<room.inimigoM.Length; i++)
            {
                if(room.inimigoM[i] == 1)
                {
                    enemy[i] = (Inimigo) EnemyScene.Instantiate();
                    enemy[i]._SetPlayerScene(player);
                    enemy[i].sala = room.Name;
                    AddChild(enemy[i]);
                    enemy[i].GlobalPosition = GetNode<Marker2D>("Sala1/inimigo"+(i+1)).GlobalPosition;
                }              
            }
            for (int i =0 ;i<room.inimigoR.Length; i++)
            {
                if(room.inimigoM[i] == 2)
                {
                    enemyR[i] = (InimigoR) InimigoRScene.Instantiate();
                    enemyR[i]._SetPlayerScene(player);
                    enemyR[i].sala = room.Name;
                    enemyR[i].Speed = (int)Random.Shared.NextInt64(400, 575);
                    AddChild(enemyR[i]);
                    enemyR[i].GlobalPosition = GetNode<Marker2D>("Sala1/inimigoR"+(i+1)).GlobalPosition;
                }

            }
        }
        else if(room.Name == "Sala2" )
        {
            for (int i =0 ;i<room.inimigoM.Length; i++)
            {
                if(room.inimigoM[i] == 1)
                {
                    enemy[i] = (Inimigo) EnemyScene.Instantiate();
                    enemy[i]._SetPlayerScene(player);
                    enemy[i].sala = room.Name;
                    AddChild(enemy[i]);
                    enemy[i].Speed = (int)Random.Shared.NextInt64(400, 575);
                    enemy[i].GlobalPosition = GetNode<Marker2D>("Sala2/inimigo"+(i+1)).GlobalPosition;
                }
                
            }
            for (int i =0 ;i<room.inimigoR.Length; i++)
            {
                if(room.inimigoM[i] == 2)
                {
                    enemyR[i] = (InimigoR) InimigoRScene.Instantiate();
                    enemyR[i]._SetPlayerScene(player);
                    enemyR[i].sala = room.Name;
                    enemyR[i].Speed = (int)Random.Shared.NextInt64(400, 575);
                    AddChild(enemyR[i]);
                    enemyR[i].GlobalPosition = GetNode<Marker2D>("Sala2/inimigoR"+(i+1)).GlobalPosition;
                }

            }
        }
        else if(room.Name == "Sala3" )
        {
            for (int i =0 ;i<room.inimigoM.Length; i++)
            {
                if(room.inimigoM[i] == 1)
                {
                    enemy[i] = (Inimigo) EnemyScene.Instantiate();
                    enemy[i]._SetPlayerScene(player);
                    enemy[i].sala = room.Name;
                    AddChild(enemy[i]);
                    enemy[i].Speed = (int)Random.Shared.NextInt64(400, 575);
                    enemy[i].GlobalPosition = GetNode<Marker2D>(room.Name+"/inimigo"+(i+1)).GlobalPosition;
                }
                
            }
            for (int i =0 ;i<room.inimigoR.Length; i++)
            {
                if(room.inimigoM[i] == 2)
                {
                    enemyR[i] = (InimigoR) InimigoRScene.Instantiate();
                    enemyR[i]._SetPlayerScene(player);
                    enemyR[i].sala = room.Name;
                    enemyR[i].Speed = (int)Random.Shared.NextInt64(400, 575);
                    AddChild(enemyR[i]);
                    enemyR[i].GlobalPosition = GetNode<Marker2D>(room.Name+"/inimigoR"+(i+1)).GlobalPosition;
                }
            }
        }
        
    }
}
