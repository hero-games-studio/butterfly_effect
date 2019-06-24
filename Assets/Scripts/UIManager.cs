using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI sizeUpText;
    [SerializeField] private TextMeshProUGUI overLevelText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    public TextMeshProUGUI debug;
    #endregion

    #region Functions

    public void Start()
    {
        sizeUpText.gameObject.SetActive(false);
        overLevelText.gameObject.SetActive(false);
    }

    public void toggleSizeUpText(bool toggle)
    {
        sizeUpText.gameObject.SetActive(toggle);
    }

    public void setOverLevelTextColor() { }

    public void setOverLevelText(string text)
    {
        overLevelText.text = text;
    }

    public void setCurrentLevetText(string text)
    {
        currentLevelText.text = text;
    }

    public void setNextLevetText(string text)
    {
        nextLevelText.text = text;
    }

    public void toggleOverLevelText(bool toggle)
    {
        overLevelText.gameObject.SetActive(toggle);
    }

    private void Update()
    {
        debug.text = Camera.main.fieldOfView.ToString();
    }
    #endregion
}
