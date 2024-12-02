using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapClickController : MonoBehaviour
{
    private InputAction clickAction;
    
    public static Action OnClick;
    
    private void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }

    private void Update()
    {
        if (clickAction.WasPressedThisFrame())
        {
            OnClick?.Invoke();
        }
        
        if (clickAction.WasReleasedThisFrame())
        {
        }
    }
}
