tool
extends State

var hook : GrapplingHook

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	hook = target as GrapplingHook


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	if hook.ball.get_position().length() >= hook.max_distance:
		var _st = change_state("Retracting")
	else:
		hook.ball.position += (hook.velocity * hook.dir) * _delta 

