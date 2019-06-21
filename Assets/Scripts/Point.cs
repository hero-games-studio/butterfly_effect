using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point : MonoBehaviour
{
    //for point animation

    TextMeshPro textMesh;

    private void OnEnable()
    {
        textMesh = this.GetComponent<TextMeshPro>();
        StartCoroutine(pointAnimation());
    }

    IEnumerator pointAnimation()
    {
        for(int i=100; i>=0; i--)
        {

            transform.Translate(0, Time.deltaTime*3, Time.deltaTime * 3, Space.World);
            textMesh.color = new Color(1,1,1,(float)i/100);
            yield return null;
        }
        Destroy(this.gameObject);

    }
}
