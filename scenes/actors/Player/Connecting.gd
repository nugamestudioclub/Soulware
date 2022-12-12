tool
extends State

var player : Player
var connecting : Node2D

var grappler : GrapplingHook

var previous_travel_dir : Vector2

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(connect_node) -> void:
	player = target as Player
	connecting = connect_node
	grappler = player.get_node("GrapplingHook")
	var _st = change_state("NoMove")


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	if Input.is_action_just_pressed("grapple"):
		var _st = change_state("NoGrapple")
		player.emit_signal("grappler_disconnect")
	
	if player.global_position.distance_to(connecting.global_position) < 10:
		player.velocity = Vector2.ZERO
		player.global_position = connecting.global_position
	else:
		 player.velocity = player.GRAPPLE_SPEED * player.global_position.direction_to(connecting.global_position)
	

# This function is called before the State exits
# XSM before_exits the root first, then the children
func _before_exit(_args) -> void:
	pass


# This function is called when the State exits
# XSM before_exits the children first, then the root
func _on_exit(_args) -> void:
	var _st = change_state("CanMove")
