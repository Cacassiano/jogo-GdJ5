using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
   
    [Export]
    public float DistanciaMinimaParaMovimentar = 50f;
    
    [Export]
    public int Speed = 400;
    [Export]
    public float RunMulti = 2f;
    [Export]
    public Area2D PlayerScene{ get; set; }
    private NavigationAgent2D agent;
    public override void _Draw()
    {
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
    }
    public override void _PhysicsProcess(double delta)
    {
        agent.TargetPosition = PlayerScene.GlobalPosition;
        this.Velocity = GlobalPosition.DirectionTo(agent.GetNextPathPosition()) * Speed;

        MoveAndSlide(); 
    } 


}