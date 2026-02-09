using Godot;

public partial class BigPanel : Panel
{
	// static reference to the instance of the BigPanel class
	public static BigPanel refer;
	public override void _Ready()
	{
		base._Ready();
		// set reference to the current instance of the BigPanel class
		refer = this;
		// add event handler for the X button press
		GetNode<Button>("X").Pressed += () => Visible = false;
		
	}
	// method to display text on the panel
	public static void ShowText(string what)
	{
		GD.Print("Showing text..");
		// set text in the RichTextLabel and enable text selection
		refer.GetNode<RichTextLabel>("RichTextLabel").Text = what;
		refer.GetNode<RichTextLabel>("RichTextLabel").SelectionEnabled = true;
		// make the panel visible
		refer.Visible = true;
	}
	// method to clear the panel
	public static void Clear()
	{
		if (refer == null) return;
		GD.Print("Clearing big panel");
		// make the panel invisible
		refer.Visible = false;
	}
}

// This code defines the BigPanel class, which inherits from Panel
// and adds functionality for displaying text on the panel. 
// It includes a method for showing text and a method for hiding the panel.
// The X button is used to hide the panel when pressed.
