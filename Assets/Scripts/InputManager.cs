using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;

    public static Vector2 mouseTouchPosition;
    public static bool wasLeftButtonPressed;
    public static bool wasLeftButtonReleased;
    public static bool isLeftButtonPressed;

    private InputAction mouseTouchPosAction;
    private InputAction mouseTouchAction;

    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();

        mouseTouchPosAction = playerInput.actions["MouseTouchPosition"];
        mouseTouchAction = playerInput.actions["MouseTouch"];
    }

    private void Update() 
    {
        mouseTouchPosition = mouseTouchPosAction.ReadValue<Vector2>();

        wasLeftButtonPressed = mouseTouchAction.WasPerformedThisFrame();
        wasLeftButtonReleased = mouseTouchAction.WasReleasedThisFrame();
        isLeftButtonPressed = mouseTouchAction.IsPressed();
    }

}
