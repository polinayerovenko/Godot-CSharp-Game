using Godot;

public partial class Movement : CharacterBody2D
{
	// static variable to control the ability to move
	public static bool CanMove = true;
	// static reference to the player
	public static Node2D Player;
	// property to get the player's sprite
	Sprite2D sprite => GetNode<Sprite2D>("Sprite2D");
	
	// time of last animation and last step sound
	float timeOfLastAnimation;
	float timeOfLastStepSound;
	
	public override void _Ready()
	{
		base._Ready();
		// set reference to the player when the node is ready
		Player = this;
	}
	
	// property to control the moving state
	bool Moving
	{
		get => _moving;
		set
		{
			if (value == _moving)
				return;
				
			// change the sprite texture depending on the moving state
			sprite.Texture = ResourceLoader.Load<Texture2D>(value 
				? "res://FreeKnight_v1/Colour1/NoOutline/120x80_PNGSheets/_Run.png"
				: "res://FreeKnight_v1/Colour1/NoOutline/120x80_PNGSheets/_Idle.png");

			_moving = value;
		}
	} bool _moving;

	public override void _Process(double delta)
	{
		base._Process(delta);
		
		 // set movement speed (increases when Shift is pressed)
		float speed = Input.IsKeyPressed(Key.Shift) ? 4 : 2;
		
		// initialize velocity vector
		Velocity = new Vector2(0, 0);

		if (CanMove)
		{
			// check WASD keys for movement
			if (Input.IsKeyPressed(Key.D))
				Velocity += new Vector2(50 * speed, 0);

			if (Input.IsKeyPressed(Key.A))
				Velocity += new Vector2(-50 * speed, 0);
			if (Input.IsKeyPressed(Key.W))
				Velocity += new Vector2(0, -50 * speed);
			if (Input.IsKeyPressed(Key.S))
				Velocity += new Vector2(0, 50 * speed);
		}
		
		// if velocity is not zero, handle animation and step sounds
		if (Velocity != Vector2.Zero)
		{
			// flip sprite depending on movement direction
			if (Velocity.X != 0)
				sprite.Scale = Scale with { X = Velocity.X > 0 ? 1 : -1 };
			
			Moving = true;
			// handle animation
			if (Time.GetTicksMsec() - timeOfLastAnimation > 100)
			{
				timeOfLastAnimation = Time.GetTicksMsec();
				if (sprite.Frame == 9)
					sprite.Frame = 0;
				else
					sprite.Frame++;
			}
			// handle step sounds
			if (Time.GetTicksMsec() - timeOfLastStepSound > 250)
			{
				AudioPlayer.PlayRandomStepSound();
				timeOfLastStepSound = Time.GetTicksMsec();
			}
			
		}
		else
			Moving = false;
		// move the character considering collisions
		MoveAndSlide();
	}
}

// This code defines the Movement class, which inherits from CharacterBody2D.
// The class manages player movement, animation, and step sounds.
// It checks key presses for movement and changes the sprite texture depending on the moving state.
// In the _Process(double delta) method, input handling, animation control, and step sound playback occur.
