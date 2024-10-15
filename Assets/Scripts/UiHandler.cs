using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel;
    private IMediatorUi _mediatorOfGame;

    public void Configure(IMediatorUi mediatorOfGame)
    {
        _mediatorOfGame = mediatorOfGame;
        panel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        panel.SetActive(true);
        text.text = message;
    }

    public void HideMessage()
    {
        panel.SetActive(false);
        text.text = "";
    }
}