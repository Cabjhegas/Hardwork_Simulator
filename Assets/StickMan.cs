using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    ObjectPooler mousePointerPooler;
    PolygonCollider2D thisCollider;

    float minTime = 5f;
    float maxTime = 15f;

    int maxHealth = 3;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<GameManager>().shootMousePointerAllowed)
        {
            ShootTimeToTime();
        }

        ObjectPooler[] objectPoolers = FindObjectsOfType<ObjectPooler>();
        foreach(ObjectPooler o in objectPoolers)
        {
            if (o.pooledObject.GetComponent<MousePointer>())
            {
                mousePointerPooler = o;
            }
        }

        thisCollider = GetComponent<PolygonCollider2D>();

        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("TextProjectile"))
        {
            PullStickManUp(3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TextProjectile"))
        {
            PullStickManUp(3);
        }
        
    }

    void PullStickManUp(float multiplier)
    {
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(0f, 0.9f);
        Vector2 randomDirection = new Vector3(x, y).normalized;
        thisRigidbody.AddForce(randomDirection * multiplier, ForceMode2D.Impulse);
    }

    public void ShootTimeToTime()
    {
        StartCoroutine(ShootTimeToTimeRoutine());
    }

    IEnumerator ShootTimeToTimeRoutine()
    {
        float randomTimer = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(randomTimer);
        if (currentHealth > 0)
        {
            GameObject mousePointerGO = mousePointerPooler.GetPooledObject();

            mousePointerGO.transform.position = transform.position;
            mousePointerGO.transform.rotation = Quaternion.identity;
            mousePointerGO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            mousePointerGO.SetActive(true);
            mousePointerGO.GetComponent<MousePointer>().ShootMousePointer();
        }
        

        ShootTimeToTime();
    }

    public void GotHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            KnockedOut();
        }
        
    }

    void KnockedOut()
    {
        thisCollider.enabled = false;
        Invoke("Revive", 10f);
    }

    void Revive()
    {
        thisCollider.enabled = true;
        currentHealth = maxHealth;
    }
}
