using UnityEngine;

public class MediatorOfGame : MonoBehaviour, IMediatorMap, IMediatorLogicOfGame, IMediatorInputHandler, IMediatorUi,
    IMediatorGame, IMediatorDeck
{
    [SerializeField] private Map map;
    [SerializeField] private LogicOfGame logicOfGame;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private UiHandler uiHandler;
    [SerializeField] private Deck deck;


    private void Start()
    {
        map.Configurate(this);
        logicOfGame.Configure(this);
        inputHandler.Configure(this);
        uiHandler.Configure(this);
        deck.Configure(this);
    }

    public IMediatorInputHandler GetInputHandler()
    {
        return this;
    }

    public IMediatorUi GetUiHandler()
    {
        return this;
    }

    public void ShowMessage(string message)
    {
        uiHandler.ShowMessage(message);
    }

    public void HideMessage()
    {
        uiHandler.HideMessage();
    }

    public void CanTouch(bool canTouch)
    {
        inputHandler.CanTouch(canTouch);
    }

    public bool GetCanTouch()
    {
        return inputHandler.GetCanTouch();
    }
}

public interface IMediatorGame
{
}

public interface IMediatorMap
{
    IMediatorInputHandler GetInputHandler();
}

public interface IMediatorLogicOfGame
{
    IMediatorInputHandler GetInputHandler();
    IMediatorUi GetUiHandler();
}

public interface IMediatorInputHandler
{
    void CanTouch(bool canTouch);
    bool GetCanTouch();
}