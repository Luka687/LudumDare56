using Godot;
using System;

public partial class IntroScene : Node2D
{
	private Label textLabel;
	private Timer timer;
	private float alpha = 0.0f;
	
	public override void _Ready()
	{
		textLabel = GetNode<Panel>("Panel").GetNode<Label>("TextLabel");
		textLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
		timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	public override void _Process(double delta)
	{	
		if (alpha < 1.0f)
		{
			alpha += 0.01f;
			textLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
		}
	}
	
	private void _on_timer_timeout()
	{
		PackedScene gameScene = (PackedScene)ResourceLoader.Load("res://game_manager.tscn");
		GetTree().ChangeSceneToPacked(gameScene);
	}

}



