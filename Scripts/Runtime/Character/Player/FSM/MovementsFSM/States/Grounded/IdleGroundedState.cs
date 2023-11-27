using UnityEngine;

namespace DP.Runtime
{
    public class IdleGroundedState : SuperGroundedState
    {
        public IdleGroundedState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (ShouldMove())
            {
                _player.MovementHSM.ChangeState(_player.MoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool ShouldMove()
        {
            return _input.MoveInput.x != 0f || _input.MoveInput.y != 0f;
        }
    }
}
