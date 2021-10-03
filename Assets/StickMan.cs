using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PullStickManUp();
    }

    void PullStickManUp()
    {
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(0f, 0.9f);
        Vector2 randomDirection = new Vector3(x, y).normalized;
        float forceMultiplier = 10f;
        thisRigidbody.AddForce(randomDirection * forceMultiplier, ForceMode2D.Impulse);
    }
}
