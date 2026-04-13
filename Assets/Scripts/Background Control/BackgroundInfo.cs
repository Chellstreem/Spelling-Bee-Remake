using UnityEngine;

namespace BackgroundControl
{
    [System.Serializable]
    public struct BackgroundInfo
    {
        [Tooltip("Type identifier for this background entry")]
        [SerializeField] private BackgroundType _type;
        [Tooltip("Sprite used as the background image for this entry")]
        [SerializeField] private Sprite _backgroundSprite;
        [Tooltip("Material used for the ground or foreground elements for this background")]
        [SerializeField] private Material _groundMaterial;

        public readonly BackgroundType Type => _type;
        public readonly Sprite BackgroundSprite => _backgroundSprite;
        public readonly Material GroundMaterial => _groundMaterial;
    }
}