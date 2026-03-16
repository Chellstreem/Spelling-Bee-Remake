using System.Collections;
using System.Collections.Generic;
using GameStates;
using InputControl;
using UnityEngine;
using Zenject;

public class tester : MonoBehaviour
{
    private GameStateController _gameStateController;
    private IInput _input;

    [Inject]
    public void Construct(GameStateController gameStateController, IInput input)
    {
        _gameStateController = gameStateController;
        _input = input;
    }

    private void Start()
    {
        _gameStateController.SetState(GameStateType.Countdown);

        _input.OnGameOver += OnEscape;


    }

    private void OnEscape()
    {
        _gameStateController.SetState(GameStateType.Missile);
    }



}
