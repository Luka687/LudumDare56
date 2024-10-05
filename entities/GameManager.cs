using Godot;
using System;

public partial class GameManager : Node2D
{
	private Random rand;

	public override void _Ready()
	{	
		rand = new Random();
		for (int i = 0; i < 5; ++i) 
		{
			Peasant p = (Peasant)ResourceLoader.Load<PackedScene>("res://entities/peasant.tscn").Instantiate();
			p.GlobalPosition = new Vector2(rand.Next(20, 100) * 1.0f, rand.Next(20, 100) * 1.0f);
			AddChild(p);
		}
		Infected inf = (Infected)ResourceLoader.Load<PackedScene>("res://entities/infected.tscn").Instantiate();
		inf.GlobalPosition = new Vector2(rand.Next(20, 100) * 1.0f, rand.Next(20, 100) * 1.0f); 
		AddChild(inf);
	}

	public void _PhysicsProcess()
	{
		// Your physics processing code here
	}
}
