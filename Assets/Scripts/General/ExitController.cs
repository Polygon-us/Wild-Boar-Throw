using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEditor;
using UI.PopUp;
using System;
using Utils;

namespace General
{
    public class ExitController : MonoBehaviourSingleton<ExitController>
    {
        #region Information

        protected override bool Persistent => true;

        private InputAction exitAction;

        #endregion

        private static readonly List<Action> backActions = new List<Action>();

        private void Start()
        {
            exitAction = InputSystem.actions.FindAction("Exit");
            exitAction.Enable();
            exitAction.started += OnBackClicked;
        }

        private void OnDisable()
        {
            exitAction.performed -= OnBackClicked;
            exitAction.Disable();
        }

        private void OnBackClicked(InputAction.CallbackContext context)
        {
            if (backActions.Count > 0)
                backActions[^1]?.Invoke();
        }

        public static void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
        
        public static void AddAction(Action action)
        {
            _ = Instance;
            backActions.Add(action);
        }

        public static void RemoveAction(Action action)
        {
            int index = backActions.LastIndexOf(action);
            if (index != -1)
                backActions.RemoveAt(index);
        }

        public static void AddNone()
        {
            AddAction(None);
        }

        public static void RemoveNone()
        {
            RemoveAction(None);
        }

        private static void None()
        {
        }
    }
}