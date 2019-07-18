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
    [SerializeField] TextMeshProUGUI pointText;
    [Header("Panel")]
    [SerializeField] GameObject panel;
     [SerializeField] GameObject InGamepanel;

    StageManager stageManager;
    Player player;
    SceneManager sceneManager;
    #endregion

    private void Awake()
    {
        stageManager = StageManager.Instance;
        player = Player.getInstance();
        InGamepanel.SetActive(true);
    }

    public void tapTo()
    {
        stageManager.levelUp();
    }

    public void setActivePanel(bool active)
    {

        panel.SetActive(active);
        InGamepanel.SetActive(!active);

    }

    public void setOverLevelText(string text)
    {
        overLevelText.text=text;
    }

      public void setPointText(string text)
    {
        pointText.text=text;
    }


}
