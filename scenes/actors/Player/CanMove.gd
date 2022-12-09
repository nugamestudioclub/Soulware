tool
extends State

var player : Player

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	player = target as Player


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction:
		player.velocity.x = direction * player.SPEED
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, player.SPEED)
	
	if Input.is_action_just_pressed("grapple") and is_active("CanGrapple"):
		var _st = change_state("Grapple")
	
	if Input.is_action_just_pressed("ui_accept") and is_active("OnGround"):
		player.velocity.y = player.JUMP_SPEED
		var _st = change_state("InAir")
