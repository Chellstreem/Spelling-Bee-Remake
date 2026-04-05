using System.Collections;
using SceneControl;
using UnityEngine;
using Zenject;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private float _sceneDuration;
    private CoroutineRunner _runner;
    private SceneController _sceneController;

    [Inject]
    public void Construct(CoroutineRunner runner, SceneController sceneController)
    {
        _runner = runner;
        _sceneController = sceneController;
    }

    private void Start() => _runner.Run(IntroCoroutine());

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(_sceneDuration);
        _sceneController.SwitchScene(SceneType.MainMenu);
    }
}




