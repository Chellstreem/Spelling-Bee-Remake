using UnityEngine;
using Zenject;

public class PlayerHealthBarController : IInitializable
{
    private readonly IScaler scaler;
    private readonly GameObject healthBarObj;
    private readonly Transform healthBarTransform;
    private readonly Transform playerTransform; // Шкала здоровья будет дочерним объектом этого объекта
    private readonly Vector3 healthBarLocalPosition;

    public PlayerHealthBarController(IScaler scaler,
        [Inject(Id = InstantiatedObjectType.HealthBar)] Transform healthBarTransform,
        [Inject(Id = InstantiatedObjectType.Player)] Transform playerTransform,
        PlayerHealthConfig config)
    {
        this.scaler = scaler;
        this.healthBarTransform = healthBarTransform;
        healthBarObj = healthBarTransform.gameObject;
        this.playerTransform = playerTransform;
        healthBarLocalPosition = config.HealthBarLocalPosition;
    }

    public void Initialize()
    {
        SetParent();
        ToggleActivation(false);
        SubsribeToEvents();
    }



    private void ToggleActivation(bool isActive) => healthBarObj.SetActive(isActive);

    private void SetParent()
    {
        healthBarTransform.SetParent(playerTransform);
        healthBarTransform.localPosition = healthBarLocalPosition;
    }

    private void SubsribeToEvents()
    {

    }
}
