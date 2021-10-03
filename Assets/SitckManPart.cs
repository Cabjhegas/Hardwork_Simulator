using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitckManPart : MonoBehaviour
{
    StickMan stickMan;
    // Start is called before the first frame update
    void Start()
    {
        stickMan = GetComponentInParent<StickMan>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TextProjectile")|| collision.gameObject.CompareTag("MainText"))
        {
            stickMan.GotHit();
        }
        
    }

}
