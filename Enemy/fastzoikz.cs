using Godot;
using System;
using Zoikz.Tools;

public class fastzoikz : PathFollow2D
{
	public TextureProgress progress { get; set; }

	public AnimatedSprite body { get; set; }

	public int THE_HP { get; set; } = StaticNumbers.FAST_ZOIKZ_HP;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		progress = GetNode<TextureProgress>("hp");
		progress.Value = THE_HP;
		body = GetNode<AnimatedSprite>("body");

	}

	public override void _PhysicsProcess(float delta)
	{
		if (this.UnitOffset > 1)
		{
			this.UnitOffset = 0;
		}
		else
		{
			this.UnitOffset += StaticNumbers.FAST_ZOIKZ_SPEED;
		}
	}
	
	private void play_destroy_then_quee()
	{
		
		if (body.Animation.Equals("destroy") && body.Playing)
		{
			StaticNumbers.CREDIT += StaticNumbers.FAST_ZOIKZ_CREDIT;
			this.QueueFree();
		}
	}
}



