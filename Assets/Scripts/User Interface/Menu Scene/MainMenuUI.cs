using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class MainMenuUI : MonoBehaviour
{
    [Header("Кнопки")]
    [SerializeField] private Button addWordButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private FieldActivator fieldActivator;
    private GameStarter gameStarter;

    [Inject]
    public void Construct(FieldActivator fieldActivator, GameStarter gameStarter)
    {
        this.fieldActivator = fieldActivator;
        this.gameStarter = gameStarter;
    }

    private void Start()
    {
        AddListenersForButtons();
        UpdateButtonStates();
    }    
    
    private void OnAddWordButtonClicked()
    {
        fieldActivator.ActivateNextField();
        UpdateButtonStates();
    }

    private void OnDeleteWordButtonClicked()
    {
        fieldActivator.DeactivateLastField();
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        addWordButton.interactable = fieldActivator.ActiveFieldIndex < fieldActivator.FieldsLength - 1;
        deleteButton.interactable = fieldActivator.ActiveFieldIndex > 0;
    }

    private void AddListenersForButtons()
    {
        addWordButton.onClick.AddListener(OnAddWordButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteWordButtonClicked);
        startButton.onClick.AddListener(gameStarter.StartGame);
        exitButton.onClick.AddListener(GameQuitter.QuitGame);
    }
}

