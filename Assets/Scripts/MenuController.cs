using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private const string m_buttonResume = @"ButtonResume";
    private const string m_buttonLevel1 = @"ButtonLevel1";
    private const string m_buttonLevel2 = @"ButtonLevel2";
    private const string m_buttonLevel3 = @"ButtonLevel3";

    private readonly Color32 m_buttonLevelTextColourEnabled = new Color32(255, 126, 0, 255);
    private readonly Color32 m_buttonResumeTextColourEnabled = new Color32(238, 87, 0, 255);
    private readonly Color32 m_buttonTextColourDisabled = new Color32(64, 64, 64, 255);

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
        SetButtonVisibility();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {		
	}

    /// <summary>
    /// Enables or disables buttons depending 
    /// on whether user has initiated a game.
    /// </summary>
    private void SetButtonVisibility()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            switch (button.name)
            {
                case m_buttonResume:
                    SetResumeButtonVisibility(button);
                    break;
                case m_buttonLevel2:
                case m_buttonLevel3:
                    SetLevelButtonVisibility(button);
                    break;
            }
        }
    }

    private void SetResumeButtonVisibility(Button button)
    {
        Text[] label = button.GetComponentsInChildren<Text>();

        if (SceneController.IsGameLoaded)
        {
            button.interactable = true;
            label[0].color = m_buttonResumeTextColourEnabled;
        }
        else
        {
            button.interactable = false;

            label[0].color = m_buttonTextColourDisabled;
        }
    }

    private void SetLevelButtonVisibility(Button button)
    {
        Text[] label = button.GetComponentsInChildren<Text>();

        if (false)
        {
            button.interactable = true;
            label[0].color = m_buttonLevelTextColourEnabled;
        }
        else
        {
            button.interactable = false;

            label[0].color = m_buttonTextColourDisabled;
        }
    }
}