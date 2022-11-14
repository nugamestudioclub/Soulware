extends Node2D

signal grapple_success
signal grapple_failure

var grappled = false

func _on_CollisionArea_body_entered(body):
	if not grappled:
		grappled = true
		print("Body Entered")
		emit_signal("grapple_success", body)


func _on_Timer_timeout():
	emit_signal("grapple_failed")

