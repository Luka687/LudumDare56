using Godot;
using System;

public partial class Menu : Node2D
{
	private void _on_play_game_pressed()
	{
		PackedScene gameScene = (PackedScene)ResourceLoader.Load("res://game_manager.tscn");
		GetTree().ChangeSceneToPacked(gameScene);
	}
}
