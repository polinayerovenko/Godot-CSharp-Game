using System;
using Godot;

[Tool]
public partial class BigSign : Node2D, Prompt
{
	// Exported text field for displaying text on the sign
	[Export(PropertyHint.MultilineText)] public string text;
	// property to control the sign's activity
	bool Active
	{
		get => _active;
		set
		{
			if (value == _active)
				return;
			
			if (value)
				BigPanel.ShowText(text); // display text on the panel if the sign is active

			else
				BigPanel.Clear(); // clear the panel if the sign is inactive

			_active = value;
		}
	} bool _active;
	
	bool lastPressed = false;
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Engine.IsEditorHint())
			return;
		
		// if the player is nearby, show the UI

		var close = Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) <= 50 && IsActive;
		GetNode<Node2D>("UI").Visible = close;
		if (!close)
			Active = false;

		var pressed = Input.IsKeyPressed(Key.E);
		if (close && !lastPressed && pressed)
		{
			GD.Print("Showing sign " + Name);
			lastPressed = true;
			Active = !Active;
			Interacted();
		}
		lastPressed = pressed;
	}
// exported node for the next step

	[Export]
	public Node next;
	// function that determines if this has become the next step
	public Func<bool> BecameNext { get; set; } = () => false;
	// action performed on interaction
	public Action Interacted { get; set; } = () => { };
	// interface for the next step
	public Prompt Next => (Prompt)next;
	// property to control activity
	public bool IsActive { get; set; } = false;
}
// This code defines the BigSign class, which inherits from Node2D and implements the Prompt interface.
// The class adds functionality for displaying text on the sign,
// controlling the sign's activity, and interacting with the player.
// It includes methods for displaying text on the panel when the sign is active,
// managing the visibility of the user interface, and handling player input.

