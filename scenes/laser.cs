using Godot;
using System;

public partial class laser : Area2D
{
	[Export]
	private int speed = 600;

	private Vector2 _direction;

	public override void _Ready()
	{
	}

	public void Initialize(Vector2 position, float rotation)
	{
		GlobalPosition = position;
		Rotation = rotation + 45;

		// Set the direction based on the rotation
		_direction = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)).Normalized();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += _direction * speed * (float)delta;
	}
}
