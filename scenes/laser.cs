using Godot;
using System;

public partial class laser : Area2D
{

	[Export]
	private int speed = 600;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
		sprite.Scale = new Vector2(0, 0);
		var tween = CreateTween();
		tween.TweenProperty(sprite, "scale", new Vector2(1, 1), 0.2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Position += new Vector2(0, -1) * speed * (float)delta;

	}
}
