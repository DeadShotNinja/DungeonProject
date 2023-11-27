using UnityEngine;

namespace DP.Runtime
{
    /// <summary>
    /// Interface abstraction between player and input.
    /// </summary>
    public interface IInputProvider
    {
        // Vectors
        public Vector2 MoveInput { get; }
        public Vector2 LookInput { get; }
        
        // Buttons
        public InputState FireInput { get; }
        public InputState AimInput { get; }
        public InputState JumpInput { get; }
        public InputState CrouchInput { get; }
        public InputState SprintInput { get; }
        public InputState InteractInput { get; }
    }
}