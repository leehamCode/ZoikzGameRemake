using Godot;
using System;
using Zoikz.Tools;

public class lowzoikz : PathFollow2D
{
	public TextureProgress progress { get; set; }

	public AnimatedSprite body { get; set; }

	public AnimatedSprite affects { get; set; }

	public int THE_HP { get; set; } = StaticNumbers.LOW_ZOIKZ_HP;

	public float THE_SPEED { get; set; } = StaticNumbers.LOW_ZOIKZ_SPEED;

	/// <summary>
	/// Is Attack By GlueGun
	/// </summary>
	public bool IS_GLUED { get; set; } = false;


	public override void _Ready()
	{
		progress = GetNode<TextureProgress>("hp");
		progress.Value = THE_HP;
		body = GetNode<AnimatedSprite>("body");
		affects = GetNode<AnimatedSprite>("affects");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (this.UnitOffset > 1)
		{
			this.UnitOffset = 0;
		}
		else
		{
			this.UnitOffset += THE_SPEED;
		}
	}



	#region Signal Connection

	private void play_destroy_then_quee()
	{
		if (body.Animation.Equals("destroy")&&body.Playing)
		{
			StaticNumbers.CREDIT += StaticNumbers.LOW_ZOIKZ_CREDIT;
			this.QueueFree();
		}
		
	}

	#endregion
}






