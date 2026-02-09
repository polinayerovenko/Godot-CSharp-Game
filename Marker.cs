using System;
using Godot;

// Prompt interface for nodes that can be part of a sequence
public interface Prompt
{
	public Func<bool> BecameNext { get; set; }
	public Action Interacted { get; set; }
	public Prompt Next { get; }
	public bool IsActive { get; set; }
}
// Marker class, which inherits from Sprite2D, to manage a sequence of nodes
public partial class Marker : Sprite2D
{
	// Current property for managing the current node in the sequence
	public Node Current
	{
		get => _current;
		set
		{
			// while value is not null and the next node became active
			while (value != null && (value as Prompt).BecameNext())
				value = ((value as Prompt).Next as Node);
			// if value is not null, set IsActive to true
			if (value != null)
				(value as Prompt).IsActive = true;
			// update current value and position
			_current = value;
			GlobalPosition = ((Node2D)value).GlobalPosition + new Vector2(0,-10);
		}
	}
	// exported field to store the current node
	[Export]
	Node _current = null;
	
	public override void _Ready()
	{
		// set the initial position of the marker
		GlobalPosition = ((Node2D)Current).GlobalPosition + new Vector2(0,-10);
		// subscribe to the Interacted event of the current node
		((Prompt)Current).Interacted += a;
		// local function to handle interaction with the current node
		void a()
		{
			// unsubscribe from the previous node and move to the next node
			((Prompt)Current).Interacted -= a;
			Current = (Node)((Prompt)Current).Next;
			((Prompt)Current).Interacted += a;
		}
	}
}
// This code defines the Prompt interface and the Marker class, which inherits from Sprite2D.
// The Prompt interface defines properties and methods for nodes that can be part of a sequence.


// The Marker class manages the sequence of nodes by setting the current node and its activity,
// and updating the marker's position. In the _Ready() method, the initial position of the marker
// is set, and it subscribes to the Interacted event of the current node to move to the next node in the sequence.
