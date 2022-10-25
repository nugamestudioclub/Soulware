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
        Controller,
    }

    //Tuple struct containing type of device and corresponding code (keycode, mouse button #, etc.)
    struct InputDeviceInfo
    {
        public InputDeviceType Type { get; set; }
        public int Code { get; set; }
    }

    //Tuple struct with Action and Index representing primary and secondary control schemes
    struct ActionBinding
    {
        public readonly InputAction Action;
        public readonly int Index;

        public ActionBinding(InputAction action, int index)
        {
            Action = action;
            Index = index;
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
