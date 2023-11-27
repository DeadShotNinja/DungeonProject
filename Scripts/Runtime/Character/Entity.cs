using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace DP.Runtime
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Entity : NetworkBehaviour
    {
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        
        private List<EntityComponent> _entityComponents = new();
        
        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponentInChildren<Animator>();
            
            EntityComponent[] components = GetComponentsInChildren<EntityComponent>();
            _entityComponents = components.ToList();
            
            foreach (EntityComponent component in _entityComponents)
            {
                component.OnInitialize(this);
            }
        }
        
        protected virtual void OnEnable()
        {
            
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            foreach (EntityComponent component in _entityComponents)
            {
                component.OnUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            foreach (EntityComponent component in _entityComponents)
            {
                component.OnFixedUpdate();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (EntityComponent component in _entityComponents)
            {
                component.OnKill();
            }
        }
        
        public T GetEntityComponent<T>() where T : EntityComponent
        {
            return _entityComponents.OfType<T>().FirstOrDefault();
        }
        
        public bool TryGetEntityComponent<T>(out T component) where T : EntityComponent
        {
            component = _entityComponents.OfType<T>().FirstOrDefault();
            return component != null;
        }
        
        protected T VerifyComponent<T>() where T : EntityComponent
        {
            if (TryGetEntityComponent(out T component))
                return component;
            
            Debug.LogError($"[Entity] Missing a critical component of type {typeof(T)}.", this);
            return null;
        }
    }
}
