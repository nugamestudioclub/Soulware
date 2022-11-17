using Godot;
using System;

public class RollingEnemy : KinematicBody2D
{
	
	private Vector2 velocity;
	private int gravity = 300;
	private int speed = 300;
	private int maxFallSpeed = 300;
	// vertical jump force
	int jumpForce = 250;
	// horizontal jump force
	int xJumpForce = 40;
	// minimum y distance from player necessary to jump at them
	int yConstant = 100;
	// minimum x distance from player necessary to jump at them
	int xConstant = 350;
	// player position
	Vector2 player;
	// direction of this enemy
	Vector2 direction;

// Sets the player position and this enemy's direction
private void SetVectors()
{
	player = GetTree().Root.GetNode("World").GetNode<Node2D>("Player").Position;
	direction = (player - this.Position).Normalized();
}

// Checks whether to jump at player, and if so, changes velocity vector
private void JumpAtPlayer()
{
	float xDistFromPlayer = Math.Abs(this.Position.x - player.x);
	
	// Jump AT player if:
	// - player is certain x distance away
	// - or if player is certain y distance away
	if ((player.y < this.Position.y - yConstant || xDistFromPlayer > xConstant) 
	&& IsOnFloor()) 
	{
		velocity.y = -1 * jumpForce;
		if (this.Position.x - player.x > 0) {
			velocity.x -= xJumpForce;
		}
		else if (this.Position.x - player.x < 0) {
			velocity.x += xJumpForce;
		}
			
	}
}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
{ 
	// Set vectors
	this.SetVectors();
	
	if (IsOnFloor())
	{
		velocity.x = direction.x * speed;
	}

	// Apply gravity
	velocity.y += delta * gravity;
	if (velocity.y > maxFallSpeed) {
		velocity.y = maxFallSpeed;
	}
	
	this.JumpAtPlayer();
	
	MoveAndSlide(velocity, new Vector2(0, -1));
}

}
