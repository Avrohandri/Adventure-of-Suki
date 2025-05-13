using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string message, Color nameColor)
    {
        text.text = message;
        text.color = nameColor;
    }
}
