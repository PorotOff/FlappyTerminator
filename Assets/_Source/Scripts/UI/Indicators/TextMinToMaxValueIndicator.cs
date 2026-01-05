using TMPro;
using UnityEngine;

public class TextMinToMaxValueIndicator : MinToMaxValueIndicator
{
    [SerializeField] private TextMeshProUGUI _text;

    public override void Display(float current)
    {
        _text.text = $"{current}/{Max}";
    }

    public override void Enable()
    {
        _text.gameObject.SetActive(true);
    }

    public override void Disable()
    {
        _text.gameObject.SetActive(false);
    }
}