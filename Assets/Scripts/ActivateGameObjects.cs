using System.Collections;
using UnityEngine;

public class ActivateGameObjects : MonoBehaviour
{
    // Referencias a los GameObjects que se activar�n
    public GameObject object1;
    public GameObject object2;

    // Variables de tiempo que determinan los intervalos de activaci�n
    public float timeIntervalX = 10.0f;
    public float timeIntervalY = 5.0f;

    private void Start()
    {
        // Iniciar las corrutinas para activar los GameObjects
        if (object1 != null)
        {
            StartCoroutine(ActivateObject(object1, timeIntervalX));
        }

        if (object2 != null)
        {
            StartCoroutine(ActivateObject(object2, timeIntervalY));
        }
    }

    // Corrutina para activar un GameObject en intervalos de tiempo
    private IEnumerator ActivateObject(GameObject obj, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            obj.SetActive(true);

            // Si deseas que el objeto se desactive despu�s de un tiempo, descomenta las siguientes l�neas
            yield return new WaitForSeconds(10.0f); // Tiempo que permanecer� activo
            obj.SetActive(false);
        }
    }
}