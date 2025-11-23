using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool up1 = false;
    private bool down1 = false;
    private bool up2 = false;
    private bool down2 = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one InputManager in the scene");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void Player1UpPressed(InputAction.CallbackContext context)
    {
        up1 = context.ReadValueAsButton();
    }

    public void Player1DownPressed(InputAction.CallbackContext context)
    {
        down1 = context.ReadValueAsButton();
    }

    public int Player1GetDirection()
    {
        if (up1)
            return 1;
        else if (down1)
            return -1;
        else
            return 0;
    }

    public void Player2UpPressed(InputAction.CallbackContext context)
    {
        up2 = context.ReadValueAsButton();
    }

    public void Player2DownPressed(InputAction.CallbackContext context)
    {
        down2 = context.ReadValueAsButton();
    }

    public int Player2GetDirection()
    {
        if (up2)
            return 1;
        else if (down2)
            return -1;
        else
            return 0;
    }
}
