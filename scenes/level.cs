using Godot;
using System;

public partial class level : Node2D
{
	// Load the scene
	private PackedScene meteorScene = GD.Load<PackedScene>("res://scenes/meteor.tscn");
	private PackedScene laserScene = GD.Load<PackedScene>("res://scenes/laser.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GetNode<player>("Player");
		player.Connect(nameof(player.LaserEventHandler), new Callable(this, nameof(_on_player_laser)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_meteor_timer_timeout()
	{
		// Create an instance
		var meteor = meteorScene.Instantiate();

		// Append the child to the scene
		GetNode("Meteors").AddChild(meteor);
	}

	private void _on_player_laser(Vector2 position, float rotation)
	{
		var laser = laserScene.Instantiate<laser>();
		GetNode("Lasers").AddChild(laser);

		laser.Initialize(position, rotation);
	}
}
