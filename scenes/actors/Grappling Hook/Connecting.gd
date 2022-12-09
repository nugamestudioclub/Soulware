tool
extends State

var hook : GrapplingHook
var connecting : Node2D

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children

# _args is the object we're connecting to
func _on_enter(connect_node) -> void:
	hook = target as GrapplingHook
	connecting = connect_node
	hook.emit_signal("grapple_success", connecting)


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	if hook.ball.global_position != connecting.global_position:
		hook.ball.set_global_position(connecting.global_position)
