using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsQualityScript : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    void Start()
    {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualitySetting", qualityIndex);
    }
}
