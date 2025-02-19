using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    public GameObject jugador; // Declarar variables dentro de la clase
    private Vector3 distancia; // "private" es opcional, pero mejora el encapsulamiento

    // Start is called before the first frame update
    void Start()
    {
        if (jugador == null)
        {
            Debug.LogError("No se asignó el objeto jugador en el inspector.");
            return;
        }

        distancia = transform.position - jugador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null) // Verificar que el jugador no sea null
        {
            transform.position = jugador.transform.position + distancia;
        }
    }
}
