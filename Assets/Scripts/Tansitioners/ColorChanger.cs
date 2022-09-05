using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    Color color;

    private void Start()
    {
        color = text.color;
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            text.color = color;
            yield return new WaitForSeconds(0.5f);
            color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 1f);
        }
    }
}
