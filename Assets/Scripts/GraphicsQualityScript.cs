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
        string[] qualityLevels = QualitySettings.names;
        graphicsDropdown.ClearOptions();
        for(int i = 0; i < qualityLevels.Length; i++)
        {
            graphicsDropdown.options.Add(new TMP_Dropdown.OptionData(qualityLevels[i]));
        }
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
        SetQuality(graphicsDropdown.value);
        graphicsDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        //PlayerPrefs.SetInt("QualitySetting", qualityIndex);
    }
}
