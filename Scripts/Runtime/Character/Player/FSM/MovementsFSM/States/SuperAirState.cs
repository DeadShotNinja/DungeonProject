using UnityEngine;

namespace DP.Runtime
{
    public class SuperAirState : PlayerMovementState
    {
        public SuperAirState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }
    }
}
