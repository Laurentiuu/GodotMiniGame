using Godot;
using System;

public partial class meteor : Area2D
{
	private int speed = 200;
	private double directionX;
	private int rotationSpeed;

	private bool canColide = true;

	[Signal]
	public delegate void MeteorEventHandler(Vector2 position);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random random = new Random();

		//get random texture from meteors file
		string path = "res://assets/meteors/" + random.Next(1, 12).ToString() + ".png";
		Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
		Texture2D texture = GD.Load<Texture2D>(path);
		sprite.Texture = texture;

		var width = GetViewport().GetVisibleRect().Size[0];
		int randomX = random.Next(0, (int)width);
		int randomY = random.Next(-100, -50);
		Position = new Vector2(randomX, randomY);

		speed = random.Next(140, 400);
		directionX = (float)(random.NextDouble() * 2 - 1);
		rotationSpeed = random.Next(40, 100);

		if (!HasSignal(nameof(MeteorEventHandler)) && canColide)
		{
			AddUserSignal(nameof(MeteorEventHandler));
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var floatDelta = (float)delta;
		Position += new Vector2((float)directionX, 1) * speed * floatDelta;
		RotationDegrees += rotationSpeed * floatDelta;
	}

	private void _on_body_entered(Node2D body)
	{
		EmitSignal(nameof(MeteorEventHandler), Position);
	}

	private async void _on_area_entered(Area2D area)
	{
		area.QueueFree();

		GetNode<AudioStreamPlayer2D>("ExplosionSound").Play();
		GetNode<Sprite2D>("Sprite2D").Hide();
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		canColide = false;

		// Await the timeout signal of the timer
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");

		QueueFree();
	}
}
