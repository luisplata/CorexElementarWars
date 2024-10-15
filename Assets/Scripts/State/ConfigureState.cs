public class ConfigureState : BaseState
{
    private readonly IMediatorUi _mediatorUi;
    private readonly IMediatorInputHandler _mediatorInputHandler;
    private float _deltaTimeLocal;

    public ConfigureState(IMediatorUi mediatorUi, IMediatorInputHandler mediatorInputHandler)
    {
        _mediatorUi = mediatorUi;
        _mediatorInputHandler = mediatorInputHandler;
        _metaDataState = new MetaDataState
        {
            id = "configureState",
            isFirst = true,
            nextStateId = "SinchronizeState",
        };
        _teaTime.Pause().Add(() =>
        {
            _mediatorUi.ShowMessage("Configure State");
        }).Add(2);
    }
}