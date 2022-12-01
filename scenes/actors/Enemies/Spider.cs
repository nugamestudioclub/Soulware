using Godot;
using System;

public class Spider : KinematicBody2D
{

	private Vector2 velocity;
	private int gravity = 300;
	private int speed = 200;
	private int maxFallSpeed = 600;
	int jumpForce = 250;
	private int clock = 0;
	// horizontal distance from player to trigger drop
	int xRange = 15;
	int fallSpeed = 500;
	// player position
	Vector2 player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// initialize velocity
		velocity = new Vector2();
		velocity.x = speed;
	}
	
	// sets the player position
	private void SetPlayer()
	{
		player = GetTree().Root.GetNode("World").GetNode<Node2D>("Player").Position;
	}
	
	// Determines whether player is in range to be dropped on, and if so, drop
	private void DropOnPlayer()
	{
		if (player.x + xRange >= this.Position.x && player.x - xRange <= this.Position.x) {
			velocity.y += fallSpeed;
			velocity.x = 0;
		}
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	// increment clock
	clock += 1;
	
	// change horizontal direction based on clock edge
	if (clock % 100 == 0) {
		velocity.x = -1 * velocity.x;
	}
	
	// set player position
	this.SetPlayer();
	
	// drop on player if in range
	this.DropOnPlayer();
	
	// upper bound y velocity at max fall speed
	if (velocity.y > maxFallSpeed) {
		velocity.y = maxFallSpeed;
	}
	
	// destroy this enemy on contact with ground
	if (IsOnFloor()) {
		this.QueueFree();
	}
	
	MoveAndSlide(velocity, new Vector2(0, -1));
  }
}
