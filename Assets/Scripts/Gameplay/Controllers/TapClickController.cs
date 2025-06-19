using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Gameplay.Controllers
{
    public class TapClickController : MonoBehaviour
    {
        private InputAction clickAction;

        public static Action OnClick;

        private void Start()
        {
            clickAction = InputSystem.actions.FindAction("Click");
            clickAction.started += Click;
        }

        private static void Click(InputAction.CallbackContext context)
        {
            OnClick?.Invoke();
        }

        private void OnDisable()
        {
            clickAction.performed -= Click;
        }
    }
}
