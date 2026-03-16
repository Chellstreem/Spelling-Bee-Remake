using System;
using GameStates;
using UnityEngine;
using Zenject;

public class PlayerBehaviour : IEventSubscriber<OnLetterCollision>,
     IEventSubscriber<OnDeath>, IDisposable
{
    private readonly GameStateController _stateController;
    private readonly EventManager eventManager;
    private readonly IPlayerAnimationPlayer animations;
    private readonly IPhysicsModifier physics;
    private readonly Vector3 newColliderCenter;

    private readonly Rigidbody rigidBody;
    private readonly BoxCollider boxCollider;

    public PlayerBehaviour(GameStateController stateController,
        EventManager eventManager,
        IPlayerAnimationPlayer animations,
        IPhysicsModifier physics,

        [Inject(Id = InstantiatedObjectType.Player)] Transform playerTransform,
        PlayerBehaviourConfig config)
    {
        _stateController = stateController;
        this.eventManager = eventManager;
        this.animations = animations;
        this.physics = physics;

        newColliderCenter = config.NewColliderCenter;

        rigidBody = playerTransform.GetComponent<Rigidbody>();
        boxCollider = playerTransform.GetComponent<BoxCollider>();

        SubscribeToEvents();
    }

    private void OnLifeChanged(HealthChangeType changeType) => animations.Flinch();

    public void OnEvent(OnLetterCollision eventData) => animations.Flinch();

    private void OnStateChanged()
    {
        if (_stateController.CurrentState.StateType == GameStateType.Victory)
            animations.Dance();
    }

    public void OnEvent(OnDeath eventData)
    {
        animations.Die();
        FallDown();
    }

    public void Dispose() => UnsubscribeFromEvents();

    private void FallDown()
    {
        physics.SetGravity(rigidBody, true);
        physics.SetBoxColliderCenter(boxCollider, newColliderCenter);
    }

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnLetterCollision>(this);
        eventManager.Subscribe<OnDeath>(this);
        _stateController.OnStateChanged += OnStateChanged;
    }

    private void UnsubscribeFromEvents()
    {
        eventManager.Unsubscribe<OnLetterCollision>(this);
        eventManager.Unsubscribe<OnDeath>(this);

        _stateController.OnStateChanged -= OnStateChanged;
    }
}




