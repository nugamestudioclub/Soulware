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

func _on_update(_delta):
	if is_active("OnGround"):
		player.velocity = player.move_and_slide_with_snap(player.velocity, Vector2(0, 1)*12, Vector2(0, -1), true)
	else:
		player.velocity = player.move_and_slide(player.velocity, Vector2(0, -1))
