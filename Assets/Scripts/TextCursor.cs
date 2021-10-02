using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCursor : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, FindObjectOfType<MainText>().mainText.fontSize - 5);
        image = GetComponent<Image>();
        float frequencyBlinking = 0.5f;
        InvokeRepeating("ToggleVisibility", frequencyBlinking, frequencyBlinking);
    }

    void ToggleVisibility()
    {
        image.enabled = !image.enabled;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
