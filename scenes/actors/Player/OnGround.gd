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
	
	if Input.is_action_just_pressed("ui_accept"):
		var _st = change_state("Jump")
	elif not player.is_on_floor():
		var _st = change_state("InAir")
	
