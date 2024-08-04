using Godot;
using System;
using Zoikz.Tools;

public class GameFootMenu : CanvasLayer
{

	private weaponbtn btn1;

	private weaponbtn btn2;

	private weaponbtn btn3;

	private weaponbtn btn4;

	private weaponbtn btn5;

	private weaponbtn btn6;

	private Label credit_label;
	private Label lifes_label;
	public Label wave_label { get;set; }

	public PlaceWeapon placeweapon;


	public override void _Ready()
	{
		credit_label = GetNode<Sprite>("credits").GetNode<Label>("Label");
		lifes_label = GetNode<Sprite>("lifes").GetNode<Label>("Label");
		wave_label = GetNode<Sprite>("waves").GetNode<Label>("Label");
		btn1 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn1");
		btn2 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn2");
		btn3 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn3");
		btn4 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn4");
		btn5 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn5");
		btn6 = GetNode<Node2D>("btns").GetNode<weaponbtn>("btn6");
		placeweapon = GetNode<PlaceWeapon>("placeweapon");

		btn1.key = Zoikz.Tools.WeaponEnum.WEAPON_MGUN;		btn1.label.Text = StaticNumbers.MGUN_PRICE + btn1.label.Text;
		btn2.key = Zoikz.Tools.WeaponEnum.WEAPON_GLUE;		btn2.label.Text = StaticNumbers.GLUE_PRICE + btn2.label.Text;
		btn3.key = Zoikz.Tools.WeaponEnum.WEAPON_CARNON;	btn3.label.Text = StaticNumbers.CARNON_PRICE + btn3.label.Text;
		btn4.key = Zoikz.Tools.WeaponEnum.WEAPON_DUN;		btn4.label.Text = StaticNumbers.DGUN_PRICE + btn4.label.Text;
		btn5.key = Zoikz.Tools.WeaponEnum.WEAPON_FIRE;		btn5.label.Text = StaticNumbers.FIRE_PRICE + btn5.label.Text;
		btn6.key = Zoikz.Tools.WeaponEnum.WEAPON_ELEC;		btn6.label.Text = StaticNumbers.ELEC_PRICE + btn6.label.Text;
		btn1._Ready(); btn2._Ready(); btn3._Ready(); btn4._Ready(); btn5._Ready(); btn6._Ready();

	}

	public override void _PhysicsProcess(float delta)
	{
		credit_label.Text = StaticNumbers.CREDIT.ToString();
		lifes_label.Text = StaticNumbers.LIVES.ToString();
		/*if (StaticNumbers.CURRENT_LEVEL == 3)
			wave_label.Text = StaticNumbers.LEVEL3_WAVE.ToString();
		else if (StaticNumbers.CURRENT_LEVEL == 2)
			wave_label.Text = StaticNumbers.LEVEL2_WAVE.ToString();
		else if (StaticNumbers.CURRENT_LEVEL == 1)
			wave_label.Text = StaticNumbers.LEVEL1_WAVE.ToString();*/
	}
}
