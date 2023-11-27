using Cinemachine;
using UnityEngine;

namespace DP.Runtime
{
    public class LookComponent : EntityComponent
    {
        [SerializeField] private float _mouseSens = 100f;
        //[SerializeField] private CinemachineVirtualCamera _virtualCamera;
        //private float _mouseXInput;
        //private float _mouseYInput;
        private float _xRotation;

        public override void OnInitialize(Entity entity)
        {
            base.OnInitialize(entity);

            // if (_virtualCamera == null)
            // {
            //     _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            //     Debug.LogError("[LookComponent] Virtual Camera was missing, attempting to locate it automatically...");
            // }
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //_mouseXInput = InputProvider.LookInput.x * _mouseSens * Time.deltaTime;
            //_mouseYInput = InputProvider.LookInput.y * _mouseSens * Time.deltaTime;

            //Rotate();
        }

        public void Rotate(float inputX, float inputY, CinemachineVirtualCamera virtualCamera)
        {
            float finalXInput = inputX * _mouseSens * Time.deltaTime;
            float finalYInput = inputY * _mouseSens * Time.deltaTime;
            _xRotation -= finalYInput;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            virtualCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            
            _entity.transform.Rotate(Vector3.up * finalXInput);
        }
    }
}
