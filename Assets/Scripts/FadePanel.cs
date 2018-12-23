using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{

    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private Image image;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Color color;

    [SerializeField]
    private Texture2D Mask;

    public bool IsFadeIn
    {
        get
        {
            return isFadeIn;
        }
        set
        {
            isFadeIn = value;
        }
    }

    public bool IsFadeOut
    {
        get
        {
            return isFadeOut;
        }
        set
        {
            isFadeOut = value;
        }
    }

    // Use this for initialization
    void Start()
    {

        image = GetComponent<Image>();
        image.color = color;
    }

    // Update is called once per frame
    void Update()
    {

        if (isFadeIn)
        {
            Color color = image.color;
            color.a -= speed;

            if (color.a <= 0.0f)
            {
                color.a = 0.0f;
                isFadeIn = false;
            }
            image.color = color;
        }

        if (isFadeOut)
        {
            Color color = image.color;
            color.a += speed;

            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                isFadeOut = false;
            }
            image.color = color;
        }
    }
}

