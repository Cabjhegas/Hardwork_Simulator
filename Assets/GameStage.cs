using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStage : MonoBehaviour
{
    public UnityEvent unityEvent;
    
    [TextAreaAttribute(10, 45)]
    public string thisStageString;
}
