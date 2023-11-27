using UnityEngine;

namespace DP.Runtime
{
    public class JumpAirState : SuperAirState
    {
        public JumpAirState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }
    }
}
