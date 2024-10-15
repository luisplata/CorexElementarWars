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
        StartCoroutine(MoveToPosition());
    }

    private IEnumerator MoveToPosition()
    {
        // move the gameobject to the assigned position
        while (Vector3.Distance(transform.position, _cardPoint.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, _cardPoint.transform.position, Time.deltaTime * 5);
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
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                cell.OnMouseDown();
            }
        }
        else
        {
            transform.localScale = _originalScale;
        }
    }

    public void OnMouseUp()
    {
        // Check if stay in any cell
    }
}