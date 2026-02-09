using Godot;

public partial class Exit1 : Node2D
{
	
	public override void _Process(double delta)
	{
		
		// check if the player is close to the exit (less than 50 units)
		if (Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) < 50f)
		{
			// stop processing this node
			SetProcess(false);
			Movement.Player.GetNode<Control>("Panel3").Visible = true;
			// add a button press handler to transition to the next level
			Movement.Player.GetNode<Button>("Panel3/Button").Pressed += () =>
			{
				GetTree().ChangeSceneToFile("res://Level2.tscn"); // switch to the second level
			};
		}
	}
}
// This code defines the Exit1 class, which inherits from Node2D.
// In the _Process(double delta) method, the distance between the player and the Exit1 node is checked.
// If the player is nearby, processing of this node is stopped,
// a results panel is shown (which can include the number of correct answers),
// and a button press handler is added that switches the scene to the second level.
