using Godot;
using System;

public partial class Peasant : Entity
{	
	enum PeasantDir 
	{
		UP = 0,
		DOWN = 1,
		LEFT = 2,
		RIGHT = 3
	};
	
	private Random rand;
	private Timer timer;
	private AnimatedSprite2D animPlayer;
	private PeasantDir pDir; 
	private float speed = 300.0f;
	
	public override void _Ready() 
	{
		rand = new Random();
		//pDir = (PeasantDir) rand.Next(0,3);
		pDir = PeasantDir.RIGHT;
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = rand.Next(5, 10);
		animPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print($"Timer not null? {timer != null}");
		GD.Print($"Animation player not null? {animPlayer != null}");	
		timer.Start();
	}
	
	public override void _PhysicsProcess(double delta)
	{	
		Vector2 vel = Velocity;
		
		switch (pDir)
		{
			case PeasantDir.UP : 
				animPlayer.Play("up_walk");
				vel = speed * new Vector2(0.0f, -1.0f);
				break;
			
			case PeasantDir.DOWN : 
				animPlayer.Play("down_walk");
				vel = speed * new Vector2(0.0f, 1.0f);
				break;
				
			case PeasantDir.LEFT : 
				animPlayer.FlipH = true;
				animPlayer.Play("right_walk");
				vel = speed * new Vector2(-1.0f, 0.0f);
				break;
			
			case PeasantDir.RIGHT : 
				animPlayer.FlipH = false;
				animPlayer.Play("right_walk");
				vel = speed * new Vector2(1.0f, 0.0f);
				break;
		}			
		
		Velocity = vel;
		MoveAndSlide();
	}

	private void _on_timer_timeout()
	{
		pDir = (PeasantDir) rand.Next(0,3);
		timer.WaitTime = rand.Next(5, 10);
		timer.Start();
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (!(body is Infected))
		{
			GD.Print("A hit");
			if (pDir == PeasantDir.UP) pDir = PeasantDir.DOWN;
			else if (pDir == PeasantDir.DOWN) pDir = PeasantDir.UP;
			else if (pDir == PeasantDir.LEFT) pDir = PeasantDir.RIGHT;
			else pDir = PeasantDir.LEFT;
		}
	}
}
