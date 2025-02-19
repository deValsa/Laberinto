using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaCubo : MonoBehaviour
{
    public Transform playerStartPoint;  // El punto de inicio donde el jugador se respawneará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Verifica si el objeto que colisiona es el jugador
        {
            // Inicia la corutina para respawnear al jugador
            StartCoroutine(RespawnPlayer(other));
            Debug.Log("Has Muerto.");
        }
    }

    private IEnumerator RespawnPlayer(Collider player)
    {
        // Desactiva el movimiento del jugador (si tiene un script de movimiento)
        player.GetComponent<ControlJugador>().enabled = false;

        // Mueve al jugador a la posición inicial
        player.transform.position = playerStartPoint.position;

        // Pausa por un momento (ajusta el tiempo si es necesario)
        yield return new WaitForSeconds(0.5f);

        // Reactiva el movimiento del jugador
        player.GetComponent<ControlJugador>().enabled = true;
    }
}
