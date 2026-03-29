using Units;
using UnityEngine;

namespace Sound
{
    public class UnitSoundInvoker : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        private void Awake() => _collider.isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Unit>(out var unit))
                return;

            unit.InvokeUnitSound();
        }
    }
}