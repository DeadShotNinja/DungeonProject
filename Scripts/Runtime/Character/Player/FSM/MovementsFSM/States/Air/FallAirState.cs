using UnityEngine;

namespace DP.Runtime
{
    public class FallAirState : SuperAirState
    {
        public FallAirState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }
    }
}
