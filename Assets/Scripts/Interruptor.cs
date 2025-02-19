using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public GameObject puerta; // La puerta que se abrirá
    public float tiempoEspera = 0.5f; // Tiempo de espera antes de que la puerta desaparezca (puedes ajustar este tiempo)

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que toca el interruptor es un proyectil (o el tag que has asignado al proyectil)
        if (other.CompareTag("Proyectil"))
        {
            // Desactiva la puerta (desaparece)
            StartCoroutine(DesaparecerPuerta());
        }
    }

    IEnumerator DesaparecerPuerta()
    {
        // Espera un tiempo antes de desactivar la puerta
        yield return new WaitForSeconds(tiempoEspera);

        // Desactiva la puerta
        puerta.SetActive(false);
        Debug.Log("¡La puerta ha desaparecido!");
    }
}
