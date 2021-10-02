using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainText : MonoBehaviour
{
    GameManager gameManager;
    public string completeText;
    public TextMeshProUGUI mainText;
    public Vector2 lastLetterPos;
    public string lastLetter;
    bool richTextFormattingInAction;

    PolygonCollider2D thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<PolygonCollider2D>();
        mainText = GetComponent<TextMeshProUGUI>();
        mainText.text = "";
        gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateMainText(int keyCountIncrement)
    {
        gameManager.pressKeyCount += keyCountIncrement;

        if (gameManager.pressKeyCount < 0)
        {
            gameManager.pressKeyCount = 0;
        }

        if (gameManager.pressKeyCount > completeText.Length)
        {
            gameManager.ChangeStage();
            return;
        }

        mainText.text = completeText.Substring(0, gameManager.pressKeyCount);


        lastLetter = mainText.text[gameManager.pressKeyCount - 1].ToString();
        //Debug.Log("Last letter is "+lastLetter);

        if (keyCountIncrement > 0)
        {
            if (lastLetter == "<" || richTextFormattingInAction)
            {
                richTextFormattingInAction = true;
                if (lastLetter == ">")
                {
                    richTextFormattingInAction = false;
                }
                UpdateMainText(1);
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
                UpdateMainText(-1);
                return;
            }
        }


        UpdateCollider();

        //Vector3[] verticesOfMainText = mainText.mesh.vertices;
        TMP_CharacterInfo thisCharacterInfo = mainText.textInfo.characterInfo[mainText.textInfo.characterCount - 1];

        if (thisCharacterInfo.isVisible)
        {
            //Vector2 charMidTopLine = new Vector2((verticesOfMainText[thisCharacterInfo.vertexIndex + 0].x + verticesOfMainText[thisCharacterInfo.vertexIndex + 2].x) / 2, (thisCharacterInfo.bottomLeft.y + thisCharacterInfo.topLeft.y) / 2);
            Vector2 lastCharEnd = new Vector2(thisCharacterInfo.bottomRight.x + 2, thisCharacterInfo.baseLine);
            lastLetterPos = mainText.transform.TransformPoint(lastCharEnd);
            gameManager.textCursor.position = lastLetterPos;
        }



    }

    //TODO: Melhorar performance da solução abaixo. Estudar Utilities do TextMeshPro pra gerar esse collider de um jeito melhor
    public void UpdateCollider()
    {
        mainText.ForceMeshUpdate();
        
        Vector3[] vertices = mainText.mesh.vertices;
        List<Vector2> vertices2D = new List<Vector2>();

        foreach(Vector3 v3 in vertices)
        {
            if(v3 != Vector3.zero)
            {
                vertices2D.Add(v3);
            }
            
        }

        thisCollider.points = vertices2D.ToArray();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DropSomeLetters());
    }

    IEnumerator DropSomeLetters()
    {
        int randomNumbersOfLettersToDrop = Random.Range(5, 20);

        for (int i = 0; i <= randomNumbersOfLettersToDrop; i++)
        {
            int randomCharIndex = mainText.text.Length - Random.Range(1, 60);
            if (randomCharIndex > 0)
            {
                gameManager.DropALetter(randomCharIndex);
                //mainText.text = mainText.text.Remove(0, randomCharIndex);
                //mainText.text = mainText.text.Insert(0, " ");

                string s = completeText.Remove(randomCharIndex, 0);
                s = s.Insert(randomCharIndex, "*");
                completeText = s;
                UpdateMainText(0);
            }
            yield return new WaitForSeconds(0.08f);
        }

        
    }
}
