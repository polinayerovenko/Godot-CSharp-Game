using Godot;
using System;
using System.Linq;

[Tool]
public partial class FillInQuestion : Node2D, Prompt
{
	// exported text field for displaying the question
	[Export(PropertyHint.MultilineText)] public string text;
	// exported text field for storing answers, separated by commas
	[Export(PropertyHint.MultilineText)] public string answers;
	// property to control the activity of the question
	bool Active
	{
		get => _active;
		set
		{
			if (value == _active)
				return;

			if (value)
			{
				GD.Print("Showing 0");
				// show the question text and possible answers on the panel
				BigPanelFillIn.ShowText(text, answers.Split(',').ToList(), this);
			}
			else
				BigPanel.Clear(); // clear the panel if the question is not active
			_active = value;
		}
	} bool _active;

	bool lastPressed = false;
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Engine.IsEditorHint())
			return;
		
		// if the player is close to the question, show the UI
		var close = Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) <= 50 && IsActive;
		GetNode<Node2D>("UI").Visible = close;
		if (!close)
			Active = false;

		var pressed = Input.IsKeyPressed(Key.E);
		if (close && !lastPressed && pressed && !Active)
		{
			lastPressed = true;
			Active = !Active;
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

// This code defines the FillInQuestion class, which inherits from Node2D and implements the Prompt interface.
// It adds functionality for displaying a text question and possible answers on a panel.
// In the _Process(double delta) method, the distance between the player and the FillInQuestion node is checked.
// If the player is nearby, the UI is displayed. The Active property manages the
// active state of the question and the display of text on the panel.
