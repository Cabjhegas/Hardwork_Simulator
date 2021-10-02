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
    public Transform mainTextBottonCollider;
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
            DropALetter(mainText.mainText.textInfo.characterCount - 1);
            mainText.UpdateMainText(-1);
        }
        else if (Input.anyKeyDown)
        {

            mainText.UpdateMainText(3);

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

    public void DropALetter(int characterIndex)
    {
        //if(lastLetter == "")
        //{
        //    UpdateMainText(-1);
        //    FireTextProjectile();
        //    return;
        //}

        GameObject textProjectileGO = textProjectilePooler.GetPooledObject();        

        textProjectileGO.transform.position = GetCharWorldPos(characterIndex);
        textProjectileGO.transform.rotation = transform.rotation;
        TextProjectile textProjectile = textProjectileGO.GetComponent<TextProjectile>();
        textProjectile.thisText.text = mainText.mainText.text[characterIndex].ToString();
        textProjectile.rigidbody.velocity = Vector2.zero;
        //textProjectile.thisText.fontSize = mainText.mainText.fontSize;
        //textProjectile.collider.radius = mainText.mainText.fontSize / 3;
        //textProjectileGO.GetComponent<RectTransform>().sizeDelta = new Vector2(mainText.mainText.fontSize, mainText.mainText.fontSize);
        textProjectileGO.SetActive(true);
        //mainText.UpdateMainText(-1);

    }

    Vector2 GetCharWorldPos(int characterIndex)
    {
        TMP_CharacterInfo thisCharacterInfo = mainText.mainText.textInfo.characterInfo[characterIndex];
        
        Vector2 charEnd = new Vector2(thisCharacterInfo.bottomRight.x + 2, thisCharacterInfo.baseLine);
        return mainText.transform.TransformPoint(charEnd);

    }
}
