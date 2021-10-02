using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTextBottonCollider : MonoBehaviour
{
    MainText mainText;
    // Start is called before the first frame update
    void Start()
    {
        mainText = FindObjectOfType<MainText>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mainText.DropSomeLetters();
    }

}
