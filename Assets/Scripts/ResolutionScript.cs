using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionScript : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            //resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionSetting", currentResolutionIndex);
            resolutionDropdown.value = 0;
            resolutionDropdown.RefreshShownValue();
            Screen.SetResolution(resolutions[0].width, resolutions[0].height, FullScreenMode.FullScreenWindow);
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
        //PlayerPrefs.SetInt("ResolutionSetting", resolutionIndex);
    }
}
