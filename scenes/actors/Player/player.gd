extends KinematicBody2D
class_name Player

export var SPEED = 300.0
export var JUMP_SPEED = -400.0

var velocity = Vector2()

var in_air = true

# This is set to 500 in this project
onready var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _physics_process(delta):
	return
	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	# Vertical movement code. Apply gravity.
	velocity.y += gravity
	
	# var stopOnSlope = get_floor_velocity().x != 0 or get_floor_velocity().y != 0
	
	# Check for jumping. is_on_floor() must be called after movement code.
	if is_on_floor():
		in_air = true
		if Input.is_action_just_pressed("ui_accept"):
			velocity.y = JUMP_SPEED
			in_air = false
	
	# Move based on the velocity and snap to the ground.
	if in_air:
		velocity = move_and_slide_with_snap(velocity, Vector2(0, 1)*12, Vector2(0, -1), true, 4, deg2rad(45))
	else:
		velocity = move_and_slide(velocity, Vector2(0, -1))
	

	
