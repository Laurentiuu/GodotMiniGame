using Godot;
using System;

public partial class level : Node2D
{
	// load the scene
	private PackedScene meteorScene = GD.Load<PackedScene>("res://scenes/meteor.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_meteor_timer_timeout()
	{
		// create an instance
		var meteor = meteorScene.Instantiate();

		//append the child to the scene
		GetNode("Meteors").AddChild(meteor);
	}
}
