using Sound;
using UnityEngine;
using Zenject;

public class SceneLauncher : MonoBehaviour
{
    [Inject] private GameplayController _gameplayController;

    private void Awake() => _gameplayController.StartGame();

}
