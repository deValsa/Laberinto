using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 7f;         // Velocidad de desplazamiento
    public float velocidadGiro = 120f;   // Velocidad de giro en grados por segundo
    public float gravedad = -9.81f;      // Fuerza de gravedad
    public float fuerzaSalto = 5f;       // Fuerza del salto

    public GameObject proyectilPrefab;   // Prefab del proyectil a disparar
    public Transform puntoDisparo;       // Punto desde donde se dispara el proyectil
    public float fuerzaDisparo = 10f;    // Fuerza del proyectil

    private CharacterController controlador;
    private Vector3 velocidadMovimiento; // Velocidad vertical acumulada
    private bool enSuelo;                // Verifica si el personaje está en el suelo

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el Character Controller del GameObject
        controlador = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el personaje está tocando el suelo
        enSuelo = controlador.isGrounded;

        if (enSuelo && velocidadMovimiento.y < 0)
        {
            velocidadMovimiento.y = -2f; // Resetea la velocidad vertical al estar en el suelo
        }

        // Movimiento en los ejes Z (desplazamiento adelante/atrás)
        float desplazamiento = Input.GetAxis("Vertical");

        // Giro del personaje en el eje Y
        float giro = Input.GetAxis("Horizontal");

        // Calcula el desplazamiento en la dirección actual del personaje
        Vector3 movimiento = transform.forward * desplazamiento;

        // Mueve al personaje usando el Character Controller
        controlador.Move(movimiento * velocidad * Time.deltaTime);

        // Gira al personaje alrededor del eje Y
        transform.Rotate(0, giro * velocidadGiro * Time.deltaTime, 0);

        // Si está en el suelo y se presiona la tecla de salto, salta
        if (enSuelo && Input.GetButtonDown("Jump"))
        {
            velocidadMovimiento.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad); // Calcula el salto
        }

        // Aplicar gravedad
        velocidadMovimiento.y += gravedad * Time.deltaTime;

        // Aplicar velocidad acumulada (gravedad o salto)
        controlador.Move(velocidadMovimiento * Time.deltaTime);

        // Disparar si se presiona la tecla de disparo (por defecto el botón izquierdo del ratón)
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    // Método para disparar el proyectil
    void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            // Instanciar el proyectil en el punto de disparo
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            // Asigna el tag "Proyectil" al proyectil para que el interruptor lo detecte
            proyectil.tag = "Proyectil";

            // Obtener el Rigidbody del proyectil y aplicar la fuerza de disparo
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(puntoDisparo.forward * fuerzaDisparo, ForceMode.VelocityChange); // Aplicar fuerza en la dirección en que mira el jugador
            }
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Salida"))
        {
            Debug.Log("¡Has salido!");
        }
    }
}
