using System;
using Godot;

[Tool]
public partial class Sign : Node2D, Prompt
{
	// exported text property for displaying text on the sign
	[Export]
	public string text
	{
		get
		{
			// check for the existence of the RichTextLabel node and return its text
			if (!HasNode("UI/Text/Panel/RichTextLabel")) return "";
			return GetNode<RichTextLabel>("UI/Text/Panel/RichTextLabel").Text;
		}
		set
		{
			// check for the existence of the RichTextLabel node and set its text
			if (!HasNode("UI/Text/Panel/RichTextLabel")) return;
			GetNode<RichTextLabel>("UI/Text/Panel/RichTextLabel").Text = value;
		}
	}
	// property to control the activity of the sign
	bool Active
	{
		get => _active;
		set
		{
			// manage the visibility of UI elements depending on activity
			GetNode<Control>(new("UI/Control")).Visible = !_active;
			GetNode<Control>(new("UI/Text")).Visible = _active;
			_active = value;
		}
	} bool _active;
	
	bool lastPressed = false;
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Engine.IsEditorHint())
			return;
		
		// if the player is close to the sign, show the UI
		var close = Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) <= 50;
		GetNode<Node2D>("UI").Visible = close;
		if (!close)
			Active = false;
		
		// check for the 'E' key press to interact with the sign
		var pressed = Input.IsKeyPressed(Key.E);
		if (close && !lastPressed && pressed)
		{
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

// This code defines the Sign class, which inherits from Node2D and implements the Prompt interface.
// The class adds functionality for displaying text on the sign, managing the sign's activity,
// and interacting with the player. In the _Process(double delta) method, the distance between
// the player and the Sign node is checked. If the player is nearby, the UI is displayed.
// The Active property manages the active state of the sign and the display of text on the panel.

