using UnityEngine;

namespace DP.Runtime
{
    public class SuperGroundedState : PlayerMovementState
    {
        public SuperGroundedState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }
    }
}
