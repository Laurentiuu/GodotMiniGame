using Godot;
using System;

public partial class ui : CanvasLayer
{
	public static string imgPath = "res://assets/player.png";

	private int timeElapsed = 0;

	public override void _Ready()
	{

	}
	public void set_health(int amount)
	{
		var hBoxContainer = GetNodeOrNull<HBoxContainer>("MarginContainer/HBoxContainer");
		if (hBoxContainer == null)
		{
			GD.PrintErr("HBoxContainer node not found");
			return;
		}

		foreach (var child in hBoxContainer.GetChildren())
		{
			child.QueueFree();
		}

		for (int i = 0; i < amount; i++)
		{
			var textureRect = new TextureRect();
			Texture2D texture = GD.Load<Texture2D>(imgPath);
			textureRect.Texture = texture;
			hBoxContainer.AddChild(textureRect);
			textureRect.StretchMode = TextureRect.StretchModeEnum.Keep;
		}
	}

	private void _on_score_timer_timeout()
	{
		timeElapsed++;

		var count = GetNode<Label>("Label");
		count.Text = timeElapsed.ToString();

		Global.Score = timeElapsed;

	}

}
