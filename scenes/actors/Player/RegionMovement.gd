tool
extends State

var player : Player

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

onready var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	player = target as Player
	
	if player.is_on_floor():
		var _st = change_state("OnGround")
	else:
		var _st = change_state("InAir")


# This function is called just after the state enters
# XSM after_enters the children first, then the parent
func _after_enter(_args) -> void:
	pass


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction:
		player.velocity.x = direction * player.SPEED
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, player.SPEED)

	# Vertical movement code. Apply gravity.
	player.velocity.y += gravity
	
	if is_active("OnGround"):
		player.velocity = player.move_and_slide_with_snap(player.velocity, Vector2(0, 1)*12, Vector2(0, -1))
	else:
		player.velocity = player.move_and_slide(player.velocity, Vector2(0, -1))


# This function is called each frame after all the update calls
# XSM after_updates the children first, then the root
func _after_update(_delta: float) -> void:
	pass


# This function is called before the State exits
# XSM before_exits the root first, then the children
func _before_exit(_args) -> void:
	pass


# This function is called when the State exits
# XSM before_exits the children first, then the root
func _on_exit(_args) -> void:
	pass
