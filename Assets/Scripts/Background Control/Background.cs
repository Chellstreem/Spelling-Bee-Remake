using UnityEngine;
using Zenject;

namespace BackgroundControl
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Background : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _groundRenderer;
        private SpriteRenderer _spriteRenderer;
        private BackgroundConfig _config;

        [Inject]
        public void Construct(GameConfig gameConfig) => _config = gameConfig.BackgroundConfig;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            var info = _config.Infos[Random.Range(0, _config.Infos.Length)];
            SetBackground(info.BackgroundSprite, info.GroundMaterial);
        }

        public void SetBackground(Sprite backgroundSprite, Material groundMaterial)
        {
            _spriteRenderer.sprite = backgroundSprite;
            _groundRenderer.material = groundMaterial;
        }
    }
}