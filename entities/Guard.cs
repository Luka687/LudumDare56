using Godot;
using System;
using System.Collections.Generic;

public partial class Guard : Entity
{
	enum GuardDir 
	{
		UP = 0,
		DOWN = 1,
		LEFT = 2,
		RIGHT = 3
	};
	
	private Random rand;
	private Timer timer;
	private AnimatedSprite2D animPlayer;
	private GuardDir gDir; 
	private float speed = 300.0f;
	
	public override void _Ready() 
	{
		rand = new Random();
		gDir = (GuardDir) rand.Next(0,3);
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
		
		switch (gDir)
		{
			case GuardDir.UP : 
				animPlayer.Play("up_walk");
				vel = speed * new Vector2(0.0f, -1.0f);
				break;
			
			case GuardDir.DOWN : 
				animPlayer.Play("down_walk");
				vel = speed * new Vector2(0.0f, 1.0f);
				break;
				
			case GuardDir.LEFT : 
				animPlayer.FlipH = true;
				animPlayer.Play("right_walk");
				vel = speed * new Vector2(-1.0f, 0.0f);
				break;
			
			case GuardDir.RIGHT : 
				animPlayer.FlipH = false;
				animPlayer.Play("right_walk");
				vel = speed * new Vector2(1.0f, 0.0f);
				break;
		}			
		
		Velocity = vel;
		MoveAndSlide();
	}

	private void _on_light_area_body_entered(Node2D body)
	{
		if (body is Infected)
		{
			((Infected) body).kill();
		}
	}

	private void _on_timer_timeout()
	{
		gDir = (GuardDir) rand.Next(0,3);
		timer.WaitTime = rand.Next(5, 10);
		timer.Start();
	}	

	private void _on_area_2d_body_entered(Node2D body)
	{
			switch (gDir)
				{
					case GuardDir.UP : 
						gDir = GuardDir.DOWN;
						break;
					
					case GuardDir.DOWN : 
						gDir = GuardDir.UP;
						break;
					
					case GuardDir.LEFT : 
						gDir = GuardDir.RIGHT;
						break;
					
					case GuardDir.RIGHT : 
						gDir = GuardDir.LEFT;
						break; 
				}
	}
}
