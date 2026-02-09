using Godot;
public partial class MainMenu : Control
{
	public override void _Ready()
	{
		base._Ready();
		// set up actions for level buttons
		GetNode<Button>("Level1").Pressed += () => GetTree().ChangeSceneToFile("res://Level.tscn");
		GetNode<Button>("Level2").Pressed += () => GetTree().ChangeSceneToFile("res://Level2.tscn");
		
		// AudioStreamPlayer node and play background music
		var menuMusic = GetNode<AudioStreamPlayer>("MenuMusic");
		menuMusic.Stream = GD.Load<AudioStream>("res://Sounds/mainmenu.ogg");
		menuMusic.Play();
		
		// RichTextLabel node for the title and TextEdit node for name input
		var title = GetNode<RichTextLabel>("Title");
		var nameInput = GetNode<TextEdit>("NameInput");
		// set up handler for text changes in the name input field
		nameInput.TextChanged += () => title.Text = "[center]Welcome, " + (nameInput.Text != "" ? nameInput.Text : "stranger") + "!";
	}
}

// This code defines the MainMenu class, which inherits from Control. 
// In the _Ready() method, actions for the buttons are set up to switch between different level scenes.
// Background music for the menu is loaded and played. The name input field updates the title text
// depending on the entered name, greeting the user.
