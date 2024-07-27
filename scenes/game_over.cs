using Godot;
using System;

public partial class game_over : Control
{
	[Export]
	private PackedScene levelScene = GD.Load<PackedScene>("res://scenes/level.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var score = GetNodeOrNull<Label>("CenterContainer/VBoxContainer/Label2");
		GD.Print("Score node:", Global.Score.ToString());

		score.Text = score.Text + Global.Score.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("shoot"))
		{
			CallDeferred(nameof(ChangeSceneDeferred));
		}
	}

	private void ChangeSceneDeferred()
	{
		GetTree().ChangeSceneToPacked(levelScene);
	}
}
