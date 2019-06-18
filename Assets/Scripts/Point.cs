using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    //for point animation

    TextMesh textMesh;

    private void OnEnable()
    {
        textMesh = this.GetComponent<TextMesh>();
        StartCoroutine(pointAnimation());
    }

    IEnumerator pointAnimation()
    {
        for(int i=10; i>=0; i--)
        {

            transform.Translate(0, Time.deltaTime*2, 0, Space.World);
            textMesh.color = new Color(1.0f,0,0,(float)i/10);
            yield return null;
        }
        Destroy(this.gameObject);

    }
}
