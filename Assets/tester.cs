using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using Zenject;

public class tester : MonoBehaviour
{
    private GameStateController _gameStateController;

    [Inject]
    public void Construct(GameStateController gameStateController)
    {
        _gameStateController = gameStateController;
    }

    private void Start()
    {
        _gameStateController.SetState(GameStateType.Interactive);
    }
}
