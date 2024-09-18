using Godot;
using System;
using Zoikz.Tools;

public class UpdateOrSell : Node2D
{
	//labels
	private Label selllabel;

	private Label uplabel;


	//btns
	private TextureButton sellbtn;

	private TextureButton upbtn;

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		selllabel = GetNode<Sprite>("sell").GetNode<Label>("selllabel");
		uplabel = GetNode<Sprite>("update").GetNode<Label>("uplabel");
		sellbtn = GetNode<Sprite>("sell").GetNode<TextureButton>("sellbtn");
		upbtn = GetNode<Sprite>("update").GetNode<TextureButton>("upbtn");

		if (GetParent() is Main)
		{
			Main main = GetParent<Main>();
			selllabel.Text = $@" {StaticNumbers.MGUN_SELL_FEE} cr";
			uplabel.Text = $@" {StaticNumbers.MGUN_UPDATE_FEE} cr";

			if (StaticNumbers.CREDIT >= StaticNumbers.MGUN_UPDATE_FEE)
				upbtn.Disabled = false;
			else
				upbtn.Disabled = true;
		}


	}

	public override void _PhysicsProcess(float delta)
	{
		if(GetParent() as Main!=null)
		{
			if (StaticNumbers.CREDIT >= StaticNumbers.MGUN_UPDATE_FEE&&(GetParent() as Main).Level<3)
				upbtn.Disabled = false;
			else
				upbtn.Disabled = true;
		}
		
	}

	private void _on_sellbtn_button_up()
	{

        GD.Print("DEBUG:Sell btn on !");
		
		if(GetParent() is Main)
		{
			StaticNumbers.CREDIT += GetParent<Main>().Level * StaticNumbers.MGUN_SELL_FEE;
			GD.Print("You Sell The MGun!");
			GetParent().QueueFree();
		}
		else if(GetParent() is GlueGun) {

			StaticNumbers.CREDIT += GetParent<GlueGun>().Level * StaticNumbers.GLUE_SELL_FEE;
			GD.Print("You sell The GlunGun!");
		}
		
	}


	private void _on_upbtn_button_up()
	{
		GD.Print("Fuck");
		if(GetParent() as Main != null)
		{
			StaticNumbers.CREDIT -= StaticNumbers.MGUN_UPDATE_FEE;
			GD.Print("You Update TheMGun!");
			Main mGun = GetParent<Main>();
			mGun.Level++;
			if (mGun.Level == 2)
			{
				mGun.GetNode<AnimatedSprite>("Twolevel").Visible = true;
				mGun.GetNode<AnimatedSprite>("Onelevel").Visible = false;
				mGun.GetNode<AnimatedSprite>("Threelevel").Visible = false;
			}
			else if(mGun.Level == 3)
			{
				mGun.GetNode<AnimatedSprite>("Twolevel").Visible = false;
				mGun.GetNode<AnimatedSprite>("Onelevel").Visible = false;
				mGun.GetNode<AnimatedSprite>("Threelevel").Visible = true;
			}

			GetParent().GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("showUp");
			this.Visible = false;
		}
		else if(GetParent() as GlueGun != null)
		{
			StaticNumbers.CREDIT -= StaticNumbers.GLUE_UPDATE_FEE;
			GD.Print("You Update TheGlueGun!");
			GlueGun gluegun =  GetParent<GlueGun>();
			gluegun.Level++;
			if(gluegun.Level == 2)
			{
				gluegun.GetNode<AnimatedSprite>("Twolevel").Visible=true;
				gluegun.GetNode<AnimatedSprite>("Onelevel").Visible=false;
				gluegun.GetNode<AnimatedSprite>("Threelevel").Visible=false;
			}
			else if (gluegun.Level == 3)
			{
                gluegun.GetNode<AnimatedSprite>("Twolevel").Visible = false;
                gluegun.GetNode<AnimatedSprite>("Onelevel").Visible = false;
                gluegun.GetNode<AnimatedSprite>("Threelevel").Visible = true;

            }

			GetParent().GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("showUp");
			this.Visible = false;
		}
	}
}



