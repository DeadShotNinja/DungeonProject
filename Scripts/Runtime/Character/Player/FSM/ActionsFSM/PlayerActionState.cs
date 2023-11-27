using UnityEngine;

namespace DP.Runtime
{
    public class PlayerActionState : BaseState<PlayerActionState>
    {
        protected PlayerEntity _player;
        
        public PlayerActionState(PlayerEntity player, StateMachine<PlayerActionState> stateMachine) : base(stateMachine)
        {
            _player = player;
        }
    }
}
