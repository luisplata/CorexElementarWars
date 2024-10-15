public class CreateCards : BaseState
{
    private readonly IMediatorDeck _mediatorDeck;

    public CreateCards(IMediatorDeck mediatorDeck)
    {
        _mediatorDeck = mediatorDeck;
        _teaTime.Pause().Add(() =>
        {
            //_mediatorDeck.FillCards();
        }).Add(1);
    }
}