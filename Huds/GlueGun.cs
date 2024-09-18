using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Zoikz.Tools;

public class GlueGun : Node2D
{

	private Sprite weaponbase;

	private CollisionShape2D shape;

	private AnimationPlayer _animationPlayer;

	private UpdateOrSell _uporsell;

	//Animationsprites

	private AnimatedSprite _Onelevel;

	private AnimatedSprite _Twolevel;

	private AnimatedSprite _Threelevel;

	/// <summary>
	/// Now It's on enemy,this v is not used
	/// </summary>
	private AnimatedSprite _attackeffect;

	private volatile HashSet<Node2D> attackarea_enemies = new HashSet<Node2D>();

	private Tween _tween;
	
	private Tween _tween2;
	
	private Timer _timer;

	public int Level { get; set; } = 1;

	public override void _Ready()
	{
		weaponbase = GetNode<Sprite>("base");
		shape = GetNode<Area2D>("clickarea").GetNode<CollisionShape2D>("realarea");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_uporsell = GetNode<UpdateOrSell>("uporsell");

		_Onelevel = GetNode<AnimatedSprite>("Onelevel");
		_Twolevel = GetNode<AnimatedSprite>("Twolevel");
		_Threelevel = GetNode<AnimatedSprite>("Threelevel");
		_attackeffect = GetNode<AnimatedSprite>("attackeffect");
		_tween = GetNode<Tween>("Tween");
		_tween2 = GetNode<Tween>("Tween2");
		_timer = GetNode<Timer>("attacktimer");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == (int)ButtonList.Left && buttonEvent.Pressed)
		{

			Vector2 globalMousePosition = GetGlobalMousePosition();


			Vector2 localMousePosition = globalMousePosition;


			GD.Print("Mouse clicked at local position: " + localMousePosition);

			//is Click Sprite
			if (IsPointInClickArea(localMousePosition))
			{
				GD.Print("Click the Sprite");

				//PackedScene pan = GD.Load<PackedScene>("res://Tools/UpdateOrSell.tscn");

				//AddChild(pan.Instance());

				_animationPlayer.Play("showUp");

				_uporsell.Visible = true;

			}

			//is Click out of uporsell
			if (IsPointClickOut_UporSell(localMousePosition) && _uporsell.Visible)
			{
				GD.Print("DEBUG:|Click out of circle! 1");

				_animationPlayer.PlayBackwards("showUp");

				_uporsell.Visible = false;
			}

		}
	}

	public override void _PhysicsProcess(float delta)
	{
		if (attackarea_enemies.Count != 0)
		{
			Node2D enemyNode = attackarea_enemies.First();

			//PathFollow2D fellowpath = (PathFollow2D)enemyNode.GetParent();

			Vector2 enemyPosition = enemyNode.GlobalPosition;

			float x2 = enemyPosition.x; float y2 = enemyPosition.y;

			Vector2 minePosition = weaponbase.GlobalPosition;

			float x1 = minePosition.x; float y1 = minePosition.y;

			double Real_len = 0; double Real_Wid = 0;

			bool angle_bool = false;

			if (x1 > x2)
			{
				Real_len = x1 - x2;
				angle_bool = false;
			}
			else
			{
				Real_len = x2 - x1;
				angle_bool = true;
			}

			if (y1 > y2)
				Real_Wid = y1 - y2;
			else
				Real_Wid = y2 - y1;

			//GD.Print($@"position({x2},{y2}),mineposition({x1},{y1})|,real_len({Real_len},{Real_Wid},{Real_len / Real_Wid})"+Math.Atan(Real_len / Real_Wid));

			if (angle_bool)  //+ angle
			{
				if (_Onelevel.Visible)
					_Onelevel.RotationDegrees = 180 - (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
				if (_Twolevel.Visible)
					_Twolevel.RotationDegrees = 180 - (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
				if (_Threelevel.Visible)
					_Threelevel.RotationDegrees = 180 - (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
			}
			else
			{            //- angle
				if (_Onelevel.Visible)
					_Onelevel.RotationDegrees = -1 * (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
				if (_Twolevel.Visible)
					_Twolevel.RotationDegrees = -1 * (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
				if (_Threelevel.Visible)
					_Threelevel.RotationDegrees = -1 * (float)(Math.Atan(Real_len / Real_Wid) * 180 / Math.PI);
			}

			//decline enemy hp

		}
		else
		{
			if (_Onelevel.Visible)
				_Onelevel.Play("static");
			if (_Twolevel.Visible)
				_Twolevel.Play("static");
			if (_Threelevel.Visible)
				_Threelevel.Play("static");

			//_tween2.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
			//_tween2.Start();
		}

	}


	#region Custom Method

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

	/// <summary>
	/// Is mouse(left) out of sellorUpdate pan
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public bool IsPointClickOut_UporSell(Vector2 vector)
	{
		CollisionShape2D shape = _uporsell.GetNode<Area2D>("baseArea").GetNode<CollisionShape2D>("base");

		if (shape.Shape is CircleShape2D circle)
		{
			Vector2 circlePosition = ToGlobal(shape.Position);

			float radius = circle.Radius;

			GD.Print("DEBUG:|Click out of circle! 2");

			return (vector - circlePosition).Length() > radius;


		}
		return false;
	}


	#endregion


	#region Connect Signals

	private void _on_attackarea_area_entered(object area)
	{
		Node2D node = area as Node2D;

		if (node != null)
		{
			lowzoikz enemy = node.GetParent() as lowzoikz;

			if (enemy != null)
			{
				attackarea_enemies.Add(enemy);

				GD.Print("enemy into attackarea");

				if (_Onelevel.Visible)
					_Onelevel.Play("fire");
				if (_Twolevel.Visible)
					_Twolevel.Play("fire");
				if (_Threelevel.Visible)
					_Threelevel.Play("fire");

				if (Level == 1)
					_timer.WaitTime = 0.5f;
				else if (Level == 2)
					_timer.WaitTime = 0.4f;
				else if (Level == 3)
					_timer.WaitTime = 0.25f;

				_timer.Start();
			}

			fastzoikz fatzoikz = node.GetParent() as fastzoikz;

			if (fatzoikz != null)
			{
				attackarea_enemies.Add(fatzoikz);

				GD.Print("enemy into attackarea");

				if (_Onelevel.Visible)
					_Onelevel.Play("fire");
				if (_Twolevel.Visible)
					_Twolevel.Play("fire");
				if (_Threelevel.Visible)
					_Threelevel.Play("fire");

				if (Level == 1)
					_timer.WaitTime = 0.5f;
				else if (Level == 2)
					_timer.WaitTime = 0.4f;
				else if (Level == 3)
					_timer.WaitTime = 0.25f;

				_timer.Start();
			}

			slowzoikz slowzoikz = node.GetParent() as slowzoikz;

			if (slowzoikz != null)
			{
				attackarea_enemies.Add(slowzoikz);

				GD.Print("enemy into attackarea");

				if (_Onelevel.Visible)
					_Onelevel.Play("fire");
				if (_Twolevel.Visible)
					_Twolevel.Play("fire");
				if (_Threelevel.Visible)
					_Threelevel.Play("fire");

				if (Level == 1)
					_timer.WaitTime = 0.5f;
				else if (Level == 2)
					_timer.WaitTime = 0.4f;
				else if (Level == 3)
					_timer.WaitTime = 0.25f;

				_timer.Start();
			}

			highzoikz highzoikz = node.GetParent() as highzoikz;

			if (highzoikz != null)
			{
				attackarea_enemies.Add(highzoikz);

				GD.Print("enemy into attackarea");

				if (_Onelevel.Visible)
					_Onelevel.Play("fire");
				if (_Twolevel.Visible)
					_Twolevel.Play("fire");
				if (_Threelevel.Visible)
					_Threelevel.Play("fire");

				if (Level == 1)
					_timer.WaitTime = 0.5f;
				else if (Level == 2)
					_timer.WaitTime = 0.4f;
				else if (Level == 3)
					_timer.WaitTime = 0.25f;

				_timer.Start();
			}

			finalzoikz finalzoikz = node.GetParent() as finalzoikz;

			if (finalzoikz != null)
			{
				attackarea_enemies.Add(finalzoikz);

				GD.Print("enemy into attackarea");

				if (_Onelevel.Visible)
					_Onelevel.Play("fire");
				if (_Twolevel.Visible)
					_Twolevel.Play("fire");
				if (_Threelevel.Visible)
					_Threelevel.Play("fire");

				if (Level == 1)
					_timer.WaitTime = 0.5f;
				else if (Level == 2)
					_timer.WaitTime = 0.4f;
				else if (Level == 3)
					_timer.WaitTime = 0.25f;

				_timer.Start();
			}


		}
	}


	private void _on_attackarea_area_exited(object area)
	{
		if (attackarea_enemies.Count != 0)
		{
			Node2D node = area as Node2D;

			if (node != null)
			{
				lowzoikz enemy = node.GetParent() as lowzoikz;

				if (enemy != null)
				{
					if (attackarea_enemies.Contains(enemy))
					{
						//when a enemy exit attackarea,remove it

						//If it with affect with gluegun,then stop it
						if(enemy.affects.Visible) { enemy.affects.Visible = false; }
						if (enemy.THE_SPEED == StaticNumbers.LOW_ZOIKZ_SPEED / 2) { enemy.THE_SPEED *= 2; }

						attackarea_enemies.Remove(enemy);
						GD.Print(" Is Removed! ");
					}

					GD.Print("enemy exit attackarea");

					if (attackarea_enemies.Count == 0)
					{
						if (_Onelevel.Visible)
						{
							_Onelevel.Play("static");
							//GD.Print(_Onelevel.Rotation);
							_tween.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
							//_tween.InterpolateProperty(_Onelevel, "position", _Onelevel.Position, new Vector2(100,100), 1.0f);

							_tween.Start();
						}
						if (_Twolevel.Visible)
						{
							_Twolevel.Play("static");
							_Twolevel.Rotation = 0;
						}
						if (_Threelevel.Visible)
						{
							_Threelevel.Play("static");
							_Threelevel.Rotation = 0;
						}

						_timer.Stop();
					}

				}

				fastzoikz enemy2 = node.GetParent() as fastzoikz;
				if (enemy2 != null)
				{
					if (attackarea_enemies.Contains(enemy2))
					{
						//when a enemy exit attackarea,remove it

						//If it with affect with gluegun,then stop it
						if (enemy2.affects.Visible) { enemy2.affects.Visible = false; }
						if (enemy2.THE_SPEED == StaticNumbers.FAST_ZOIKZ_SPEED / 2) { enemy2.THE_SPEED *= 2; }

						attackarea_enemies.Remove(enemy2);
						GD.Print(" Is Removed! ");
					}

					GD.Print("enemy2 exit attackarea");

					if (attackarea_enemies.Count == 0)
					{
						if (_Onelevel.Visible)
						{
							_Onelevel.Play("static");
							//GD.Print(_Onelevel.Rotation);
							_tween.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
							//_tween.InterpolateProperty(_Onelevel, "position", _Onelevel.Position, new Vector2(100,100), 1.0f);

							_tween.Start();
						}
						if (_Twolevel.Visible)
						{
							_Twolevel.Play("static");
							_Twolevel.Rotation = 0;
						}
						if (_Threelevel.Visible)
						{
							_Threelevel.Play("static");
							_Threelevel.Rotation = 0;
						}

						_timer.Stop();
					}
				}

				slowzoikz enemy3 = node.GetParent() as slowzoikz;
				if (enemy3 != null)
				{
					if (attackarea_enemies.Contains(enemy3))
					{
						//when a enemy exit attackarea,remove it

						//If it with affect with gluegun,then stop it
						if (enemy3.affects.Visible) { enemy3.affects.Visible = false; }
						if (enemy3.THE_SPEED == StaticNumbers.SLOW_ZOIKZ_SPEED/ 2) { enemy3.THE_SPEED *= 2; }

						attackarea_enemies.Remove(enemy3);
						GD.Print(" Is Removed! ");
					}

					GD.Print("enemy3 exit attackarea");

					if (attackarea_enemies.Count == 0)
					{
						if (_Onelevel.Visible)
						{
							_Onelevel.Play("static");
							//GD.Print(_Onelevel.Rotation);
							_tween.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
							//_tween.InterpolateProperty(_Onelevel, "position", _Onelevel.Position, new Vector2(100,100), 1.0f);

							_tween.Start();
						}
						if (_Twolevel.Visible)
						{
							_Twolevel.Play("static");
							_Twolevel.Rotation = 0;
						}
						if (_Threelevel.Visible)
						{
							_Threelevel.Play("static");
							_Threelevel.Rotation = 0;
						}

						_timer.Stop();
					}
				}

				highzoikz enemy4 = node.GetParent() as highzoikz;
				if (enemy4 != null)
				{
					if (attackarea_enemies.Contains(enemy4))
					{
						//when a enemy exit attackarea,remove it

						//If it with affect with gluegun,then stop it
						if (enemy4.affects.Visible) { enemy4.affects.Visible = false; }
						if (enemy4.THE_SPEED == StaticNumbers.HIGH_ZOIKZ_SPEED / 2) { enemy4.THE_SPEED *= 2; }

						attackarea_enemies.Remove(enemy4);
						GD.Print(" Is Removed! ");
					}

					GD.Print("enemy4 exit attackarea");

					if (attackarea_enemies.Count == 0)
					{
						if (_Onelevel.Visible)
						{
							_Onelevel.Play("static");
							//GD.Print(_Onelevel.Rotation);
							_tween.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
							//_tween.InterpolateProperty(_Onelevel, "position", _Onelevel.Position, new Vector2(100,100), 1.0f);

							_tween.Start();
						}
						if (_Twolevel.Visible)
						{
							_Twolevel.Play("static");
							_Twolevel.Rotation = 0;
						}
						if (_Threelevel.Visible)
						{
							_Threelevel.Play("static");
							_Threelevel.Rotation = 0;
						}

						_timer.Stop();
					}
				}

				finalzoikz enemy5 = node.GetParent() as finalzoikz;

				if (enemy5 != null)
				{
					if (attackarea_enemies.Contains(enemy5))
					{
						//when a enemy exit attackarea,remove it

						//If it with affect with gluegun,then stop it
						if (enemy5.affects.Visible) { enemy5.affects.Visible = false; }
						if (enemy5.THE_SPEED == StaticNumbers.FINAL_ZOIKZ_SPEED / 2) { enemy5.THE_SPEED *= 2; }

						attackarea_enemies.Remove(enemy5);
						GD.Print(" Is Removed! ");
					}

					GD.Print("enemy5 exit attackarea");

					if (attackarea_enemies.Count == 0)
					{
						if (_Onelevel.Visible)
						{
							_Onelevel.Play("static");
							//GD.Print(_Onelevel.Rotation);
							_tween.InterpolateProperty(_Onelevel, "rotation_degrees", _Onelevel.RotationDegrees, 0, 1.0f);
							//_tween.InterpolateProperty(_Onelevel, "position", _Onelevel.Position, new Vector2(100,100), 1.0f);

							_tween.Start();
						}
						if (_Twolevel.Visible)
						{
							_Twolevel.Play("static");
							_Twolevel.Rotation = 0;
						}
						if (_Threelevel.Visible)
						{
							_Threelevel.Play("static");
							_Threelevel.Rotation = 0;
						}

						_timer.Stop();
					}
				}
			}




		}
	}
	
	private void _on_attacktimer_timeout()
	{
		if (attackarea_enemies.Count != 0)
		{
			Node2D enemy = attackarea_enemies.First();

			lowzoikz real_enemy = enemy as lowzoikz;

			if (real_enemy != null)
			{
				if (real_enemy.progress.Value <= StaticNumbers.GLUE_DEMAGE)
				{
					real_enemy.progress.Value = 0;
					real_enemy.progress.Visible = false;
					attackarea_enemies.Remove(enemy);
					real_enemy.body.Play("destroy");
				}
				else
				{
					//GlueGun slow down speed,show the affect anim
					real_enemy.progress.Value -= StaticNumbers.GLUE_DEMAGE;
					if (!real_enemy.IS_GLUED)
					{
						real_enemy.THE_SPEED = real_enemy.THE_SPEED / 2;
						real_enemy.IS_GLUED = true;
					}
					real_enemy.affects.Visible = true;
					real_enemy.affects.Play("default");
				}
			}

			fastzoikz real_enemy2 = enemy as fastzoikz;

			if (real_enemy2 != null)
			{
				if (real_enemy2.progress.Value <= StaticNumbers.GLUE_DEMAGE)
				{
					real_enemy2.progress.Value = 0;
					real_enemy2.progress.Visible = false;
					attackarea_enemies.Remove(enemy);
					real_enemy2.body.Play("destroy");
				}
				else
				{
					real_enemy2.progress.Value -= StaticNumbers.GLUE_DEMAGE;
					if (!real_enemy2.IS_GLUED)
					{
						real_enemy2.THE_SPEED = real_enemy2.THE_SPEED / 2;
						real_enemy2.IS_GLUED = true;
					}
					real_enemy2.affects.Visible = true;
					real_enemy2.affects.Play("default");
				}
			}

			slowzoikz real_enemy3 = enemy as slowzoikz;

			if (real_enemy3 != null)
			{
				if (real_enemy3.progress.Value <= StaticNumbers.GLUE_DEMAGE)
				{
					real_enemy3.progress.Value = 0;
					real_enemy3.progress.Visible = false;
					attackarea_enemies.Remove(enemy);
					real_enemy3.body.Play("destroy");
				}
				else
				{
					real_enemy3.progress.Value -= StaticNumbers.GLUE_DEMAGE;
					if (!real_enemy3.IS_GLUED)
					{
						real_enemy3.THE_SPEED = real_enemy3.THE_SPEED / 2;
						real_enemy3.IS_GLUED = true;
					}
					real_enemy3.affects.Visible = true;
					real_enemy3.affects.Play("default");
				}
			}

			highzoikz real_enemy4 = enemy as highzoikz;

			if (real_enemy4 != null)
			{
				if (real_enemy4.progress.Value <= StaticNumbers.GLUE_DEMAGE)
				{
					real_enemy4.progress.Value = 0;
					real_enemy4.progress.Visible = false;
					attackarea_enemies.Remove(enemy);
					real_enemy4.body.Play("destroy");
				}
				else
				{
					real_enemy4.progress.Value -= StaticNumbers.GLUE_DEMAGE;
					if (!real_enemy4.IS_GLUED)
					{
						real_enemy4.THE_SPEED = real_enemy4.THE_SPEED / 2;
						real_enemy4.IS_GLUED = true;
					}
					real_enemy4.affects.Visible = true;
					real_enemy4.affects.Play("default");
				}
			}

			finalzoikz real_enemy5 = enemy as finalzoikz;

			if (real_enemy5 != null)
			{
				if (real_enemy5.progress.Value <= StaticNumbers.GLUE_DEMAGE)
				{
					real_enemy5.progress.Value = 0;
					real_enemy5.progress.Visible = false;
					attackarea_enemies.Remove(enemy);
					real_enemy5.body.Play("destroy");
				}
				else
				{
					real_enemy5.progress.Value -= StaticNumbers.GLUE_DEMAGE;
					if (!real_enemy5.IS_GLUED)
					{
						real_enemy5.THE_SPEED = real_enemy5.THE_SPEED / 2;
						real_enemy5.IS_GLUED = true;
					}
					real_enemy5.affects.Visible = true;
					real_enemy5.affects.Play("default");
				}
			}
		}
	}

	#endregion

}






