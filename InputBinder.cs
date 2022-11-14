using Godot;
using System;
using System.Collections.Generic;
using System.Configuration;
using static System.Collections.Specialized.BitVector32;

public class InputBinder : Node
{
    /*
        TODO:
        //ADD ACTIONS

        //WHEN binding or init ADD ACTION EVENT
     
     
     
     
     */

    //data structure to hold all the bindings
    private BindingEntry[][] bindings;



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
    struct BindingEntry
    {
        private readonly InputDeviceType deviceType;
        public InputDeviceType DeviceType => deviceType;

        private readonly int deviceCode;
        public int DeviceCode => deviceCode;

        private readonly InputAction action;
        public InputAction Action => action;
        public BindingEntry(InputDeviceType deviceType, int deviceCode, InputAction action)
        {
            this.deviceType = deviceType;
            this.deviceCode = deviceCode;
            this.action = action;
        }
    }

    //Tuple struct with Binding and Index representing primary and secondary control schemes
    struct ActionInfo
    {
        private readonly InputAction action;
        public InputAction Action => action;

        private readonly int index;
        public int Index => index;

        public ActionInfo(InputAction action, int index)
        {
            this.action = action;
            this.index = index;
        }
    }

    private ActionInfo currentAction;
    private bool BindingInProgress => currentAction.Action != InputAction.None;

    public override void _Ready()
    {
        //Grab control bindings from save or get defaults
        //load them into the BindingEntry list
        bindings = GetDefaultBindings();
        
        //get all gui nodes
        //connect startbinding to signal
    }
    
    private BindingEntry[][] GetDefaultBindings() {
        return new BindingEntry[][]{
            GetDefaultBindingForAction(InputAction.MoveUp),
            GetDefaultBindingForAction(InputAction.MoveDown),
            GetDefaultBindingForAction(InputAction.MoveLeft),
            GetDefaultBindingForAction(InputAction.MoveRight),
            GetDefaultBindingForAction(InputAction.Jump),
            GetDefaultBindingForAction(InputAction.Fire),
            GetDefaultBindingForAction(InputAction.Grapple),
        };
    }


    private static BindingEntry[] GetDefaultBindingForAction(InputAction action) {
        int primaryCode;
        int secondaryCode;

        switch (action) {
            case InputAction.MoveUp:
                primaryCode = (int)KeyList.W;
                secondaryCode = (int)JoystickList.DpadUp;
                break;
            case InputAction.MoveDown:
                primaryCode = (int)KeyList.S;
                secondaryCode = (int)JoystickList.DpadDown;
                break;
            case InputAction.MoveLeft:
                primaryCode = (int)KeyList.A;
                secondaryCode = (int)JoystickList.DpadLeft;
                break;
            case InputAction.MoveRight:
                primaryCode = (int)KeyList.D;
                secondaryCode = (int)JoystickList.DpadRight;
                break;
            case InputAction.Jump:
                primaryCode = (int)KeyList.Space;
                secondaryCode = (int)JoystickList.XboxA;
                break;
            case InputAction.Fire:
                primaryCode = (int)KeyList.Q;
                secondaryCode = (int)JoystickList.XboxX;
                break;
            case InputAction.Grapple:
                primaryCode = (int)KeyList.E;
                secondaryCode = (int)JoystickList.XboxY;
                break;
            default:
                primaryCode = -1;
                secondaryCode = -1;
                GD.Print($"Invalid InputAction:{action}");
                break;
        }
        return new BindingEntry[] {
            new BindingEntry(InputDeviceType.Keyboard, primaryCode, action),
            new BindingEntry(InputDeviceType.ControllerButton, secondaryCode, action)
        };
    }

    private void StartBinding(InputAction action, int index)
    {
        currentAction = new ActionInfo(action, index);
    }

    public override void _Input(InputEvent inputEvent)
    {
        //if we are in change binding state
        if (BindingInProgress)
        {
            Bind(inputEvent);
            FinishBinding();
        }
    }

    //TODO: If old key is used, swap it with new key
    private void Bind(InputEvent inputEvent)
    {
        //get the current key pressed and 
        //bind it to the action pressed 
        var (deviceType, deviceCode) = GetDeviceInfo(inputEvent);
        //access current action

        //set it up in godot
        //release old binding

        //set new binding




    }
    private (InputDeviceType deviceType, int deviceCode) GetDeviceInfo(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
            return (InputDeviceType.Keyboard, (int)keyEvent.Scancode);
        else if (inputEvent is InputEventMouseButton mouseEvent)
            return (InputDeviceType.Mouse, mouseEvent.ButtonIndex);
        else if (inputEvent is InputEventJoypadButton controllerButtonEvent)
            return (InputDeviceType.ControllerButton, controllerButtonEvent.ButtonIndex);
        else if (inputEvent is InputEventJoypadMotion controllerMotionEvent)
        {
            //need to parse what joystick and what direction
            return (InputDeviceType.ControllerButton, controllerMotionEvent.Axis);
        }
        return (InputDeviceType.None, deviceCode: 0);
    }

    private void FinishBinding()
    {
        currentAction = new ActionInfo(InputAction.None, index: 0);
    }
}
