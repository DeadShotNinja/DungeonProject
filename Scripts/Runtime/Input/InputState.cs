using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DP.Runtime
{
    /// <summary>
    /// Object to create different input states for buttons (Pressed, Held, Released)
    /// and register them to an InputAction listener.
    /// </summary>
    [Serializable]
    public class InputState
    {
        [field: BoxGroup("States"), SerializeField, ReadOnly]
        public bool Pressed { get; private set; }
        [field: BoxGroup("States"), SerializeField, ReadOnly]
        public bool Held { get; private set; }
        [field: BoxGroup("States"), SerializeField, ReadOnly]
        public bool Released { get; private set; }

        public InputState(InputAction inputAction)
        {
            inputAction.started += ctx => OnStarted();
            inputAction.performed += ctx => OnPerformed();
            inputAction.canceled += ctx => OnCanceled();
        }

        public InputState() { }

        public void OnStarted() => Held = true;
        public void OnPerformed() => Pressed = true;
        public void OnCanceled()
        {
            Held = false;
            Released = true;
        }

        public void Reset()
        {
            Pressed = false;
            Released = false;
        }
    }
}