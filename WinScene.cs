using Godot;
using System;

public partial class WinScene : Node2D
{	
	private Label congratsLabel;
	private Label textLabel;
	private Button returnToGame;
	private Button returnToMenu;
	private float alpha = 0.0f;
	
	public override void _Ready()
	{
		congratsLabel = GetNode<Panel>("Panel").GetNode<Label>("CongratsLabel");
		textLabel = GetNode<Panel>("Panel").GetNode<Label>("TextLabel");
		returnToGame = GetNode<Panel>("Panel").GetNode<Button>("ReturnToGame");
		returnToMenu = GetNode<Panel>("Panel").GetNode<Button>("ReturnToMenu");
		
		congratsLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha); 
		textLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
		returnToGame.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
		returnToMenu.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
	}
	
	public override void _Process(double delta)
	{
		if (alpha < 1.0f)
		{
			alpha += 0.01f;
			congratsLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
			textLabel.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
			returnToGame.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
			returnToMenu.SelfModulate = new Color(1.0f, 1.0f, 1.0f, alpha);
		}
	}
	
	private void _on_return_to_game_pressed()
	{	
		PackedScene gameScene = (PackedScene)ResourceLoader.Load("res://game_manager.tscn");
		GetTree().ChangeSceneToPacked(gameScene);
	}
	
	private void _on_return_to_menu_pressed()
	{
		PackedScene menuScene = (PackedScene)ResourceLoader.Load("res://menu.tscn");
		GetTree().ChangeSceneToPacked(menuScene);
	}
}
