using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Card : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private IMediatorInputHandler _inputHandler;
    private IDeck _deck;
    private bool _isReadyToMove;
    private CardPoint _cardPoint;
    private Vector3 _originalScale;
    private Cell _cell;

    private void Reset()
    {
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Configure(IMediatorInputHandler inputHandler, IDeck deck, CardPoint cardPoint)
    {
        _inputHandler = inputHandler;
        _deck = deck;
        _cardPoint = cardPoint;
        mainCamera = Camera.main;
        _originalScale = transform.localScale;
        StartCoroutine(MoveToPosition(_cardPoint.transform.position));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        _isReadyToMove = false;
        // move the gameobject to the assigned position
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 5);
            yield return null;
        }

        _isReadyToMove = true;
    }

    public void OnMouseDown()
    {
        if (!_inputHandler.GetCanTouch() || !_isReadyToMove) return;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        Debug.Log("OnMouseDown");
    }

    public void OnMouseDrag()
    {
        if (!_inputHandler.GetCanTouch() || !_isReadyToMove) return;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z) + offset;
        // check if stay in any cell
        var hits = Physics2D.RaycastAll(mouseWorldPosition, Vector2.zero);

        var isValidPosition = false;

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                _cell = hit.collider.GetComponent<Cell>();
                if (_cell != null)
                {
                    transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    _cell.OnMouseDown();
                    isValidPosition = true;
                }
            }
        }

        if (!isValidPosition)
        {
            transform.localScale = _originalScale;
            _cell = null;
        }
    }

    public void OnMouseUp()
    {
        // Check if stay in any cell
        if (_cell != null)
        {
            if (!_cell.IsFull() || _cell.IsEqualCard(this))
            {
                StartCoroutine(MoveToPosition(_cell.transform.position));
                _cell.IsFull(true);
                _cell.SetCard(this);
                return;
            }
        }

        StartCoroutine(MoveToPosition(_cardPoint.transform.position));
        transform.localScale = _originalScale;
    }
}