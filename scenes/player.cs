using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	private int SPEED = 600;

	private Boolean canShoot = true;

	[Signal]
	public delegate void LaserEventHandler(Vector2 position);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Ensuring that the signal is registered and can be connected to
		if (!HasSignal(nameof(LaserEventHandler)))
		{
			AddUserSignal(nameof(LaserEventHandler));
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var floatDelta = (float)delta;

		var dir = Input.GetVector("left", "right", "up", "down");

		Velocity = dir * SPEED;

		MoveAndSlide();
		// _RotateToMouse();

		if (Input.IsActionJustPressed("shoot") && canShoot)
		{
			Node2D startPos = GetNode<Node2D>("LaserStartPosition");
			EmitSignal(nameof(LaserEventHandler), startPos.GlobalPosition);

			canShoot = false;
			GetNode<Timer>("LaserTimer").Start();
		}
	}

	// private void _RotateToMouse()
	// {
	// 	var mousePosition = GetGlobalMousePosition();

	// 	var direction = mousePosition - GlobalPosition;

	// 	var angle = direction.Angle();

	// 	Rotation = angle + 45;
	// }

	private void _on_laser_timer_timeout()
	{
		canShoot = true;
	}
}
