using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene"); // Reemplaza con el nombre de la escena del juego
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego..."); // Muestra un mensaje en la consola
        Application.Quit(); // Cierra la aplicación (solo funciona en la versión compilada)
    }
}
