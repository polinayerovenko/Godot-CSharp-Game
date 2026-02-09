using Godot;

public partial class Exit : Node2D
{
	
	public override void _Process(double delta)
	{
		// check if the player is close to the exit (less than 50 units)
		if (Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) < 50f)
		{
			// stop processing this node
			SetProcess(false);
			// show the results panel
			Movement.Player.GetNode<Control>("Panel2").Visible = true;
			// add a button press handler to go to the main menu
			Movement.Player.GetNode<Button>("Panel2/Button").Pressed += () =>
			{
				GetTree().ChangeSceneToFile("res://MainMenu.tscn"); // switch to MainMenu.tscn
			};
		}
	}
}
// This code defines the Exit class, which inherits from Node2D.
// In the _Process(double delta) method, the distance between the player and the Exit node is checked.
// If the player is nearby, processing of this node is stopped,
// a results panel is shown (which can include the number of correct answers),
// and a button press handler is added that switches the scene to the main menu.
