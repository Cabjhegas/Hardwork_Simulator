using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainText : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    PolygonCollider2D thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<PolygonCollider2D>();
        mainText = GetComponent<TextMeshProUGUI>();
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
}
