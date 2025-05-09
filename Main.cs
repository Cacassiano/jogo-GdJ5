using Godot;
using System;
using System.Collections.Generic;
using System.Runtime;

public partial class Main : Node2D
{
    [Export]
    public PackedScene PlayerScene;

    [Export]
    public PackedScene EnemyScene;

    [Export]
    public PackedScene InimigoRScene;
    public Player player;

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
        for (int i = 0; i<room.inimigoR.Length;i++)
        {
            GD.Print("Procurando no vetor");
            if(room.inimigoR[i] == id)
            {
                GetNode<SalaChao>(room.Name+"/"+room.Name).inimigoR[i] = 0;
                GD.Print("Achei e apaguei");
                return;
            }
        }
    }

    public void FimDeJogo()
    {
        GD.Print("Fim de jogo");
        GetTree().ChangeSceneToFile("res://fim.tscn");
    }

    
    public void _Warp(Marker2D mark)
    {
        player.GlobalPosition = mark.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetNode<Label>("MainCamera/Control/VBoxContainer/Label").Text = player._currentHp.ToString();
        if (player._currentHp <= 0)
        {
            GD.Print("Game Over");
            GetTree().ChangeSceneToFile("res://game_over.tscn");
        }
    }



    public override void _Ready()
    {
        player = (Player) PlayerScene.Instantiate();
        AddChild(player);
        player.GlobalPosition = GetNode<Marker2D>("Player/SpawnPoint").GlobalPosition;
        GetNode<Area2D>("SalaIni/Sala chao2").Name ="SalaPrincipal";
        GetNode<Area2D>("Sala2/Sala2").Name ="Sala2";
        GetNode<Area2D>("Sala3/Sala3").Name ="Sala3";
        GetNode<Area2D>("Sala4/Sala4").Name ="Sala4";
        GetNode<Area2D>("Sala6/Sala6").Name ="Sala6";
        GetNode<MainCamera>("MainCamera").player = player;
        GetNode<Area2D>("Sala5/Sala5").Name ="Sala5";
        GetNode<Area2D>("Sala1/Sala1").Name = "Sala1";

        player.ZIndex = 10;
        
    }
    
    public void _Timeout()
    {
        GD.Print("Timeout");
        FimDeJogo();
    }
    public void _SwapSala(SalaChao room)
    {
        try
        {
            GetNode<InimigoR>("Sala4/InimigoR").PlayerScene = null;
            GetNode<InimigoR>("Sala4/InimigoR3").PlayerScene = null;
            GetNode<InimigoR>("Sala4/InimigoR2").PlayerScene = null;
            

            GetNode<InimigoR>("Sala6/InimigoR").PlayerScene = null;
            GetNode<InimigoR>("Sala6/InimigoR3").PlayerScene = null;
            GetNode<InimigoR>("Sala6/InimigoR2").PlayerScene = null;
            GetNode<Inimigo>("Sala6/InimigoMeelee").PlayerScene = null;
        }
        catch (System.NullReferenceException e)
        {
            GD.Print("Erro ao trocar de sala");
            GD.Print(e);
        }
            
        for ( int i = 2; i<= 5;i++)
        {
            GetNode<Inimigo>("Sala6/InimigoMeelee"+i).PlayerScene = null;
        }
        if(GetNodeOrNull<InimigoR>("Sala5/InimigoR") != null)
        {
            GetNode<InimigoR>("Sala5/InimigoR").PlayerScene = null;
        }
        Inimigo[] enemy = new Inimigo[10];        

        if(room.Name == "Sala1")
        {
            for (int i =0 ;i<room.inimigoM.Length; i++)
            {
                if(room.inimigoM[i] == 1)
                {
                    GD.Print("Criandp inimigoR" + room.Name);
                    enemy[i] = (Inimigo) EnemyScene.Instantiate();
                    enemy[i]._SetPlayerScene(player);
                    enemy[i].sala = room.Name;
                    AddChild(enemy[i]);
                    enemy[i].GlobalPosition = GetNode<Marker2D>("Sala1/inimigo"+(i+1)).GlobalPosition;
                    GD.Print("Criando na posicao" + enemy[i].GlobalPosition);
                }              
            }
        }
        else if(room.Name == "Sala2" )
        {
            for (int i =0 ;i<room.inimigoM.Length; i++)
            {
                GD.Print("Criandp inimigoR" + room.Name);
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
        }
        else if(room.Name == "Sala3" )
        {
            GD.Print("Criandp inimigoR" + room.Name);
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
        } 
        else if(room.Name == "Sala4")
        {
            GetNode<InimigoR>("Sala4/InimigoR").PlayerScene = player;
            GetNode<InimigoR>("Sala4/InimigoR2").PlayerScene = player;
            GetNode<InimigoR>("Sala4/InimigoR3").PlayerScene = player;
            GetNode<InimigoR>("Sala4/InimigoR").PlayerScene = player;
            GetNode<InimigoR>("Sala4/InimigoR2").PlayerScene = player;
            GetNode<InimigoR>("Sala4/InimigoR3")._Ready();
            GetNode<InimigoR>("Sala4/InimigoR")._Ready();
            GetNode<InimigoR>("Sala4/InimigoR2")._Ready();
        }
        else if(room.Name == "Sala5")
        {
            GetNode<InimigoR>("Sala5/InimigoR").PlayerScene = player;
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
            
        }
        else if(room.Name == "Sala6")
        {
            player.amountS6 = 6;
            GetNode<InimigoR>("Sala6/InimigoR").PlayerScene = player;
            GetNode<InimigoR>("Sala6/InimigoR2").PlayerScene = player;
            GetNode<InimigoR>("Sala6/InimigoR3").PlayerScene = player;
            GetNode<Inimigo>("Sala6/InimigoMeelee").PlayerScene = player;
            for ( int i = 2; i<= 5;i++)
            {
                GetNode<Inimigo>("Sala6/InimigoMeelee"+i).PlayerScene = player;
            }
            GD.Print("Comecando timer");
            GetNode<Timer>("Sala6/Survive").Start();
        }
        
        
    }
}
