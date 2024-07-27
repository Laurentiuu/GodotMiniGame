using Godot;
using System;
public partial class level : Node2D
{
	// Load the scene
	private PackedScene meteorScene = GD.Load<PackedScene>("res://scenes/meteor.tscn");
	private PackedScene laserScene = GD.Load<PackedScene>("res://scenes/laser.tscn");

	private int health = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GetNode<player>("Player");
		player.Connect(nameof(player.LaserEventHandler), new Callable(this, nameof(_on_player_laser)));
		GetTree().CallGroup("ui", nameof(ui.set_health), health);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_meteor_timer_timeout()
	{
		// Create an instance
		Node meteorInstance = meteorScene.Instantiate();

		// Append the child to the scene
		GetNode("Meteors").AddChild(meteorInstance);

		var meteor = (meteor)meteorInstance;
		meteor.Connect(nameof(meteor.MeteorEventHandler), new Callable(this, nameof(_on__meteor_colision)));

	}

	private void _on__meteor_colision(Vector2 position)
	{
		health--;
		var ui = GetNode<ui>("UI");

		GetTree().CallGroup("ui", nameof(ui.set_health), health);
		GD.Print(ui);

		if (health == 0)
		{
			CallDeferred(nameof(ChangeSceneDeferred));
		}
	}

	private void ChangeSceneDeferred()
	{
		GetTree().ChangeSceneToFile("res://scenes/game_over.tscn");
	}


	private void _on_player_laser(Vector2 position)
	{
		var laser = laserScene.Instantiate<Node2D>();
		GetNode("Lasers").AddChild(laser);

		laser.Position = position;
	}
}
