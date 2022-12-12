extends KinematicBody2D
class_name Player

export var SPEED = 300.0
export var JUMP_SPEED = -400.0

export var GRAPPLE_SPEED = 900.0

var grappling_hook = preload("res://scenes/actors/Grappling Hook/GrapplingHook.tscn")
onready var grappler_origin = $GrapplerOrigin

onready var xsm = $XSM

var velocity = Vector2()

var in_air = true

# This is set to 500 in this project
onready var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

#warning-ignore:unused_signal
signal grappler_disconnect

func on_grappler_success(node):
	xsm.change_state("Connecting", node)


func on_grappler_retract():
	xsm.change_state("CanGrapple")
