using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public Transform textCursor;
    public ObjectPooler textProjectilePooler;
    public Transform textShield;

    string completeText =
    "<b>INTRODUCTION</b><br><br><br>"
    +"Text New TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew <b>lazy dog</b> TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew Text<br>"
    +"Text New TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew <b>lazy dog</b> TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew Text<br>"
    +"Text New TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew <b>lazy dog</b> TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew Text<br>"
    +"Text New TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew <b>lazy dog</b> TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew TextNew Text<br>";

    int pressKeyCount = 0;

    Vector2 lastLetterPos;
    string lastLetter;

    public Transform[] testeDosVertices;
    // Start is called before the first frame update
    void Start()
    {
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

        mainText.text = completeText.Substring(0, pressKeyCount);

        if (pressKeyCount == mainText.text.Length)
        {
            Debug.Log("Game Over");
        }

        lastLetter = mainText.text[pressKeyCount - 1].ToString();
        Debug.Log(lastLetter);
        Debug.Log(richTextFormattingInAction);

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


        mainText.ForceMeshUpdate();

        //Vector3[] verticesOfMainText = mainText.mesh.vertices;
        TMP_CharacterInfo thisCharacterInfo = mainText.textInfo.characterInfo[mainText.textInfo.characterCount - 1];

        if (thisCharacterInfo.isVisible)
        {
            Debug.Log(lastLetter);
            //Vector2 charMidTopLine = new Vector2((verticesOfMainText[thisCharacterInfo.vertexIndex + 0].x + verticesOfMainText[thisCharacterInfo.vertexIndex + 2].x) / 2, (thisCharacterInfo.bottomLeft.y + thisCharacterInfo.topLeft.y) / 2);
            Vector2 lastCharEnd = new Vector2(thisCharacterInfo.bottomRight.x + 2, thisCharacterInfo.baseLine);
            lastLetterPos = mainText.transform.TransformPoint(lastCharEnd);
            textCursor.position = lastLetterPos;
            textShield.position = lastLetterPos;
        }



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
        textProjectile.text.fontSize = mainText.fontSize;
        textProjectile.collider.size = new Vector2(mainText.fontSize-5, mainText.fontSize-5);
        textProjectileGO.GetComponent<RectTransform>().sizeDelta = new Vector2(mainText.fontSize, mainText.fontSize);
        textProjectileGO.SetActive(true);
        textProjectile.Fire();
        UpdateMainText(-1);

    }
}
