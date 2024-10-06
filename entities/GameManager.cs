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
		
		// TODO: manually go through children and assign them IDs
		//Guard g = (Guard)ResourceLoader.Load<PackedScene>("res://entities/guard.tscn").Instantiate();
		//g.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f);
		//g.setId(idCnt++);
		//AddChild(g);
		//g.Scale = new Vector2(3.0f, 3.0f);
		//
		//
		//Peasant p1 = (Peasant)ResourceLoader.Load<PackedScene>("res://entities/peasant.tscn").Instantiate();
		//p1.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f);
		//p1.setId(idCnt++);
		//AddChild(p1);
		//p1.Scale = new Vector2(3.0f, 3.0f);
		//
		//Peasant p2 = (Peasant)ResourceLoader.Load<PackedScene>("res://entities/peasant.tscn").Instantiate();
		//p2.GlobalPosition = new Vector2(rand.Next(5, 300) * 1.0f, rand.Next(5, 300) * 1.0f);
		//p2.setId(idCnt++);
		//AddChild(p2);
		//p2.Scale = new Vector2(3.0f, 3.0f);
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
