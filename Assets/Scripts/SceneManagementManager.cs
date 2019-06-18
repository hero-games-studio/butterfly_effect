using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementManager : MonoBehaviour
{
    private Scene scene;    

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void resetActiveScene()
    {
        SceneManager.LoadScene(scene.name);
    }

}
