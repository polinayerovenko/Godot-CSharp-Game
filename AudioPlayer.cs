using System.Collections.Generic;
using Godot;

public partial class AudioPlayer : AudioStreamPlayer2D
{
	// static reference to the instance of the AudioPlayer class
	public static AudioPlayer refer;
	public override void _Ready()
	{
		base._Ready();
		// set the reference to the current instance of the AudioPlayer class
		refer = this;
	}
	
	// method to play audio from the specified path
	public static void Play(string path)
	{
		// load the audio stream from the resource
		refer.Stream = ResourceLoader.Load<AudioStream>(path);
		refer.Play();
	}
	// method to play a random step sound
	public static void PlayRandomStepSound()
	{
		// create a list of step sound paths and play one
		Play(new List<string>
		{
			"res://Sounds/step1.ogg",
			"res://Sounds/step2.ogg",
			"res://Sounds/step3.ogg",
			"res://Sounds/step4.ogg",
			"res://Sounds/step5.ogg",
		// generate a random index to select a random sound from the list
		}[new RandomNumberGenerator().RandiRange(0, 4)]);
	}
}
	
	// This code defines the AudioPlayer class, 
// which inherits from AudioStreamPlayer2D and adds functionality for audio playback. 
// It includes a method to play an audio file from a given path and 
// a method to play a random sound from a set of step sounds.
