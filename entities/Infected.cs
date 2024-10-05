using Godot;
using System;

public partial class Infected : Entity
{	
	private float speed = 100.0f;
	public GameManager manager;
	
	[Export]
	public RayCast2D raycast; 
	
	public override void _Ready() 
	{
		manager = (GameManager)GetParent();
		GD.Print(manager != null);
		GD.Print(raycast != null);
	}
	
	public override void _PhysicsProcess(double delta) 
	{
		Vector2 vel = Velocity;
		Vector2 dir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down"); 
		
		inSight();
		
		raycast.GlobalPosition = this.GlobalPosition;
		raycast.TargetPosition = dir.Normalized()*25;
		
		vel = speed * dir;
		Velocity = vel;
		MoveAndSlide();	
	}
	
	public void inSight() 
	{
		if (raycast.IsColliding()) 
		{
			Object o = raycast.GetCollider();
			if (o is Peasant)
			{
				Entity e = (Entity) o;
				QueueFree();
				manager.infect(e.getId());
				
			}
		}
	}
}
