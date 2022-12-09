tool
extends State

var player : Player

var grappler : GrapplingHook
#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	player = target as Player
	grappler = player.grappling_hook.instance()
	grappler.position += player.grappler_origin.position
	
	var grappler_dir = Vector2()
	
	if Input.is_action_pressed("ui_right"):
		grappler_dir.x += 1
	if Input.is_action_pressed("ui_left"):
		grappler_dir.x -= 1
	if Input.is_action_pressed("ui_up"):
		grappler_dir.y -= 1
	if Input.is_action_pressed("ui_down"):
		grappler_dir.y += 1
	if grappler_dir == Vector2.ZERO:
		grappler_dir = Vector2(1, 0)
	
	grappler.set_direction(grappler_dir)
	
	player.add_child(grappler)
	grappler.connect("grapple_success", player, "on_grappler_success")
	grappler.connect("grapple_retract", player, "on_grappler_retract")
	player.connect("grappler_disconnect", grappler, "unconnect")

