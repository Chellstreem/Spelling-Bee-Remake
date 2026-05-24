using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SceneControl
{
    public class IntroScene : MonoBehaviour
    {
        [Tooltip("Duration in seconds the intro scene stays visible before loading next scene")]
        [SerializeField] private float _sceneDuration = 3f;

        private SceneController _sceneController;

        [Inject]
        public void Construct(SceneController sceneController) => _sceneController = sceneController;
        private void Start() => PlayIntroAsync().Forget();

        private async UniTaskVoid PlayIntroAsync()
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(_sceneDuration));
            _sceneController.LoadScene(SceneType.MainMenu);
        }
    }
}