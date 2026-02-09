using System;
using Godot;

[Tool]
public partial class TextSubmit : Node2D, Prompt
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
	
	// exported correct answer
	[Export] public string CorrectAnswer;
	
	// exported text field for displaying the question
	[Export]
	public string QuestionText
	{
		get
		{
			if (!HasNode("UI/Question/RichTextLabel")) return "";
			return GetNode<RichTextLabel>("UI/Question/RichTextLabel").Text;
		}
		set
		{
			if (!HasNode("UI/Question/RichTextLabel")) return;
			GetNode<RichTextLabel>("UI/Question/RichTextLabel").Text = value;
		}
	}

	public override void _Ready()
	{
		base._Ready();
		if (Engine.IsEditorHint())
			return;
		
		// disable player movement while editing the text
		var textEdit = GetNode<TextEdit>("UI/Question/TextEdit");
		textEdit.FocusEntered += () =>
		{
			Movement.CanMove = false;
		};
		textEdit.FocusExited += () =>
		{
			Movement.CanMove = true;
		};
		
		// add button press event
		GetNode<TextureButton>("UI/Question/TextureButton").Pressed += () => 
		{
			// hide the UI
			GetNode<Node2D>("UI").Visible = false;
			
			// check the entered text
			var entered = GetNode<TextEdit>("UI/Question/TextEdit").Text;
			bool isCorrect = entered.ToLower() == CorrectAnswer.ToLower();
			if (isCorrect)
			{
				AudioPlayer.Play("res://Sounds/kaching.ogg");
				Interacted();
				SetProcess(false);
			}
			GetNode<Sprite2D>(isCorrect ? "Correct" : "Incorrect").Visible = true;
			if (isCorrect)
				GetNode<Sprite2D>("Incorrect").Visible = false;
			
			GD.Print("Submitted. The answer was " + (isCorrect ? "correct" : "incorrect"));
			
			// re-enable movement
			GetNode<Node2D>("UI").Visible = false;
			Movement.CanMove = true;
		};
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Engine.IsEditorHint())
			return;

		if (!IsActive)
			return;
		// make the UI visible if the player is nearby
		GetNode<Node2D>("UI").Visible = Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) <= 50;
	}
}

// This code defines the TextSubmit class, which inherits from Node2D and implements the Prompt interface.
// The class adds functionality for displaying a text-based question with the ability to enter an answer
// and check if the entered text is correct. In the _Ready() method, event handlers are set up for text input
// and the submit button. In the _Process(double delta) method, the distance between the player and the TextSubmit node
// is checked, and the UI is displayed if the player is close enough.
