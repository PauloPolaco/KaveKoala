using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KaveKoala.Characters;

namespace KaveKoala
{
    public class EndLevelController : MonoBehaviour
    {
        #region Load Image Assets

        private static bool s_assetLoaded;
        private static Dictionary<EndLevelCode, Sprite> s_textImages = new Dictionary<EndLevelCode, Sprite>();
        private static readonly Dictionary<EndLevelCode, string> s_resImages = new Dictionary<EndLevelCode, string>()
    {
        { EndLevelCode.GameOver, "TextGameOver" },
        { EndLevelCode.LoadLevel2, "TextCongrats" },
        { EndLevelCode.LoadLevel3, "TextCongrats" },
        { EndLevelCode.GameWon, "TextWinner" }
    };  // Resources/TextGameOver.png

        private static void LoadAssets()
        {
            if (EndLevelController.s_assetLoaded == false)
            {
                foreach (KeyValuePair<EndLevelCode, string> fileName in s_resImages)
                {
                    byte[] fileData;
                    Texture2D texture = new Texture2D(720, 100);

                    if (File.Exists(fileName.Value))
                    {
                        fileData = File.ReadAllBytes(fileName.Value);
                        texture.LoadImage(fileData);

                        s_textImages.Add(fileName.Key, Sprite.Create(
                            texture, new Rect(0, 0, texture.width, texture.height), new Vector2()));
                    }
                }

                s_assetLoaded = true;
            }
        }

        #endregion Load Image Assets

        public enum EndLevelCode
        {
            GameOver,
            LoadLevel2,
            LoadLevel3,
            GameWon
        }

        public EndLevelCode GameOverCode;
        public Image GameOverText;
        private Rigidbody2D m_rigidbody;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            LoadAssets();

            m_rigidbody = GetComponent<Rigidbody2D>();
            InitializeGameOverImage();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
        }

        private void InitializeGameOverImage()
        {
            if (this.GameOverText == null)
            {
                GameObject gameOverObject = GameObject.FindGameObjectsWithTag("Finish")[0];
                this.GameOverText = gameOverObject.GetComponent<Image>();
            }

            this.GameOverText.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D otherObject)
        {
            if (otherObject.gameObject.name.Equals(Tags.Player, StringComparison.OrdinalIgnoreCase))
            {
                Destroy(otherObject.gameObject);

                //this.GameOverText.sprite = s_textImages[GameOverCode];
                this.GameOverText.sprite = Resources.Load<Sprite>(s_resImages[GameOverCode]);
                this.GameOverText.enabled = true;

                /* Start the function after a
                 * given amount of seconds.*/
                Invoke("PerformEndLevelAction", 2);
            }
            else
            {
                if (!(otherObject.gameObject.tag.Equals(Tags.Enemy, StringComparison.OrdinalIgnoreCase) &&
                    this.gameObject.tag.Equals(Tags.Projectile, StringComparison.OrdinalIgnoreCase)))
                {
                    Destroy(otherObject.gameObject);
                }
            }
        }

        private void PerformEndLevelAction()
        {
            SceneController sceneController = new SceneController();

            switch (GameOverCode)
            {
                case EndLevelCode.GameOver:
                case EndLevelCode.GameWon:
                default:
                    SceneController.IsGameLoaded = false;
                    sceneController.LoadMenu(LoadSceneMode.Single);
                    break;
                case EndLevelCode.LoadLevel2:
                    EnableLevel(MenuController.PrefsLevel2Name, 1);
                    SceneController.IsLevel2Enabled = true;
                    sceneController.LoadLevel(SceneController.GameScene.GameLevel2);
                    break;
                case EndLevelCode.LoadLevel3:
                    EnableLevel(MenuController.PrefsLevel3Name, 1);
                    SceneController.IsLevel3Enabled = true;
                    sceneController.LoadLevel(SceneController.GameScene.GameLevel3);
                    break;
            }
        }

        private void EnableLevel(string levelName, int isEnabled)
        {
            PlayerPrefs.SetInt(levelName, isEnabled);
            PlayerPrefs.Save();
        }
    }
}