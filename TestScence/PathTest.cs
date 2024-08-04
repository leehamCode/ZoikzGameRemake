using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zoikz.Tools;

public class PathTest : Node2D
{
	/// <summary>
	/// Path Speed
	/// </summary>
	private static float SPEED = 0.001f;


	private RigidBody2D rigidBody2D;

	private Random ran { get; set; } = new Random();

	//0neLevelTimer
	private Timer _timer;

	private Node2D spawntimers { get; set; }

	//Maingun list
	public List<Main> mains { get; set; } = new List<Main>();

	//Place weapon area
	public List<CollisionShape2D> shapes { get; set; } = new List<CollisionShape2D>();

	//all enemy paths
	public List<Path2D> all_paths { get; set; } = new List<Path2D>();

	[Export]
	public List<Timer> leveltimers { get; set; } = new List<Timer>();

	public Timer gaptimer { get; set; }

	/// <summary>
	/// themenu
	/// </summary>
	public GameFootMenu menu { get; set; }

	public PackedScene fastzoikz_packed = GD.Load<PackedScene>("res://Enemy/fastzoikz.tscn");
	public PackedScene finalzoikz_packed = GD.Load<PackedScene>("res://Enemy/finalzoikz.tscn");
	public PackedScene lowzoikz_packed = GD.Load<PackedScene>("res://Enemy/lowzoikz.tscn");
	public PackedScene highzoikz_packed = GD.Load<PackedScene>("res://Enemy/highzoikz.tscn");
	public PackedScene slowzoikz_packed = GD.Load<PackedScene>("res://Enemy/slowzoikz.tscn");

	public override void _Ready()
	{


		_timer = GetNode<Timer>("OneLevel");
		spawntimers = GetNode<Node2D>("SpawnZoikzTimers");
		gaptimer = GetNode<Timer>("levelGap");
		menu = GetNode<GameFootMenu>("footmenu");

		for(int i = 1; i<=StaticNumbers.LEVEL3_WAVE; i++)
		{
			Timer timer = new Timer();
			LevelConfig levelConfig = StaticNumbers.level3config[i-1];

			timer.Name = "level" + i;
			timer.WaitTime = levelConfig.WaitTime;

			Godot.Collections.Array array = new Godot.Collections.Array();

			array.Add(levelConfig.LowZoikzNum);
			array.Add(levelConfig.SlowZoikzNum);
			array.Add(levelConfig.FastZoikzNum);
			array.Add(levelConfig.HighZoikzNum);
			array.Add(levelConfig.FinalZoikzNum);
			array.Add(levelConfig.OneTimeSpanCount);
			array.Add(levelConfig.LevelIndex);

			timer.Connect("timeout",this, "_BatchLevel_timeout"+i,array);
			
			leveltimers.Add(timer);
			spawntimers.AddChild(timer);
		}

		leveltimers.First().Start();
		//_timer.Start();
		//_timer.Paused

		#region collect path2ds

		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("downpath1"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("downpath2"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("downpath3"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("downpath4"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("uppath1"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("uppath2"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("uppath3"));
		all_paths.Add(GetNode<Node2D>("paths").GetNode<Path2D>("uppath4"));



		#endregion

		#region take colarea
		rigidBody2D = GetNode<RigidBody2D>("rigarea");
		CollisionShape2D col1 = rigidBody2D.GetNode<CollisionShape2D>("midone");
		CollisionShape2D col2 = rigidBody2D.GetNode<CollisionShape2D>("midtwo");
		CollisionShape2D col3 = rigidBody2D.GetNode<CollisionShape2D>("midthree");

		CollisionShape2D col4 = rigidBody2D.GetNode<CollisionShape2D>("leftone");
		CollisionShape2D col5 = rigidBody2D.GetNode<CollisionShape2D>("lefttwo");
		CollisionShape2D col6 = rigidBody2D.GetNode<CollisionShape2D>("leftthree");
		CollisionShape2D col7 = rigidBody2D.GetNode<CollisionShape2D>("leftfour");
		CollisionShape2D col8 = rigidBody2D.GetNode<CollisionShape2D>("leftfive");
		CollisionShape2D col9 = rigidBody2D.GetNode<CollisionShape2D>("leftsix");
		CollisionShape2D col10 = rigidBody2D.GetNode<CollisionShape2D>("leftseven");
		CollisionShape2D col11 = rigidBody2D.GetNode<CollisionShape2D>("lefteight");

		CollisionShape2D col12 = rigidBody2D.GetNode<CollisionShape2D>("topone");
		CollisionShape2D col13 = rigidBody2D.GetNode<CollisionShape2D>("toptwo");
		CollisionShape2D col14 = rigidBody2D.GetNode<CollisionShape2D>("topthree");
		CollisionShape2D col15 = rigidBody2D.GetNode<CollisionShape2D>("topfour");
		CollisionShape2D col16 = rigidBody2D.GetNode<CollisionShape2D>("topfive");
		CollisionShape2D col17 = rigidBody2D.GetNode<CollisionShape2D>("topsix");
		CollisionShape2D col18 = rigidBody2D.GetNode<CollisionShape2D>("topseven");


		CollisionShape2D col19 = rigidBody2D.GetNode<CollisionShape2D>("bottomone");
		CollisionShape2D col20 = rigidBody2D.GetNode<CollisionShape2D>("bottomtwo");
		CollisionShape2D col21 = rigidBody2D.GetNode<CollisionShape2D>("bottomthree");
		CollisionShape2D col22 = rigidBody2D.GetNode<CollisionShape2D>("bottomfour");
		CollisionShape2D col23 = rigidBody2D.GetNode<CollisionShape2D>("bottomfive");
		CollisionShape2D col24 = rigidBody2D.GetNode<CollisionShape2D>("bottomsix");
		CollisionShape2D col25 = rigidBody2D.GetNode<CollisionShape2D>("bottomseven");
		CollisionShape2D col26 = rigidBody2D.GetNode<CollisionShape2D>("bottomeight");
		CollisionShape2D col27 = rigidBody2D.GetNode<CollisionShape2D>("bottomnine");

		CollisionShape2D col28 = rigidBody2D.GetNode<CollisionShape2D>("inmidone");
		CollisionShape2D col29 = rigidBody2D.GetNode<CollisionShape2D>("inmidtwo");

		shapes.Add(col1); shapes.Add(col2); shapes.Add(col3); shapes.Add(col4); shapes.Add(col5); shapes.Add(col6); shapes.Add(col7); shapes.Add(col8); shapes.Add(col9);
		shapes.Add(col10); shapes.Add(col12); shapes.Add(col13); shapes.Add(col14); shapes.Add(col15); shapes.Add(col16); shapes.Add(col17); shapes.Add(col18); shapes.Add(col19);
		shapes.Add(col11); shapes.Add(col20); shapes.Add(col21); shapes.Add(col22); shapes.Add(col23); shapes.Add(col24); shapes.Add(col25); shapes.Add(col26); shapes.Add(col27);
		shapes.Add(col28); shapes.Add(col29); 

		#endregion


	}

	public override void _PhysicsProcess(float delta)
	{

		/*if (_fellow2d.UnitOffset > 1)
		{
			_fellow2d.UnitOffset = 0;
		}
		else
		{
			_fellow2d.UnitOffset += StaticNumbers.LOW_ZOIKZ_SPEED;
			
		}

		if (_pathfellow2.UnitOffset > 1)
		{
			_pathfellow2.UnitOffset = 0;
		}
		else
		{
			_pathfellow2.UnitOffset += StaticNumbers.LOW_ZOIKZ_SPEED;

		}

		if(_fellow3.UnitOffset > 1)
		{
			_fellow3.UnitOffset = 0;
		}
		else
		{
			_fellow3.UnitOffset += StaticNumbers.FAST_ZOIKZ_SPEED;
		}*/

		if (StaticNumbers.LIVES == 0)
		{
			GD.Print("GameOver");
		}

	}


	#region ConnectionSignals 

	/// <summary>
	/// Enemy Into EndArea and Destroy 
	/// </summary>
	/// <param name="area"></param>
	private void Into_destrroy_area_entered(object area)
	{
		lowzoikz lowzoikz = area as lowzoikz;

		if (lowzoikz != null) { lowzoikz.body.Play("destroy"); }

		slowzoikz slowzoikz = area as slowzoikz;

		if (slowzoikz != null) { slowzoikz.body.Play("destroy"); }

		fastzoikz fastzoikz = area as fastzoikz;

		if (fastzoikz != null) { fastzoikz.body.Play("destroy"); }

		highzoikz highzoikz = area as highzoikz;

		if(highzoikz != null) { highzoikz.body.Play("destroy"); }

		finalzoikz finalzoikz = area as finalzoikz;

		if(finalzoikz != null) { finalzoikz.body.Play("destroy"); }

		StaticNumbers.LIVES--;
	}

	private void _on_OneLevel_timeout()
	{
		

		fastzoikz node = (fastzoikz)fastzoikz_packed.Instance();

		Random ran = new Random();

		//Random Select a path
		Path2D path2D = all_paths[ran.Next(0, all_paths.Count)];

		path2D.AddChild(node);

		//GD.Print("HOLY SHIT");
	}

	#endregion


	#region fuck,60 methods


	private void _BatchLevel_timeout1(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{

		if(StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array();param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1]+"Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}



				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if(real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if(real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if(real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
		
	}
	private void _BatchLevel_timeout2(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level2");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout3(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level3");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout4(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level4");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout5(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level5");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout6(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level6");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout7(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("It Start Level7");
		if (StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum == (lownum + slownum + fastnum + highnum + finalnum))
		{
			leveltimers[levelIndex - 1].Stop();

			Godot.Collections.Array param = new Godot.Collections.Array(); param.Add(levelIndex);
			if (gaptimer.IsConnected("timeout", this, "_on_levelGap_timeout"))
				gaptimer.Disconnect("timeout", this, "_on_levelGap_timeout");
			gaptimer.Connect("timeout", this, "_on_levelGap_timeout", param);
			gaptimer.WaitTime = StaticNumbers.LEVEL_GAP;
			gaptimer.OneShot = true;
			gaptimer.Start();
			menu.wave_label.Text = levelIndex.ToString();
			GD.Print(StaticNumbers.level3_verify[levelIndex - 1] + "Now Stop It!" + (lownum + slownum + fastnum + highnum + finalnum));
		}
		else
		{
			LevelVerify levelVerify = StaticNumbers.level3_verify[levelIndex - 1];
			if (totolcount == 1)
			{
				List<PathFollow2D> templist = new List<PathFollow2D>();
				lowzoikz low = null;
				if (levelVerify.LowZoikzNum != 0)
				{
					low = (lowzoikz)lowzoikz_packed.Instance();
					templist.Add(low);
				}
				slowzoikz slow = null;
				if (levelVerify.SlowZoikzNum != 0)
				{
					slow = (slowzoikz)slowzoikz_packed.Instance();
					templist.Add(slow);
				}
				fastzoikz fast = null;
				if (levelVerify.FastZoikzNum != 0)
				{
					fast = (fastzoikz)fastzoikz_packed.Instance();
					templist.Add(fast);
				}
				highzoikz high = null;
				if (levelVerify.HighZoikzNum != 0)
				{
					high = (highzoikz)highzoikz_packed.Instance();
					templist.Add(high);
				}
				finalzoikz final = null;
				if (levelVerify.FinalZoikzNum != 0)
				{
					final = (finalzoikz)finalzoikz_packed.Instance();
					templist.Add(final);
				}

				if (templist.Count != 0)
				{
					PathFollow2D real = templist[ran.Next(templist.Count)];

					if (real is lowzoikz)
						levelVerify.LowZoikzNum--;
					else if (real is highzoikz)
						levelVerify.HighZoikzNum--;
					else if (real is finalzoikz)
						levelVerify.FinalZoikzNum--;
					else if (real is slowzoikz)
						levelVerify.SlowZoikzNum--;
					else if (real is fastzoikz)
						levelVerify.FastZoikzNum--;

					Path2D randpath = all_paths[ran.Next(all_paths.Count)];

					randpath.AddChild(real);

					StaticNumbers.level3_verify[levelIndex - 1].TheTotalNum++;
				}


			}
		}
	}
	private void _BatchLevel_timeout8(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout9(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout10(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout11(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout12(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout13(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout14(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout15(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout16(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout17(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout18(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout19(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout20(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout21(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout22(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout23(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout24(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout25(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout26(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout27(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout28(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout29(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout30(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout31(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout32(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout33(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout34(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout35(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout36(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout37(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout38(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout39(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout40(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout41(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout42(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout43(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout44(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout45(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout46(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout47(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout48(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}


	private void _BatchLevel_timeout49(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout50(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout51(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout52(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout53(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout54(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}

	private void _BatchLevel_timeout55(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout56(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout57(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout58(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout59(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	private void _BatchLevel_timeout60(int lownum, int slownum, int fastnum, int highnum, int finalnum, int totolcount,int levelIndex)
	{
		GD.Print("IS REAL CONNECT?" + lownum + "fuck" + slownum + "fuck" + fastnum + "fuck" + highnum + "fuck" + fastnum + "fuck" + totolcount);
	}
	#endregion

	private void _on_levelGap_timeout(int levelIndex)
	{
		GD.Print("Start Next Level!" + levelIndex);
		leveltimers[levelIndex].Start();
	}

}









