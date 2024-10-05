using Godot;
using System;

public partial class Guard : Entity
{
	public override void _Ready() 
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		
	}

	private void _on_light_area_body_entered(Node2D body)
	{
		if (body is Infected){
			((Infected) body).kill();
		}
	}
	
}
