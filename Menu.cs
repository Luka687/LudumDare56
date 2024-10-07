using Godot;
using System;

public partial class Menu : Node2D
{
	private void _on_play_game_pressed()
	{
		PackedScene introScene = (PackedScene)ResourceLoader.Load("res://intro_scene.tscn");
		GetTree().ChangeSceneToPacked(introScene);
	}
}
