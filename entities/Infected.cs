using Godot;
using System;
using System.Collections.Generic;

public partial class Infected : Entity
{	
	enum SpriteDir 
	{
		UP, 
		DOWN,
		LEFT,
		RIGHT
	};
	
	private Dictionary<Vector2, SpriteDir> dict;
	private float speed = 700.0f;
	private Area2D area2D;
	private uint? idToInfect;
	
	public GameManager manager;
	public Timer timer;
	public RayCast2D raycast; 
	public AnimatedSprite2D animPlayer;
	
	public override void _Ready() 
	{
		dict = new Dictionary<Vector2, SpriteDir>();
		dict[new Vector2(1.0f, 0.0f)] = SpriteDir.RIGHT;
		dict[new Vector2(-1.0f, 0.0f)] = SpriteDir.LEFT;
		dict[new Vector2(0.0f, 1.0f)] = SpriteDir.DOWN;
		dict[new Vector2(0.0f, -1.0f)] = SpriteDir.UP;
		
		manager = (GameManager)GetParent();
		timer = GetNode<Timer>("Timer");
		animPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		timer.Start();
		GD.Print($"Timer null? {timer != null}");
		GD.Print($"Manager null? {manager != null}");
		GD.Print($"Raycast null? {raycast != null}");
		GD.Print($"Animation player null? {animPlayer != null}");
	}
	
	public override void _PhysicsProcess(double delta) 
	{
		Vector2 vel = Velocity;
		Vector2 dir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down"); 
		
		SpriteDir s;
		
		if(dict.TryGetValue(dir, out s)){
			switch (s){
				case SpriteDir.UP:
					animPlayer.Play("up_walk");
					break;
				case SpriteDir.DOWN:
					animPlayer.Play("down_walk");
					break;
				case SpriteDir.LEFT:
					animPlayer.FlipH = true;
					animPlayer.Play("right_walk");
					break;
				case SpriteDir.RIGHT:
					animPlayer.FlipH = false;
					animPlayer.Play("right_walk");
					break;
			}			
		}
		
		if (Input.IsKeyPressed(Key.E)){
			GD.Print("Can't Infect");
			if(idToInfect != null){
				GD.Print("INFECT");
				manager.infect(idToInfect);
				QueueFree();
			}
			else{
				GD.Print("Can't Infect");
			}
		}
		
		vel = speed * dir;
		Velocity = vel;
		MoveAndSlide();	
	}
	
	public void kill(){
		QueueFree();
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
		manager.loseState();
	}
	
	public double TimeRemaining()
	{
		return timer.TimeLeft;
	}
	
	public void inInfectRange(uint? id){
		idToInfect = id;
	}

}
