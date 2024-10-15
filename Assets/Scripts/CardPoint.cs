using UnityEngine;

public class CardPoint : MonoBehaviour
{
    private bool _isFull;
    public bool IsFull()
    {
        return _isFull;
    }

    public void SetCard()
    {
        _isFull = true;
    }
}