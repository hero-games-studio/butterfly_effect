using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI sizeUpText;
    //[SerializeField] private Text overLevelText;
    [SerializeField] private TextMeshProUGUI overLevelText;
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

    public void toggleOverLevelText(bool toggle)
    {
        overLevelText.gameObject.SetActive(toggle);
    }

    #endregion
}
