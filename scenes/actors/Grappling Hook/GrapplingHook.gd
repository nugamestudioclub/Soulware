extends Node2D

class_name GrapplingHook

#warning-ignore:unused_signal
signal grapple_success
#warning-ignore:unused_signal
signal grapple_retract

var grappled = false

var dir = Vector2.RIGHT

export var velocity = 900.0
export var max_distance = 150.0

onready var ball = $Ball
onready var chain = $Chain
onready var sm = $HookStateMachine

func _process(_delta):
	chain.rotation = ball.position.angle()
	chain.position = ball.position / 2
	chain.get_node("Sprite").region_rect.size.x = ball.position.length()
	chain.get_node("CollisionShape2D").shape.extents.x = ball.position.length() / 2

func _on_CollisionArea_body_entered(body):
	if not grappled and !body.is_in_group("Player"):
		grappled = true
		print("Body Entered")
		if body.is_in_group("CanGrapple"):
			var _st = sm.change_state("Connecting", body)
		else:
			var _st = sm.change_state("Retracting")

func _on_CollisionArea_area_entered(area):
	if not grappled and !area.is_in_group("Player") and !area.is_in_group("GrapplingHook"):
		grappled = true
		print("Area Entered")
		if area.is_in_group("CanGrapple"):
			print("Area can be grappled")
			var _st = sm.change_state("Connecting", area)

func get_ball_distance():
	return ball.position.length()

func set_direction(input_dir : Vector2):
	dir = Vector2(input_dir)

func unconnect():
	sm.change_state("Retracting")
