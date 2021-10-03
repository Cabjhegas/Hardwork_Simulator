using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    MainText mainText;
    GameStagesHolder gameStagesHolder;
    public Transform textCursor;
    public Transform mainTextBottonCollider;
    public ObjectPooler textProjectilePooler;
    public GameObject stickManPrefab;
    int currentStage = -1;

    public int pressKeyCount = 0;

    bool dropLetterAllowed;
    public bool shootMousePointerAllowed;

    public Transform[] testeDosVertices;

    public bool isPaused;
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
        if (isPaused)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            mainText.UpdateMainText(-1);
            mainText.UpdateMainText(-1);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && dropLetterAllowed)
        {
            DropALetter(mainText.mainText.textInfo.characterCount - 1);
            mainText.UpdateMainText(-1);
        }
        else if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {
            return;
        }
        else if (Input.anyKeyDown)
        {

            mainText.UpdateMainText(1);
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

    public void ScrollMainTextUp()
    {
        StartCoroutine(ScrollMainTextUpRoutine());
    }

    IEnumerator ScrollMainTextUpRoutine()
    {
        Vector3 mainTextOriginalPos = mainText.transform.position;
        Vector3 textCursorOriginalPos = textCursor.transform.position;
        Vector3 mainTextBottonColliderOriginalPos = mainTextBottonCollider.transform.position;

        float totalAmountToMove = 3f;
        float timer = 0.5f;
        float t = 0;
        while(t<timer)
        {
            mainText.transform.position = mainTextOriginalPos + (Vector3.up * totalAmountToMove*t/timer);
            textCursor.transform.position = textCursorOriginalPos + (Vector3.up * totalAmountToMove * t / timer);
            mainTextBottonCollider.transform.position = mainTextBottonColliderOriginalPos + (Vector3.up * totalAmountToMove * t / timer);
            t += Time.deltaTime;
            yield return null;
        }
        mainText.transform.position = mainTextOriginalPos + (Vector3.up * totalAmountToMove);
        textCursor.transform.position = textCursorOriginalPos + (Vector3.up * totalAmountToMove);
        mainTextBottonCollider.transform.position = mainTextBottonColliderOriginalPos + (Vector3.up * totalAmountToMove);

    }

    public void SpawnStickMan(int number)
    {
        for(int i = 0; i < number; i++)
        {
            Instantiate(stickManPrefab, new Vector3(UnityEngine.Random.Range(-3, 3), -4, 0),Quaternion.identity);
        }
    }

 

    public void AllowDropLetter()
    {
        dropLetterAllowed = true;
    }

    public void AllowShootMousePointer()
    {
        StickMan[] allStickMen = FindObjectsOfType<StickMan>();

        foreach(StickMan s in allStickMen)
        {
            s.ShootTimeToTime();
        }
        shootMousePointerAllowed = true;


    }

    public void RelaunchScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
