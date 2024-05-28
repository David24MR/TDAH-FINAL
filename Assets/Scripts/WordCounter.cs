using UnityEngine;
using TMPro;  // Para usar TextMeshPro

public class WordCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;  // Referencia al texto que muestra el conteo
    private int count = 0;

    public void OnButtonPressed()
    {
        count++;
        counterText.text = "Count: " + count.ToString();
    }
}