using Sirenix.OdinInspector;
using UnityEngine;

namespace DP.Runtime
{
    /// <summary>
    /// Handles receiving input from input system and feeding it to the required classes.
    /// </summary>
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        private PlayerInput _inputActions;

        #region Player Inputs

        // Vector2 Values
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly]
        public Vector2 MoveInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly]
        public Vector2 LookInput { get; private set; }
        
        // Buttons
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState FireInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState AimInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState JumpInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState CrouchInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState SprintInput { get; private set; }
        [field: FoldoutGroup("Debugging"), SerializeField, ReadOnly, InlineProperty]
        public InputState InteractInput { get; private set; }

        #endregion

        private void Awake()
        {
            _inputActions = new PlayerInput();

            FireInput = new InputState(_inputActions.Player.Fire);
            AimInput = new InputState(_inputActions.Player.Aim);
            JumpInput = new InputState(_inputActions.Player.Jump);
            CrouchInput = new InputState(_inputActions.Player.Crouch);
            SprintInput = new InputState(_inputActions.Player.Sprint);
            InteractInput = new InputState(_inputActions.Player.Interact);
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void Update()
        {
            MoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            LookInput = _inputActions.Player.Look.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            FireInput.Reset();
            AimInput.Reset();
            JumpInput.Reset();
            CrouchInput.Reset();
            SprintInput.Reset();
            InteractInput.Reset();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}