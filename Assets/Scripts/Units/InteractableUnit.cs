using System;
using Sound;
using Spawn;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    public enum InteractableType
    {
        Letter, HostileAnimal, Player, Missile
    }

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InteractableUnit : MonoBehaviour
    {
        [SerializeField] private UnitType _unitType;

        [Header("Sound")]
        [SerializeField] protected SoundUnit _characterSound;

        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected ObjectPool _objectPool;
        private ParticlePlayer _particlePlayer;

        public abstract InteractableType InteractableType { get; }
        public UnitType UnitType => _unitType;

        public event Action OnDeath;
        public event Action OnAttack;
        public event Action OnDance;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        [Inject]
        public virtual void Construct(ObjectPool objectPool, ParticlePlayer particlePlayer)
        {
            _objectPool = objectPool;
            _particlePlayer = particlePlayer;
        }

        public virtual void InvokeCharacterSound()
        {
            if (_characterSound == null)
                return;

            _characterSound.PlayOneShot();
        }

        protected abstract void HandleCollision(InteractableUnit other);
        protected virtual void InvokeDeath() => OnDeath?.Invoke();
        protected virtual void InvokeAttack() => OnAttack?.Invoke();
        protected virtual void InvokeDance() => OnDance?.Invoke();

        public ParticleSystem PlayEffect(ParticleEffectInfo info)
        {
            Vector3 position = transform.position + info.Offset;
            return _particlePlayer.Play(info.Type, position, info.Scale);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableUnit interactable))
                HandleCollision(interactable);
        }
    }
}