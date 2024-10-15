public class GameState : BaseState
{
    private readonly IMediatorInputHandler _inputHandler;
    private readonly IMediatorUi _mediatorUi;
    private float _deltaTimeLocal;
    private int _countDown = 3;

    public GameState(IMediatorInputHandler inputHandler, IMediatorUi mediatorUi)
    {
        _inputHandler = inputHandler;
        _mediatorUi = mediatorUi;
        _metaDataState = new MetaDataState { id = "GameplayState" };
        mediatorUi.ShowMessage($"Start Game In: {_countDown}");
        _teaTime.Pause().Loop(handler =>
        {
            _deltaTimeLocal += handler.deltaTime;
            if (_deltaTimeLocal > 1)
            {
                _deltaTimeLocal = 0;
                _countDown--;
                mediatorUi.ShowMessage($"Start Game In: {_countDown}");
            }

            if (_countDown <= 0)
            {
                handler.Break();
                mediatorUi.HideMessage();
            }
        }).Add(() => { _inputHandler.CanTouch(true); });
    }
}