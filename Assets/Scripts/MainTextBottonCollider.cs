using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTextBottonCollider : MonoBehaviour
{
    MainText mainText;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        mainText = FindObjectOfType<MainText>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //mainText.DropSomeLetters();
        gameManager.DropSomeLetters(10);
    }

}
