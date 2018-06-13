using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KaveKoala
{
    public class SceneController : MonoBehaviour
    {
        private const string s_websiteUrl = @"http://appgraft.com";

        public enum GameScene
        {
            MenuScreen,
            GameLevel1,
            GameLevel2,
            GameLevel3,
            MenuAbout
        }

        public GameScene Scene;

        /// <summary>
        /// Player has loaded a level when value is true.
        /// </summary>
        public static bool IsGameLoaded { get; set; }

        public static bool IsLevel2Enabled { get; set; }

        public static bool IsLevel3Enabled { get; set; }

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>    
        void Update()
        {
        }

        public void LoadLevel(string levelName)
        {
            switch (levelName)
            {
                case "GameLevel1":
                default:
                    LoadLevel(GameScene.GameLevel1);
                    break;
                case "GameLevel2":
                    LoadLevel(GameScene.GameLevel2);
                    break;
                case "GameLevel3":
                    LoadLevel(GameScene.GameLevel3);
                    break;
            }
        }

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param name="levelName">Name of scene to be loaded.</param>
        public void LoadLevel(GameScene levelName)
        {
            UnloadMenu();

            SceneManager.LoadScene(levelName.ToString(), LoadSceneMode.Single);
            SceneController.IsGameLoaded = true;
        }

        public void UnloadMenu()
        {
            try
            {
                SceneManager.UnloadSceneAsync(GameScene.MenuScreen.ToString());
            }
            catch
            {
                // Scene wasn't loaded
            }

            Time.timeScale = 1;
        }

        public void LoadMenu()
        {
            LoadMenu(LoadSceneMode.Additive);
        }

        public void LoadMenu(LoadSceneMode mode)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(GameScene.MenuScreen.ToString(), mode);
        }

        public void UnloadAbout()
        {
            SceneManager.UnloadSceneAsync(GameScene.MenuAbout.ToString());
        }

        public void LoadAbout()
        {
            SceneManager.LoadScene(GameScene.MenuAbout.ToString(), LoadSceneMode.Additive);
        }

        public void LoadWebsiteUrl()
        {
            Application.OpenURL(s_websiteUrl);
        }
    }
}