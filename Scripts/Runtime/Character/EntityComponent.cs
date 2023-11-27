using UnityEngine;

namespace DP.Runtime
{
    public abstract class EntityComponent : MonoBehaviour
    {
        protected Entity _entity;
        
        public virtual void OnInitialize(Entity entity)
        {
            _entity = entity;
        }

        public virtual void OnUpdate()
        {
            
        }
        
        public virtual void OnFixedUpdate()
        {
            
        }
        
        public virtual void OnKill()
        {
            
        }
    }
}
