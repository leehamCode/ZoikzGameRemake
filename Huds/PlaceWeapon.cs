using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Zoikz.Tools;

public class PlaceWeapon : Node2D
{

	public WeaponEnum key { get; set; } = WeaponEnum.WEAPON_MGUN;

	private Sprite _mgunbase { get; set; }

	private Sprite _gluebase { get; set; }

	private Sprite _Carnonbase { get; set; }

	private Sprite _dgunbase { get; set; }

	private Sprite _firebase { get; set; }

	private Sprite _elecbase { get; set; }

	private Sprite _warning { get; set; }

	private PathTest pathTest { get; set; }



	public override void _Input(InputEvent @event)
	{
		/*if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == (int)ButtonList.Left && buttonEvent.Pressed)
		{

			Vector2 globalMousePosition = GetGlobalMousePosition();


			Vector2 localMousePosition = globalMousePosition;


			//GD.Print("Mouse clicked at local position: " + localMousePosition);

			//is Click Sprite
			if (IsPointInClickArea(localMousePosition))
			{
				GD.Print($@"Click the {key} Sprite");

				//To place the Select weapon
				PackedScene hover_pan = GD.Load<PackedScene>("res://Huds/PlaceWeapon.tscn");

				PlaceWeapon node = (PlaceWeapon)hover_pan.Instance();

				GetParent().GetParent().AddChild(node);
			}

		}*/



		//Right Click Cannel
		if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == (int)ButtonList.Right && buttonEvent.Pressed && _warning.Visible)
		{
			Visible = false;
		}

		//Left Click Confirm
		if (@event is InputEventMouseButton buttonEvent2 && buttonEvent2.ButtonIndex == (int)ButtonList.Left && buttonEvent2.Pressed && !_warning.Visible && Visible)
		{
			Vector2 globalMousePosition = GetGlobalMousePosition();

			if (globalMousePosition.y < 384 && globalMousePosition.x < 800)
			{
				switch (key)
				{
					case WeaponEnum.WEAPON_MGUN:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.MGUN_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.MGUN_PRICE;
								CollisionShape2D MouseInArea = pathTest.shapes.Where(it => IsPointInRectArea(globalMousePosition, it)).FirstOrDefault();
								Vector2 AreaglobalPosition = MouseInArea.Position;

								RectangleShape2D rect = (RectangleShape2D)MouseInArea.Shape;
								Vector2 extents = rect.Extents;

								float Minx = AreaglobalPosition.x - extents.x;
								float Miny = AreaglobalPosition.y - extents.y;
								float Maxx = Minx + 2 * extents.x;
								float Maxy = Miny + 2 * extents.y;

								Vector2[] point = FindPlaceWeaponPoint(Minx, Miny, Maxx, Maxy);

								//GD.Print($@"DEBG:minx:{Minx},miny:{Miny},maxx:{Maxx},maxy:{Maxy}");

								foreach(var p in point)
								{
									//GD.Print("findpoints"+p.x+":||"+p.y);
								}

								Vector2 real = FindMostClostFromPoints(globalMousePosition, point);

								if (!pathTest.mains.Any(it => it.Position == real))
								{
									PackedScene mgunscence = GD.Load<PackedScene>("res://Huds/MGun.tscn");

									Main node = (Main)mgunscence.Instance();

									node.Position = real;

									pathTest.mains.Add(node);
									pathTest.AddChild(node);

									_warning.Visible = true;

									return;
								}

								
							}
						}
						break;
					case WeaponEnum.WEAPON_GLUE:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.GLUE_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.GLUE_PRICE;

								//Temp Used MGun,Need Torewrite More weapon
								CollisionShape2D MouseInArea = pathTest.shapes.Where(it => IsPointInRectArea(globalMousePosition, it)).FirstOrDefault();
								Vector2 AreaglobalPosition = MouseInArea.Position;

								RectangleShape2D rect = (RectangleShape2D)MouseInArea.Shape;
								Vector2 extents = rect.Extents;

								float Minx = AreaglobalPosition.x - extents.x;
								float Miny = AreaglobalPosition.y - extents.y;
								float Maxx = Minx + 2 * extents.x;
								float Maxy = Miny + 2 * extents.y;

								Vector2[] point = FindPlaceWeaponPoint(Minx, Miny, Maxx, Maxy);

								//GD.Print($@"DEBG:minx:{Minx},miny:{Miny},maxx:{Maxx},maxy:{Maxy}");

								foreach (var p in point)
								{
									//GD.Print("findpoints" + p.x + ":||" + p.y);
								}

								Vector2 real = FindMostClostFromPoints(globalMousePosition, point);

								PackedScene mgunscence = GD.Load<PackedScene>("res://Huds/GlueGun.tscn");

								GlueGun node = (GlueGun)mgunscence.Instance();

								node.Position = real;

								pathTest.AddChild(node);
							}
						}
						break;
					case WeaponEnum.WEAPON_CARNON:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.CARNON_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.CARNON_PRICE;
							}
						}
						break;
					case WeaponEnum.WEAPON_DUN:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.DGUN_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.DGUN_PRICE;
							}
						}
						break;
					case WeaponEnum.WEAPON_FIRE:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.FIRE_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.FIRE_PRICE;
							}
						}
						break;
					case WeaponEnum.WEAPON_ELEC:
						{
							if (StaticNumbers.CREDIT >= StaticNumbers.ELEC_PRICE)
							{
								StaticNumbers.CREDIT -= StaticNumbers.ELEC_PRICE;
							}
						}
						break;
					default:
						break;
				}

				Visible = false;
			}


		}

	}

	public override void _Ready()
	{
		_mgunbase = GetNode<Sprite>("mgunbase");
		_gluebase = GetNode<Sprite>("gluebase");
		_Carnonbase = GetNode<Sprite>("Carnonbase");
		_dgunbase = GetNode<Sprite>("dgunbase");
		_firebase = GetNode<Sprite>("firebase");
		_elecbase = GetNode<Sprite>("elecbase");

		_warning = GetNode<Sprite>("warning");

		pathTest = GetParent().GetParent<PathTest>();

		switch (key)
		{
			case WeaponEnum.WEAPON_MGUN:
				{
					_mgunbase.Visible = true; _mgunbase.GetNode<Sprite>("mgun").Visible = true;
					_gluebase.Visible = false; _gluebase.GetNode<Sprite>("gluegun").Visible = false;
					_Carnonbase.Visible = false; _Carnonbase.GetNode<Sprite>("carnongun").Visible = false;
					_dgunbase.Visible = false; _dgunbase.GetNode<Sprite>("dgun").Visible = false;
					_firebase.Visible = false; _firebase.GetNode<Sprite>("firegun").Visible = false;
					_elecbase.Visible = false; _elecbase.GetNode<Sprite>("elecgun").Visible = false;
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
				}
				break;
			default:
				break;
		}

	}


	public override void _PhysicsProcess(float delta)
	{
		Vector2 mouse_position = GetGlobalMousePosition();
		this.Position = mouse_position;


		bool flag = pathTest.shapes.Any(it => IsPointInRectArea(mouse_position, it));

		//GD.Print($@"({mouse_position.x},{mouse_position.y}),|{flagOne},{flagTwo}");

		if (flag)
			_warning.Visible = false;
		else
			_warning.Visible = true;


	}

	#region CustomMethod

	//Is Point In a plogy
	/*	private bool IsPointInPolygon(Vector2 point, Vector2[] vertices)
		{
			int j = vertices.Length - 1;
			bool oddNodes = false;

			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[i].y < point.y && vertices[j].y >= point.y || vertices[j].y < point.y && vertices[i].y >= point.y)
				{
					if (vertices[i].x + (point.y - vertices[i].y) / (vertices[j].y - vertices[i].y) * (vertices[j].x - vertices[i].x) < point.x)
					{
						oddNodes = !oddNodes;
					}
				}

				j = i;
			}

			return oddNodes;
			CollisionPolygon2D shape = new CollisionPolygon2D();

		}*/

	public bool IsPointInRectArea(Vector2 vector, CollisionShape2D shape)
	{
		if (shape == null || shape.Shape == null)
		{
			return false;
		}

		if (shape.Shape is RectangleShape2D rect)
		{
			Vector2 rectPosition = shape.Position;

			Vector2 rectSize = rect.Extents;

			//GD.Print($@"FUCKYOU:({rectPosition.x},{rectPosition.y},mouse:{vector.x},{vector.y})");

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


	/// <summary>
	/// Find Weapon Place Point
	/// </summary>
	/// <param name="minx"></param>
	/// <param name="miny"></param>
	/// <param name="maxx"></param>
	/// <param name="maxy"></param>
	/// <returns></returns>
	public Vector2[] FindPlaceWeaponPoint(float minx, float miny, float maxx, float maxy)
	{
		if (minx == 0 && miny == 0 && maxx == 0 && maxy == 0)
			return null;

		List<TempCount> xpoints = new List<TempCount>();
		List<TempCount> ypoints = new List<TempCount>();

		int COUNTX = 1;
		while (minx < maxx)
		{
			xpoints.Add(new TempCount() { value = minx + 16,Count = COUNTX});
			minx += 16;COUNTX++;
		}

		int COUNTY = 1;
		while(miny < maxy)
		{
			ypoints.Add(new TempCount() { value = miny + 16,Count = COUNTY});
			miny += 16;
		}

		xpoints.RemoveAll(it => (it.Count % 2) == 0);
		ypoints.RemoveAll(it => (it.Count % 2) == 0);

		List<Vector2> vectors = new List<Vector2>();

		ypoints.ForEach(it => {

			xpoints.ForEach(itt =>
			{
				vectors.Add(new Vector2(itt.value,it.value));
			});
		
		});

		return vectors.ToArray();
	}

	public Vector2 FindMostClostFromPoints(Vector2 main, Vector2[] vectors)
	{
		if (main == null || vectors == null)
			return Vector2.Zero;

		float x1 = main.x;
		float y1 = main.y;

		List<float> floats = new List<float>();

		for(int i = 0;i < vectors.Length;i++) { 
			
			float x2 = vectors[i].x;
			float y2 = vectors[i].y;

			float real_len = 0; float real_wid = 0;

			if(x1 > x2)
				real_len = x1 - x2;
			else
				real_len = x2 - x1;

			if(y1 > y2)
				real_wid = y1 - y2;
			else
				real_wid = y2 - y1;

			floats.Add((float)Math.Sqrt(real_len*real_len+real_wid*real_wid));

		}

		return vectors[floats.IndexOf(floats.Min())];

	}
	#endregion
}
