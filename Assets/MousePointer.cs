using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        ShootMousePointer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootMousePointer()
    {
        float x = Random.Range(-0.8f, 0.8f);
        float y = Random.Range(0f, 0.9f);
        Vector2 randomDirection = new Vector3(x, y).normalized;
        float forceMultiplier = 1f;
        Debug.LogError(randomDirection);
        thisRigidbody.AddForce(randomDirection * forceMultiplier, ForceMode2D.Impulse);
    }
}
