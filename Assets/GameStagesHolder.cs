using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStagesHolder : MonoBehaviour
{
    public GameStage[] GameStagesArray;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameStagesArray = GetComponentsInChildren<GameStage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
