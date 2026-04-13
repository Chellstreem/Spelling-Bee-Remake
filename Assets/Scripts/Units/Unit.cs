using Sound;
using Spawn;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Unit : MonoBehaviour
    {
        [Tooltip("Type identifier for this unit used in collision and spawn logic")]
        [SerializeField] protected UnitType _unitType;
        [Tooltip("List of unit types this unit can interact with via collisions")]
        [SerializeField] private UnitType[] _allowedCollisions;
        [Tooltip("Optional sound played by this unit when triggered")]
        [SerializeField] protected SoundUnit _unitSound;

        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected UnitPool _objectPool;
        protected ParticlePlayer _particlePlayer;
        protected CoroutineRunner _coroutineRunner;

        public UnitType UnitType => _unitType;
        public bool IsInteractable { get; protected set; } = true;

        [Inject]
        public virtual void Construct(UnitPool objectPool, ParticlePlayer particlePlayer, CoroutineRunner coroutineRunner)
        {
            _objectPool = objectPool;
            _particlePlayer = particlePlayer;
            _coroutineRunner = coroutineRunner;
        }

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public abstract void HandleCollision(Unit other);
        public abstract void Damage(int damage);

        public virtual void InvokeUnitSound()
        {
            if (_unitSound == null) return;
            _unitSound.PlayOneShot();
        }

        public ParticleSystem ApplyParticleEffect(ParticleEffectInfo info, Transform parent = null)
        {
            Vector3 position = info.IsOffset ? transform.position + info.Position : info.Position;
            var particle = _particlePlayer.Play(info.Type, position, info.Scale);

            if (parent != null)
                particle.transform.SetParent(parent);

            return particle;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Unit unit))
                return;

            foreach (var type in _allowedCollisions)
            {
                if (type == unit.UnitType)
                {
                    HandleCollision(unit);
                    return;
                }
            }
        }
    }
}