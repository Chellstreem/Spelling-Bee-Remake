using UnityEngine;

namespace BackgroundControl
{
    [System.Serializable]
    public struct BackgroundInfo
    {
        [SerializeField] private BackgroundType _type;
        [SerializeField] private Sprite _backgroundSprite;
        [SerializeField] private Material _groundMaterial;

        public readonly BackgroundType Type => _type;
        public readonly Sprite BackgroundSprite => _backgroundSprite;
        public readonly Material GroundMaterial => _groundMaterial;
    }
}