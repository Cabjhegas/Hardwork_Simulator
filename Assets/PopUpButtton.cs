using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpButtton : MonoBehaviour
{
    CanvasGroup canvasGroup;
    GameManager gameManager;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        PopUpOff();
    }

    public void PopUpOn()
    {
        audioSource.Play();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        gameManager.isPaused = true;
        GetComponentInChildren<Button>().Select();
        Time.timeScale = 0;
    }

    public void PopUpOff()
    {
        Time.timeScale = 1;
        gameManager.isPaused = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopUpOff();
        }
    }
}
