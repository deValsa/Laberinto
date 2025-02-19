using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpikeTrap : MonoBehaviour
{
    public Transform playerStartPoint; // Posición inicial del jugador
    public float moveSpeed = 0.5f;
    public float moveDistance = 2.5f;

    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;

        if (playerStartPoint == null)
        {
            Debug.LogError("StartPoint no asignado. Asegúrate de configurar el Player Start Point en el Inspector.");
        }
    }

    void Update()
    {
        float movement = moveSpeed * Time.deltaTime;
        if (movingUp)
        {
            transform.position += Vector3.up * movement;
            if (transform.position.y >= startPos.y + moveDistance)
                movingUp = false;
        }
        else
        {
            transform.position -= Vector3.up * movement;
            if (transform.position.y <= startPos.y)
                movingUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia la corutina para respawnear al jugador
            StartCoroutine(RespawnPlayer(other));
            Debug.Log("Has muerto.");
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
