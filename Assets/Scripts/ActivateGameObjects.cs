using System.Collections;
using UnityEngine;

public class ActivateGameObjects : MonoBehaviour
{
    // Referencias a los GameObjects que se activarán
    public GameObject object1;
    public GameObject object2;

    // Variables de tiempo para activar los objetos (en segundos)
    public float activationTime1 = 180.0f; // 3 minutos
    public float activationTime2 = 300.0f; // 5 minutos

    // Duración de tiempo que los objetos estarán activos (en segundos)
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

    // Corrutina para activar un GameObject después de un tiempo específico y desactivarlo después de una duración
    private IEnumerator ActivateObject(GameObject obj, float activationTime, float duration)
    {
        yield return new WaitForSeconds(activationTime);
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}