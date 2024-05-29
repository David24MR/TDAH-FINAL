using System.Collections;
using UnityEngine;

public class ActivateGameObjects : MonoBehaviour
{
    // Referencias a los GameObjects que se activar�n
    public GameObject object1;
    public GameObject object2;

    // Variables de tiempo para activar los objetos (en segundos)
    public float activationTime1 = 180.0f; // 3 minutos
    public float activationTime2 = 300.0f; // 5 minutos

    // Duraci�n de tiempo que los objetos estar�n activos (en segundos)
    public float duration1 = 20.0f;
    public float duration2 = 10.0f;

    private void Start()
    {
        // Iniciar las corrutinas para activar los GameObjects
        if (object1 != null)
        {
            StartCoroutine(ActivateObject(object1, activationTime1, duration1));
        }

        if (object2 != null)
        {
            StartCoroutine(ActivateObject(object2, activationTime2, duration2));
        }
    }

    // Corrutina para activar un GameObject despu�s de un tiempo espec�fico y desactivarlo despu�s de una duraci�n
    private IEnumerator ActivateObject(GameObject obj, float activationTime, float duration)
    {
        yield return new WaitForSeconds(activationTime);
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}