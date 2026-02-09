using System;
using Godot;

public partial class Door : Node2D, Prompt
{
	// exported node pointing to the next step or node
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
	
	public override void _Ready()
	{
		base._Ready();
		// define behavior when moving to the next step
		BecameNext = () =>
		{
			// move the door to a new Y position
			GlobalPosition = new Vector2(GlobalPosition.X, 999);
			return true;
		};
	}
}

// This code defines the Door class, which inherits from Node2D and implements the Prompt interface.
// It adds functionality for handling interaction with the door and transitioning to the next step or node.
// In the _Ready() method, the behavior for moving to the next step is defined, where the door is moved
// to a new position along the Y axis.
