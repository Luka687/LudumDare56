using Godot;
using System;

public partial class Infected : Entity
{	
	private float speed = 100.0f;

	[Export]
	public RayCast2D raycast; 
	
	public override void _Ready() 
	{
		GD.Print(raycast != null);
	}
	
	public override void _PhysicsProcess(double delta) 
	{
		Vector2 vel = Velocity;
		Vector2 dir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down"); 
		
		raycast.GlobalPosition = this.GlobalPosition;
		raycast.TargetPosition = dir.Normalized() * 100;
		
		vel = speed * dir;
		Velocity = vel;
		MoveAndSlide();	
	}
}
