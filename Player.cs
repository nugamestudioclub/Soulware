using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	private Vector2 velocity;
	int speed = 70;
	int gravity = 1200;
	int maxFallSpeed = 800;
	int maxSpeed = 300;
	int jumpForce = 400;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		velocity = new Vector2();
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	
	velocity.x = Mathf.Clamp(velocity.x, -1 * maxSpeed, maxSpeed);
	
	velocity.y += delta * gravity;
	if (velocity.y > maxFallSpeed) {
		velocity.y = maxFallSpeed;
	}	

	if (Input.IsActionPressed("right")) {
		velocity.x += speed;
	}
	else if  (Input.IsActionPressed("left")) {
		velocity.x -= speed;
	}
	else {
		velocity.x = 0;
	}
	if (Input.IsActionPressed("jump") && IsOnFloor()) {
		velocity.y = -1 * jumpForce;
	}
	
	MoveAndSlide(velocity, new Vector2(0, -1));

  }
}
