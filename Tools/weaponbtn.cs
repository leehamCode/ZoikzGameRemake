using Godot;
using System;
using System.Diagnostics.Eventing.Reader;
using Zoikz.Tools;

public class weaponbtn : Node2D
{

	public WeaponEnum key { get; set; } = WeaponEnum.WEAPON_MGUN;

	//TheArea2D's shape
	private CollisionShape2D shape { get; set; }
	
	private Sprite _mgunbase { get; set; }

	private Sprite _gluebase { get; set; }

	private Sprite _Carnonbase { get; set; }

	private Sprite _dgunbase { get; set; }

	private Sprite _firebase { get; set; }

	private Sprite _elecbase { get; set; }

	private Sprite Yeabase { get; set; }

	private Sprite Nobase { get; set; }


	public Label label { get; set; }


	

	public override void _Ready()
	{
		_mgunbase = GetNode<Sprite>("mgunbase");
		_gluebase = GetNode<Sprite>("gluebase");
		_Carnonbase = GetNode<Sprite>("Carnonbase");
		_dgunbase = GetNode<Sprite>("dgunbase");
		_firebase = GetNode<Sprite>("firebase");
		_elecbase = GetNode<Sprite>("elecbase");
		label = GetNode<Label>("Label");
		Yeabase = GetNode<Sprite>("Yesbase");
		Nobase = GetNode<Sprite>("Nobase");
		shape = GetNode<Area2D>("clickarea").GetNode<CollisionShape2D>("click");

		switch (key)
		{
			case WeaponEnum.WEAPON_MGUN:
				{
					_mgunbase.Visible = true;_mgunbase.GetNode<Sprite>("mgun").Visible = true;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
					//label.Text = StaticNumbers.MGUN_PRICE+label.Text;
				}
				break;
			case WeaponEnum.WEAPON_GLUE:
				{
					_mgunbase.Visible = false; _mgunbase.GetNode<Sprite>("mgun").Visible = false;
					_gluebase.Visible = true; _gluebase.GetNode<Sprite>("gluegun").Visible = true;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
					//label.Text = StaticNumbers.GLUE_PRICE + label.Text;
				}
				break;
			case WeaponEnum.WEAPON_CARNON:
				{
					_mgunbase.Visible = false; _mgunbase.GetNode<Sprite>("mgun").Visible = false;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = true; _Carnonbase.GetNode<Sprite>("carnongun").Visible = true;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
					//label.Text = StaticNumbers.CARNON_PRICE + label.Text;
				}
				break;
			case WeaponEnum.WEAPON_DUN:
				{
					_mgunbase.Visible = false; _mgunbase.GetNode<Sprite>("mgun").Visible = false;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = true; _dgunbase.GetNode<Sprite>("dgun").Visible = true;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
					//label.Text = StaticNumbers.DGUN_PRICE + label.Text;
				}
				break;
			case WeaponEnum.WEAPON_FIRE:
				{
					_mgunbase.Visible = false; _mgunbase.GetNode<Sprite>("mgun").Visible = false;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = true; _firebase.GetNode<Sprite>("firegun").Visible = true;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
					//label.Text = StaticNumbers.FIRE_PRICE + label.Text;
				}
				break;
			case WeaponEnum.WEAPON_ELEC:
				{
					_mgunbase.Visible = false; _mgunbase.GetNode<Sprite>("mgun").Visible = false;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = true; _elecbase.GetNode<Sprite>("elecgun").Visible = true;
					//label.Text = StaticNumbers.ELEC_PRICE + label.Text;
				}
				break;
			default:
				break;
		}

	}

	/// <summary>
	/// Listening is player click this Sprite
	/// </summary>
	/// <param name="event"></param>
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == (int)ButtonList.Left && buttonEvent.Pressed)
		{

			Vector2 globalMousePosition = GetGlobalMousePosition();


			Vector2 localMousePosition = globalMousePosition;


			//GD.Print("Mouse clicked at local position: " + localMousePosition);

			//is Click Sprite
			if (IsPointInClickArea(localMousePosition))
			{
				GD.Print($@"Click the {key} Sprite");

				GameFootMenu gameFootMenu = GetParent().GetParent<GameFootMenu>();
				gameFootMenu.placeweapon.key = key;
				gameFootMenu.placeweapon._Ready();
				gameFootMenu.placeweapon.Visible = true;
				
			}

		}
	}

	public override void _PhysicsProcess(float delta)
	{
		switch (key)
		{
			case WeaponEnum.WEAPON_MGUN:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.MGUN_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true;Yeabase.Visible = false;

					}
				}
				break;
			case WeaponEnum.WEAPON_GLUE:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.GLUE_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true; Yeabase.Visible = false;

					}
				}
				break;
			case WeaponEnum.WEAPON_CARNON:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.CARNON_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true; Yeabase.Visible = false;
					}
				}
				break;
			case WeaponEnum.WEAPON_DUN:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.DGUN_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true; Yeabase.Visible = false;

					}
				}
				break;
			case WeaponEnum.WEAPON_FIRE:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.FIRE_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true; Yeabase.Visible = false;

					}
				}
				break;
			case WeaponEnum.WEAPON_ELEC:
				{
					if (StaticNumbers.CREDIT >= StaticNumbers.ELEC_PRICE)
					{
						Yeabase.Visible = true; Nobase.Visible = false;
					}
					else
					{
						Nobase.Visible = true; Yeabase.Visible = false;

					}
				}
				break;
			default:
				break;
		}
		
	}



	#region CustomerMethod

	/// <summary>
	///  Is Mouse (left) click theSprite
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public bool IsPointInClickArea(Vector2 vector)
	{
		if (shape == null || shape.Shape == null)
		{
			return false;
		}

		if (shape.Shape is RectangleShape2D rect)
		{
			Vector2 rectPosition = ToGlobal(shape.Position);

			Vector2 rectSize = rect.Extents;

			rectPosition.x = rectPosition.x - rectSize.x;
			rectPosition.y = rectPosition.y - rectSize.y;

			float minX = rectPosition.x;
			float maxX = rectPosition.x + 2 * rectSize.x;
			float minY = rectPosition.y;
			float maxY = rectPosition.y + 2 * rectSize.y;

			return vector.x >= minX && vector.x <= maxX && vector.y >= minY && vector.y <= maxY;
		}

		return false;
	}

	

	#endregion

}
