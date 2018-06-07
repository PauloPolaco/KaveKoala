using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelController : MonoBehaviour
{
    #region Load Image Assets

    private static bool s_assetLoaded;
    private static Dictionary<EndLevelCode, Sprite> s_textImages = new Dictionary<EndLevelCode, Sprite>();
    private static Dictionary<EndLevelCode, string> s_fileNames = new Dictionary<EndLevelCode, string>()
    {
        { EndLevelCode.GameOver, "Assets/Sprites/MobileUI/TextGameOver.png" },
        { EndLevelCode.LoadLevel2, "Assets/Sprites/MobileUI/TextCongrats.png" },
        { EndLevelCode.LoadLevel3, "Assets/Sprites/MobileUI/TextCongrats.png" },
        { EndLevelCode.GameWon, "Assets/Sprites/MobileUI/TextWinner.png" }
    };

    private static void LoadAssets()
    {
        if (EndLevelController.s_assetLoaded == false)
        {
            foreach (KeyValuePair<EndLevelCode, string> fileName in s_fileNames)
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
    void Start ()
    {
        LoadAssets();

        m_rigidbody = GetComponent<Rigidbody2D>();
        this.GameOverText.enabled = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
	}

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.name.Equals("Player", StringComparison.OrdinalIgnoreCase))
        {
            Destroy(otherObject.gameObject);

            //this.GameOverText.sprite = s_textImages[GameOverCode];
            this.GameOverText.enabled = true;

            /* Start the function after a
             * given amount of seconds.*/
            Invoke("PerformEndLevelAction", 2);
        }
        else
        {
            Destroy(otherObject.gameObject);
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
                sceneController.LoadLevel(SceneController.GameScene.GameLevel1);
                break;
            case EndLevelCode.LoadLevel3:
                sceneController.LoadLevel(SceneController.GameScene.GameLevel3);
                break;
        }
    }
}