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
    public Transform textBottomCollider;
    public event Action thisAction;
    public UnityAction UnityAction;
    public UnityEvent UnityEvent;
    int currentStage = -1;


    string completeText;
    int pressKeyCount = 0;

    Vector2 lastLetterPos;
    string lastLetter;

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
            UpdateMainText(-1);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            FireTextProjectile();
        }
        else if (Input.anyKeyDown)
        {

            UpdateMainText(1);

        }
    }

    bool richTextFormattingInAction;

    void UpdateMainText(int keyCountIncrement)
    {
        pressKeyCount += keyCountIncrement;

        if (pressKeyCount < 0)
        {
            pressKeyCount = 0;
        }

        if(pressKeyCount> completeText.Length)
        {
            ChangeStage();
            return;
        }

        mainText.mainText.text = completeText.Substring(0, pressKeyCount);

        if (pressKeyCount == mainText.mainText.text.Length)
        {
            Debug.Log("Game Over");
        }

        lastLetter = mainText.mainText.text[pressKeyCount - 1].ToString();
        Debug.Log("Last letter is "+lastLetter);

        if (keyCountIncrement > 0)
        {
            if (lastLetter == "<" || richTextFormattingInAction)
            {
                richTextFormattingInAction = true;
                if (lastLetter == ">")
                {
                    richTextFormattingInAction = false;
                }
                UpdateMainText(keyCountIncrement);
                return;
            }
        }
        else if (keyCountIncrement < 0)
        {
            if (lastLetter == ">" || richTextFormattingInAction)
            {
                richTextFormattingInAction = true;
                if (lastLetter == "<")
                {
                    richTextFormattingInAction = false;
                }
                UpdateMainText(keyCountIncrement);
                return;
            }
        }


        mainText.UpdateCollider();

        //Vector3[] verticesOfMainText = mainText.mesh.vertices;
        TMP_CharacterInfo thisCharacterInfo = mainText.mainText.textInfo.characterInfo[mainText.mainText.textInfo.characterCount - 1];

        if (thisCharacterInfo.isVisible)
        {
            Debug.Log(lastLetter);
            //Vector2 charMidTopLine = new Vector2((verticesOfMainText[thisCharacterInfo.vertexIndex + 0].x + verticesOfMainText[thisCharacterInfo.vertexIndex + 2].x) / 2, (thisCharacterInfo.bottomLeft.y + thisCharacterInfo.topLeft.y) / 2);
            Vector2 lastCharEnd = new Vector2(thisCharacterInfo.bottomRight.x + 2, thisCharacterInfo.baseLine);
            lastLetterPos = mainText.transform.TransformPoint(lastCharEnd);
            textCursor.position = lastLetterPos;
            textBottomCollider.position = lastLetterPos;
        }



    }

    void ChangeStage()
    {
        currentStage++;
        completeText += gameStagesHolder.GameStagesArray[currentStage].thisStageString;
        gameStagesHolder.GameStagesArray[currentStage].unityEvent.Invoke();
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


        

        textProjectileGO.transform.position = lastLetterPos;
        textProjectileGO.transform.rotation = transform.rotation;
        TextProjectile textProjectile = textProjectileGO.GetComponent<TextProjectile>();
        textProjectile.text.text = lastLetter;
        textProjectile.text.fontSize = mainText.mainText.fontSize;
        textProjectile.collider.radius = mainText.mainText.fontSize / 3;
        textProjectileGO.GetComponent<RectTransform>().sizeDelta = new Vector2(mainText.mainText.fontSize, mainText.mainText.fontSize);
        textProjectileGO.SetActive(true);
        textProjectile.Fire();
        UpdateMainText(-1);

    }
}
