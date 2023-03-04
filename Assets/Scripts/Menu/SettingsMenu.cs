using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Ali Hassan

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    

    private void Start()
    {
        //We will populate the resolution dropdown with all the resolutions standard to Unity
        //We clear all the resolutions and make an array of the new ones
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //To add the options to the dropdown, we need to convert them to strings
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option  = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //Check to see if there is a resolution that already fits
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        //Set the default resolution to the resolution that fits best
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void setVolume (float masterVolume)
    {
        //Binds the slider on the settings menu to the audio mixer's master volume
        audioMixer.SetFloat("masterVolume", masterVolume);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
}
