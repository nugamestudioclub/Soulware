using Godot;
using System;

public class SimpleBot : KinematicBody2D
{
	private Vector2 velocity;
	private int gravity = 200;
	private int speed = 5;
	private int clock = 0;
	private int maxFallSpeed = 200;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// set initial horizontal velocity
		velocity.x = speed;
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	// Increment clock
	clock += 1;
	
	// Apply gravity
	velocity.y += delta * gravity;
	if (velocity.y > maxFallSpeed) {
		velocity.y = maxFallSpeed;
	}
	
	var motion = velocity * delta;
	MoveAndCollide(motion);
	
	// Move the enemy in a direction determined by clock edge
	this.Position += new Vector2(velocity.x, 0);
	if (clock % 100 == 0) {
		velocity.x = -1 * velocity.x;
	}
	
  }
}
