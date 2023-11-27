using UnityEngine;

namespace DP.Runtime
{
    public class PlayerMovementState : BaseState<PlayerMovementState>
    {
        protected PlayerEntity _player;
        protected IInputProvider _input;
        
        public PlayerMovementState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(stateMachine)
        {
            _player = player;
            
            if (GameManager.Instance != null)
                _input = GameManager.Instance.InputProvider;
            else
                Debug.LogError("[PlayerMovementState] No Movement Input assigned, GameManager was not instanced.");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _player.Look.Rotate(_input.LookInput.x, _input.LookInput.y, _player.VirtualCamera);
        }
    }
}
