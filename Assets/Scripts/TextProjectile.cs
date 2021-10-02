using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextProjectile : MonoBehaviour
{
    public TextMeshProUGUI thisText;
    public CircleCollider2D collider;
    public Rigidbody2D rigidbody;
    //bool isFalling;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        MainText mainText = FindObjectOfType<MainText>();
        thisText.fontSize = mainText.mainText.fontSize;
        collider.radius = mainText.mainText.fontSize / 3;
        GetComponent<RectTransform>().sizeDelta = new Vector2(mainText.mainText.fontSize, mainText.mainText.fontSize);

    }

    // Update is called once per frame
    void Update()
    {
        //if (isFalling)
        //{
        //    rectTransform.Translate(new Vector2(0, -5f * Time.deltaTime));
        //}
    }

    public void Fire()
    {
        //isFalling = true;
        rigidbody.velocity = Vector2.zero;
    }



}
