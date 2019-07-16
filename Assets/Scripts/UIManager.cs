using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    #region  Variables
    [Header("Text")]
    [SerializeField] TextMeshProUGUI tapToText;
    [SerializeField] TextMeshProUGUI overLevelText;
    [Header("Panel")]
    [SerializeField] GameObject panel;

    StageManager stageManager;
    Player player;
    SceneManager sceneManager;
    #endregion

    private void Awake()
    {
        stageManager = StageManager.Instance;
        player = Player.getInstance();
    }

    public void tapTo()
    {
       stageManager.levelUp();
    }

    public void setActivePanel(bool active){

        panel.SetActive(active);

    }

}
