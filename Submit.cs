using System;
using System.Collections.Generic;
using Godot;

[Tool]
public partial class Submit : Node2D, Prompt
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
	[Export(PropertyHint.MultilineText)]
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
		
		// get all answer choice buttons
		List<Button> buttons = new()
		{
			GetNode<Button>("UI/Question/A"),
			GetNode<Button>("UI/Question/B"),
			GetNode<Button>("UI/Question/C"),
			GetNode<Button>("UI/Question/D"),
		};
		
		foreach (var _ in buttons)
		{
			// button press handler
			_.Pressed += () =>
			{
				GD.Print("Button clicked");
				
				// check if the answer is correct
				GetNode<Node2D>(_.Name == CorrectAnswer ? "Correct" : "Incorrect").Visible = true;
				if (_.Name == CorrectAnswer)
					GetNode<Sprite2D>("Incorrect").Visible = false;
				
				if (_.Name == CorrectAnswer)
				{
					// play sound
					AudioPlayer.Play("res://Sounds/kaching.ogg");
					Interacted();
					SetProcess(false);
				}
				
				// hide the UI
				GetNode<Node2D>("UI").Visible = false;
			};
		}
	}
	
	public override void _Process(double delta)
	{
		base._Process(delta);
		// stop execution in the editor
		if (Engine.IsEditorHint())
			return;
		if (!IsActive)
			return;
		// show UI if the player is nearby
		GetNode<Node2D>("UI").Visible = Movement.Player.GlobalPosition.DistanceTo(GlobalPosition) < 50;
	}
}
