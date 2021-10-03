using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    int totalHealth = 3;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        //ShootMousePointer();
    }

    private void OnEnable()
    {
        currentHealth = totalHealth;
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
        float forceMultiplier = 100f;
        GetComponent<Rigidbody2D>().AddForce(randomDirection * forceMultiplier, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("TextProjectile"))
        {
            currentHealth--;
            Debug.LogError(currentHealth);
        }
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Debug.LogError("Died");
        }
    }
}
