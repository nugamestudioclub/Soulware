using Godot;
using System;
using System.Collections.Generic;

public class InputBinder : Node
{
    /*
        TODO:
        //ADD ACTIONS

        //WHEN binding or init ADD ACTION EVENT
     
     
     
     
     */






    //all of the define inputs
    enum InputAction
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Jump,
        Fire,
        Grapple,
    }

    //Each device that can be used for input binding
    enum InputDeviceType
    {
        None,
        Keyboard,
        Mouse,
        ControllerButton,
        ControllerMotion,
    }

    //Tuple struct containing type of device and corresponding code (keycode, mouse button #, etc.)
    struct InputDeviceInfo
    {
        private readonly InputDeviceType type;
        public InputDeviceType Type => type;

        private readonly int code;
        public int Code => code;

        public InputDeviceInfo(InputDeviceType type, int code)
        {
            this.type = type;
            this.code = code;
        }
    }

    //Tuple struct with Action and Index representing primary and secondary control schemes
    struct ActionBinding
    {
        private readonly InputAction action;
        public InputAction Action => action;

        private readonly int index;
        public int Index => index;

        public ActionBinding(InputAction action, int index)
        {
            this.action = action;
            this.index = index;
        }
    }

    private ActionBinding currentAction;
    private bool BindingInProgress => currentAction.Action != InputAction.None;
    private void FinishBinding()
    {
        currentAction = new ActionBinding(InputAction.None, 0);
    }

    public override void _Input(InputEvent inputEvent)
    {
        //if we are in change binding state
        if (!BindingInProgress)
            return;
        if (inputEvent is InputEventKey keyEvent)
        {
            //get the current key pressed and 
            //bind it to the action pressed 
            //keyEvent.Scancode;
            //set it up in godot
            Bind(new InputDeviceInfo(InputDeviceType.Keyboard, (int)keyEvent.Scancode));
        }
        else if (inputEvent is InputEventMouseButton mouseEvent)
        {
            Bind(new InputDeviceInfo(InputDeviceType.Mouse, mouseEvent.ButtonIndex));
        }
        else if (inputEvent is InputEventJoypadButton controllerButtonEvent)
        {
            Bind(new InputDeviceInfo(InputDeviceType.ControllerButton, 
                controllerButtonEvent.ButtonIndex));
        }
        else if (inputEvent is InputEventJoypadMotion controllerMotionEvent)
        {
            Bind(new InputDeviceInfo(InputDeviceType.ControllerButton,
                controllerMotionEvent.Axis));
        }
        // If keyboard
        // If mouse 

        FinishBinding();
    }

    //TODO: If old key is used, swap it with new key
    private void Bind(InputDeviceInfo info)
    {
        //release old binding

        //set new binding
    }
}
