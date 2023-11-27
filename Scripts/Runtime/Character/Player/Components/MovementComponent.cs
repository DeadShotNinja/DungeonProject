using Cinemachine;
using UnityEngine;

namespace DP.Runtime
{
    public class MovementComponent : EntityComponent
    {
        [SerializeField] private float _walkSpeed = 5f;
        
        
        public void Move(Vector2 moveVector, CinemachineVirtualCamera virtualCamera)
        {
            Transform vcTransform = virtualCamera.transform;
            Vector3 forward = vcTransform.forward;
            Vector3 right = vcTransform.right;
        
            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();
        
            Vector3 direction = (forward * moveVector.y + right * moveVector.x).normalized;
            direction *= _walkSpeed * Time.fixedDeltaTime;
            
            _entity.CharacterController.Move(direction);
        }
    }
}
