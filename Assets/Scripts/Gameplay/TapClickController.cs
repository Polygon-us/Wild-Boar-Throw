using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapClickController : MonoBehaviour
{
    private InputAction clickAction;

    private bool isClicking;

    public static Action OnClick;
    
    private void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }

    private void Update()
    {
        if (clickAction.WasPressedThisFrame())
        {
            isClicking = true;
            
            OnClick?.Invoke();
        }
        
        if (clickAction.WasReleasedThisFrame())
        {
            isClicking = false;
        }
    }
}
