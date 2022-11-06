using Godot;
using System;

public class Enemy : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Vector2 velocity;
	private int gravity = 200;
	private int speed = 5;
	private int clock = 0;
	private int maxFallSpeed = 200;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		velocity.x = speed;
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	clock += 1;
	velocity.y += delta * gravity;
	if (velocity.y > maxFallSpeed) {
		velocity.y = maxFallSpeed;
	}
	
	var motion = velocity * delta;
	MoveAndCollide(motion);
	
	  this.Position += new Vector2(velocity.x, 0);
	if (clock % 100 == 0) {
		velocity.x = -1 * velocity.x;
		}
	
  }
}
