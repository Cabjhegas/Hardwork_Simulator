using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    MainText mainText;
    GameStagesHolder gameStagesHolder;
    public Transform textCursor;
    public ObjectPooler textProjectilePooler;
    int currentStage = -1;

    public int pressKeyCount = 0;

    

    public Transform[] testeDosVertices;
    // Start is called before the first frame update
    void Start()
    {
        mainText = FindObjectOfType<MainText>();
        gameStagesHolder = FindObjectOfType<GameStagesHolder>();
        ChangeStage();
        //textProjectile.gameObject.SetActive(false);
        //UpdateMainText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            mainText.UpdateMainText(-1);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            FireTextProjectile();
        }
        else if (Input.anyKeyDown)
        {

            mainText.UpdateMainText(1);

        }
    }

    

    

    public void ChangeStage()
    {
        currentStage++;
        if(currentStage> gameStagesHolder.GameStagesArray.Length)
        {
            GameWin();
            return;
        }
        mainText.completeText += gameStagesHolder.GameStagesArray[currentStage].thisStageString;
        gameStagesHolder.GameStagesArray[currentStage].unityEvent.Invoke();
    }

    void GameWin()
    {
        Debug.Log("YOU WIN!");
    }

    void FireTextProjectile()
    {
        //if(lastLetter == "")
        //{
        //    UpdateMainText(-1);
        //    FireTextProjectile();
        //    return;
        //}

        GameObject textProjectileGO = textProjectilePooler.GetPooledObject();        

        textProjectileGO.transform.position = mainText.lastLetterPos;
        textProjectileGO.transform.rotation = transform.rotation;
        TextProjectile textProjectile = textProjectileGO.GetComponent<TextProjectile>();
        textProjectile.text.text = mainText.lastLetter;
        textProjectile.text.fontSize = mainText.mainText.fontSize;
        textProjectile.collider.radius = mainText.mainText.fontSize / 3;
        textProjectileGO.GetComponent<RectTransform>().sizeDelta = new Vector2(mainText.mainText.fontSize, mainText.mainText.fontSize);
        textProjectileGO.SetActive(true);
        textProjectile.Fire();
        mainText.UpdateMainText(-1);

    }
}
