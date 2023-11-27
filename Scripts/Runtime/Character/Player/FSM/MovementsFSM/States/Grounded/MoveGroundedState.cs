using UnityEngine;

namespace DP.Runtime
{
    public class MoveGroundedState : SuperGroundedState
    {
        public MoveGroundedState(PlayerEntity player, StateMachine<PlayerMovementState> stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _player.Animator.SetBool(_player.AnimatorParams.WalkBool, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (ShouldIdle())
            {
                _player.MovementHSM.ChangeState(_player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            _player.Movement.Move(_input.MoveInput, _player.VirtualCamera);
        }

        public override void Exit()
        {
            base.Exit();
            
            _player.Animator.SetBool(_player.AnimatorParams.WalkBool, false);
        }
        
        private bool ShouldIdle()
        {
            return _input.MoveInput is { x: 0f, y: 0f };
        }
    }
}
