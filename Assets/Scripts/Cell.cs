using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    [SerializeField] private TypeOfCell typeOfCell;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private Position _position;

    public Action<Cell> OnCellClickedDown;
    public Action OnCellClickedUp;
    private IMediatorInputHandler _inputHandler;
    private bool _isSelect;

    [ContextMenu("Reset Cell")]
    private void ResetOption()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Reset()
    {
        ResetOption();
    }

    public void Configure(Position position, IMediatorInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        name = $"Cell {position.x} {position.y}";
        _position = position;
        spriteRenderer.color = Global.GetColor(typeOfCell);
    }

    public void OnMouseDown()
    {
        if (!_inputHandler.GetCanTouch()) return;
        OnCellClickedDown?.Invoke(this);
    }

    public void OnMouseUp()
    {
        if (!_inputHandler.GetCanTouch()) return;
    }

    public void IsSelect(bool isSelect)
    {
        _isSelect = isSelect;
    }

    public bool IsSelect()
    {
        return _isSelect;
    }
}