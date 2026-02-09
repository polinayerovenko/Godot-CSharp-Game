using System;
using System.Collections.Generic;
using Godot;

public partial class BigPanelFillIn : Panel
{
	// static reference to the instance of the BigPanelFillIn class
	public static BigPanelFillIn refer;
	
	public override void _Ready()
	{
		base._Ready();
		// set reference to the current instance of the BigPanelFillIn class
		refer = this;
		// add event handler for the X button press
		GetNode<Button>("X").Pressed += () =>
		{
			Visible = false;
			Movement.CanMove = true;
		};
		// add event handler for the Confirm button press
		refer.GetNode<Button>("Confirm").Pressed += () =>
		{
			submission();
		};
		
		var texEdit = GetNode<TextEdit>("TextEdit");
		// disable movement while the text field is focused
		texEdit.FocusEntered += () =>
		{
			Movement.CanMove = false;
		};
		texEdit.FocusExited += () =>
		{
			Movement.CanMove = true;
		};
	}
	// static action for handling submission
	static Action submission = () => { GD.Print("Attempting"); };
	
	// method to display text and check answers
	public static void ShowText(string what, List<string> answers, Node2D us)
	{
		refer.Visible = true;
		
		string correct = what;
		// replace "???" markers in the text with the answers
		while (answers.Count > 0)
		{
			for (int i = 0; i < correct.Length; i++)
			{
				if (i >= 2 && correct[i] == '?' && correct[i - 1] == '?' && correct[i - 2] == '?')
				{
					i -= 2;
					correct = correct.Remove(i, 3);
					correct = correct.Insert(i, answers[0]);
					answers.RemoveAt(0);
					break;
				}
			}
		}

		var edit = refer.GetNode<TextEdit>("TextEdit");
		edit.Text = what;
		
		submission = () =>
		{
			// check if the answer is correct
			if (edit.Text == correct)
			{
				us.GetNode<Sprite2D>("Correct").Visible = true;
				us.GetNode<Node2D>("UI").Visible = false;
				AudioPlayer.Play("res://Sounds/kaching.ogg");
				refer.Visible = false;
				Movement.CanMove = true;
				us.SetProcess(false);
				(us as Prompt).Interacted();
			}
		};
	}
	// method to clear the panel
	public static void Clear()
	{
		if (refer == null) return;
		GD.Print("Clearing text..");
		refer.Visible = false;
	}
}

// This code defines the BigPanelFillIn class, which inherits from Panel
// and adds functionality for displaying a text field with the ability
// to input and check answers. It includes methods for showing text,
// verifying correctness of the input, and methods for managing panel visibility
// and disabling movement while the text field is focused.
