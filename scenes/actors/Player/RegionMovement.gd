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
	
	if player.is_on_floor():
		var _st = change_state("OnGround")
	else:
		var _st = change_state("InAir")

# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction:
		player.velocity.x = direction * player.SPEED
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, player.SPEED)
	
	
	if is_active("OnGround"):
		player.velocity = player.move_and_slide_with_snap(player.velocity, Vector2(0, 1)*12, Vector2(0, -1))
	else:
		player.velocity = player.move_and_slide(player.velocity, Vector2(0, -1))
