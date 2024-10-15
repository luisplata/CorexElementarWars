public class SinchronizeState : BaseState
{
    private readonly IMediatorUi _mediatorUi;
    private float _deltaTimeLocal;

    public SinchronizeState(IMediatorUi mediatorUi)
    {
        _mediatorUi = mediatorUi;
        _metaDataState = new MetaDataState { id = "SinchronizeState", nextStateId = "GameplayState" };
        _teaTime.Pause().Add(() =>
        {
            _mediatorUi.ShowMessage("Sinchronize State");
        }).Add(2);
    }
}