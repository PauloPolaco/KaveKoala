using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageVisibilityController : MonoBehaviour
{
    private Image m_imageSprite;

    public Image ImageSprite
    {
        get { return m_imageSprite; }
        set
        {
            m_imageSprite = value;
            SetVisibility(this.IsVisible);
        }
    }

    public bool IsVisible;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
        this.ImageSprite = gameObject.GetComponent<Image>();
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {		
	}

    public void SetVisibility(bool isVisible)
    {
        if (m_imageSprite != null)
        {
            m_imageSprite.enabled = this.IsVisible;
        }
    }
}