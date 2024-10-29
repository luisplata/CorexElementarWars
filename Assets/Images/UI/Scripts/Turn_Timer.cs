using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float tiempoRestante = 45f;
    public TextMeshProUGUI turn_timer; //Nombre del textMeshpro

    private bool temporizadorActivo = false;

    void Start()
    {
        if (turn_timer == null)
        {
            Debug.LogError("TextMeshPro no está asignado en el inspector.");
            return;
        }

        IniciarTemporizador();
    }

    void Update()
    {
        if (temporizadorActivo)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                ActualizarTexto();
            }
            else
            {
                tiempoRestante = 0;
                temporizadorActivo = false;
                ActualizarTexto(); 

                Debug.Log("Turn Ended");
            }
        }
    }

    public void IniciarTemporizador()
    {
        tiempoRestante = 45f;
        temporizadorActivo = true;
    }

    private void ActualizarTexto()
    {
        int segundos = Mathf.CeilToInt(tiempoRestante);
        turn_timer.text = segundos.ToString();
    }
}

