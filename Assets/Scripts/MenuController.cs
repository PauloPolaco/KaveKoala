using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaveKoala
{
    public class MenuController : MonoBehaviour
    {
        private const string m_buttonResume = @"ButtonResume";
        private const string m_buttonLevel1 = @"ButtonLevel1";
        private const string m_buttonLevel2 = @"ButtonLevel2";
        private const string m_buttonLevel3 = @"ButtonLevel3";

        private readonly Color32 m_buttonLevelTextColourEnabled = new Color32(255, 126, 0, 255);
        private readonly Color32 m_buttonResumeTextColourEnabled = new Color32(238, 87, 0, 255);
        private readonly Color32 m_buttonTextColourDisabled = new Color32(64, 64, 64, 255);

        public static bool IsPrefsLoaded { get; set; }

        public const string PrefsLevel2Name = @"IsLevel2Enabled";
        public const string PrefsLevel3Name = @"IsLevel3Enabled";

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            LoadPrefs();
            SetButtonVisibility();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
        }

        private static void LoadPrefs()
        {
            if (MenuController.IsPrefsLoaded == false)
            {
                int isLevel2Enabled = 0;
                int isLevel3Enabled = 0;

                isLevel2Enabled = PlayerPrefs.GetInt(PrefsLevel2Name);
                isLevel3Enabled = PlayerPrefs.GetInt(PrefsLevel3Name);

                if (isLevel2Enabled == 1)
                {
                    SceneController.IsLevel2Enabled = true;
                }

                if (isLevel3Enabled == 1)
                {
                    SceneController.IsLevel3Enabled = true;
                }

                MenuController.IsPrefsLoaded = true;
            }
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
                        SetLevelButtonVisibility(button, SceneController.IsLevel2Enabled);
                        break;
                    case m_buttonLevel3:
                        SetLevelButtonVisibility(button, SceneController.IsLevel3Enabled);
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

        private void SetLevelButtonVisibility(Button button, bool isEnabled)
        {
            Text[] label = button.GetComponentsInChildren<Text>();

            if (isEnabled)
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
}