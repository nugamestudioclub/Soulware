tool
extends State

var hook : GrapplingHook

var ball_distance : float

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	hook = target as GrapplingHook
	ball_distance = hook.get_ball_distance()


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	# TODO vector length is always positive, but I want to exit when this equals 0.
	# if hook.ball.position.is_equal_approx(Vector2.ZERO):
	# if hook.get_ball_distance() > ball_distance:
	if hook.ball.position.length() < .0001:
		hook.emit_signal("grapple_retract")
		print("Hook retracted")
		hook.queue_free()
	else:
		ball_distance = hook.get_ball_distance()
		hook.ball.position = hook.ball.position.move_toward(Vector2.ZERO, hook.velocity * _delta)
