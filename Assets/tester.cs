using System.Collections;
using System.Collections.Generic;
using GameStates;
using InputControl;
using Units;
using UnityEngine;
using Zenject;

public class tester : MonoBehaviour
{
    private GameStateController _gameStateController;
    private IInput _input;
    [SerializeField] private Player _player;

    [Inject]
    public void Construct(GameStateController gameStateController, IInput input)
    {
        _gameStateController = gameStateController;
        _input = input;
    }

    private void Start()
    {
        _gameStateController.SetState(GameStateType.Countdown);

        _input.OnGameOver += OnGameOver;


    }

    private void OnGameOver()
    {
        _player.StatusController.SetStatus(UnitStatusType.Invincible, 5f);
    }
}
