using Godot;
using System;
using Godot.Collections;

public partial class GameManager : Node2D
{
	private uint idCnt = 0;
	private Random rand;

	[Export]
	public TileMap cityMap;

	public override void _Ready()
	{	
		rand = new Random();
		//for (int i = 0; i < 10; ++i) 
		//{
			//if (i % 2 == 0)
			//{
				//Peasant p = (Peasant)ResourceLoader.Load<PackedScene>("res://entities/peasant.tscn").Instantiate();
				//p.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f);
				//p.setId(idCnt++);
				//AddChild(p);
				//p.Modulate = new Color(0.0f, 0.0f, 1.0f);
				//p.Scale = new Vector2(3.0f, 3.0f);
			//}
			//else 
			//{
				//Guard g = (Guard)ResourceLoader.Load<PackedScene>("res://entities/guard.tscn").Instantiate();
				//g.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f);
				//g.setId(idCnt++);
				//AddChild(g);
				//g.Modulate = new Color(1.0f, 1.0f, 0.0f);
				//g.Scale = new Vector2(3.0f, 3.0f);
			//}
		//}
		Infected inf = (Infected)ResourceLoader.Load<PackedScene>("res://entities/infected.tscn").Instantiate();
		inf.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f); 
		AddChild(inf);
		inf.setId(idCnt++);
		inf.Scale = new Vector2(3.0f, 3.0f);
	}
	
	public void infect(uint idToInfect)
	{
		Array<Node> children = GetChildren();
		for (int i = 0; i < children.Count; ++i)
		{
			if (children[i].Name == "Camera2D" || children[i].Name == "CityMap") continue;
			
			if (((Entity) children[i]).getId() == idToInfect)
			{
				Node2D oldNode = (Node2D) children[i];
				Infected newNode = (Infected)ResourceLoader.Load<PackedScene>("res://entities/infected.tscn").Instantiate();
				newNode.GlobalPosition = oldNode.GlobalPosition;
				newNode.setId(idToInfect);
				RemoveChild(oldNode);
				AddChild(newNode);
				break;
			}
		}
	}
}
