using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
   public AudioMixer audioMixer;

    Resolution[] resolutions;

    public Dropdown resolutionsDropDown;

    void Start()
    {
        resolutions = Screen.resolutions;

        //Clear out all resolutions in this dropdown list
        resolutionsDropDown.ClearOptions();
        
        //Creating a list of strings that are going to be our options
        List<string> options = new List<string>();

        //Sets the current resolution of the computer to an index of 0
        int currentResolutionIndex = 0;
        
        //Looping through the resolutions in our resolutions array
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Format the resolutions into a string so that they can be easily legible and then add it to our options list
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            //Checks if the resolution of the screen, in both width and height, is equal to the current resolution and sets it to that resolution
            if (resolutions[i].width  == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //Adds the resolutions found above to the dropdowm menu
        resolutionsDropDown.AddOptions(options);

        //Sets the value of the resolution on the dropdown list to the current resolution of the screen
        resolutionsDropDown.value = currentResolutionIndex;

        //Refreshes the dropdown list
        resolutionsDropDown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
      //We set our float to the name of the AudioMixer we renamed in Unity (which is the first perameter)
      audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        //The quality settings access an index of which goes as Low - 0, Medium - 1, and High - 2, then changes accordingly
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        //Sets the game to fullscreen
        Screen.fullScreen = isFullscreen;
        print("Hello");
    }
}
