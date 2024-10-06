using Godot;
using System;
using Godot.Collections;

public partial class GameManager : Node2D
{
	private uint idCnt = 0;
	private Random rand;

	private Camera2D camera;
	private Label time_left;
	private Label game_time_left;
	private Timer gameTimeLeft;

	[Export]
	public TileMap cityMap;

	public override void _Ready()
	{	
		rand = new Random();
		camera = GetNode<Camera2D>("Camera2D");
		time_left = GetNode<Camera2D>("Camera2D").GetNode<Panel>("Panel").GetNode<Label>("TimeLeft");
		game_time_left = GetNode<Camera2D>("Camera2D").GetNode<Panel>("Panel").GetNode<Label>("GameTimeLeft");
		gameTimeLeft = GetNode<Timer>("Timer");
		GD.Print($"Camera not null? {camera != null}");
		
		Array<Node> children = GetChildren();
		uint id = 0;
		for (int i = 0; i < children.Count; ++i, ++id)
		{
			if (
				children[i].Name == "Camera2D" || 
				children[i].Name == "CityMap" || 
				children[i].Name == "Timer" ||
				children[i].Name.ToString().Contains("NoGoZone")
			) continue;
			
			((Entity)children[i]).setId(id);
		}
		
		gameTimeLeft.Start();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Array<Node> children = GetChildren();
		for (int i = 0; i < children.Count; ++i)
		{
			if (
				children[i].Name == "Camera2D" || 
				children[i].Name == "CityMap" || 
				children[i].Name == "Timer" ||
				children[i].Name.ToString().Contains("NoGoZone")
			) continue;
			
			if (children[i] is Infected)
			{
				camera.GlobalPosition = ((Infected)children[i]).GlobalPosition;
				time_left.Text = "Time until host dies: " + Math.Round(((Infected)children[i]).TimeRemaining()).ToString();
				game_time_left.Text = "Time until daybreak: " + Math.Round(gameTimeLeft.TimeLeft).ToString();
			}
		}
	}
	
	public void infect(uint idToInfect)
	{
		Array<Node> children = GetChildren();
		for (int i = 0; i < children.Count; ++i)
		{
			if (
				children[i].Name == "Camera2D" || 
				children[i].Name == "CityMap" || 
				children[i].Name == "Timer" ||
				children[i].Name.ToString().Contains("NoGoZone")
			) continue;
			
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
	
	public void loseState()
	{
		
	}
	
	//winState
	private void _on_timer_timeout()
	{
			
	}
}
