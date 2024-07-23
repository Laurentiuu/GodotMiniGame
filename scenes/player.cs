using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	private int SPEED = 600;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var floatDelta = (float)delta;

		var dir = Input.GetVector("left", "right", "up", "down");

		Velocity = dir * SPEED;

		MoveAndSlide();
		_RotateToMouse();

	}

	private void _RotateToMouse()
	{
		var mousePosition = GetGlobalMousePosition();

		var direction = mousePosition - GlobalPosition;

		var angle = direction.Angle();

		Rotation = angle + 45;
	}
}
