using Cinemachine;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

namespace DP.Runtime
{
    public class PlayerEntity : Entity
    {
        [field: FoldoutGroup("Animator Params"), SerializeField, HideLabel]
        public AnimatorParams AnimatorParams { get; private set; }
        
        public CinemachineVirtualCamera VirtualCamera { get; private set; }

        // TODO: Might be better to init this on entity creation.
        private IInputProvider _inputProvider;
        public IInputProvider InputProvider => _inputProvider ??= GameManager.Instance.InputProvider;

        // Components
        public LookComponent Look { get; private set; }
        public MovementComponent Movement { get; private set; }
        // State Machines
        public StateMachine<PlayerMovementState> MovementHSM { get; private set; }
        public StateMachine<PlayerActionState> ActionFSM { get; private set; }
        // Movement States
        public IdleGroundedState IdleState { get; private set; }
        public MoveGroundedState MoveState { get; private set; }
        public JumpAirState JumpState { get; private set; }
        public FallAirState FallState { get; private set; }
        // Action States
        

        protected override void Awake()
        {
            base.Awake();

            VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            
            // Get/Verify Critical Components
            Look = VerifyComponent<LookComponent>();
            Movement = VerifyComponent<MovementComponent>();

            // Init State Machines
            MovementHSM = new StateMachine<PlayerMovementState>();
            ActionFSM = new StateMachine<PlayerActionState>();
            // Init Movement States
            IdleState = new IdleGroundedState(this, MovementHSM);
            MoveState = new MoveGroundedState(this, MovementHSM);
            JumpState = new JumpAirState(this, MovementHSM);
            FallState = new FallAirState(this, MovementHSM);
            // Init Action States
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            MovementHSM.Initialize(IdleState);
            // TODO: need to initialize action FSM
        }

        protected override void Update()
        {
            base.Update();
            
            MovementHSM.CurrentState.LogicUpdate();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            MovementHSM.CurrentState.PhysicsUpdate();
        }
        
        public void SetupForNetwork(ulong clientID)
        {
            if (GetComponent<NetworkObject>().OwnerClientId != clientID)
            {
                VirtualCamera.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
