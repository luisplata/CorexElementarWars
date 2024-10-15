using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private IMediatorInputHandler _mediatorOfGame;
    private bool _canTouch;

    public void Configure(IMediatorInputHandler mediatorOfGame)
    {
        _mediatorOfGame = mediatorOfGame;
    }

    public void CanTouch(bool canTouch)
    {
        _canTouch = canTouch;
    }

    public bool GetCanTouch()
    {
        return _canTouch;
    }
}