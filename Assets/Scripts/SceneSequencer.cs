using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSequencer : MonoBehaviour { 

    private FadePanel fadePanel;
    private bool isChangeScene = false;
    private string nextSceneName;



    // Use this for initialization
    void Start()
    {
        fadePanel = GameObject.Find("FadePanel").GetComponent<FadePanel>();
        fadePanel.IsFadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangeScene && !fadePanel.IsFadeOut)
        {
            SceneManager.LoadScene(nextSceneName);
            fadePanel.IsFadeIn = true;
        }
    }

    public void ChangeScene(string name)
    {
        if (fadePanel.IsFadeIn)
        {
            return;
        }
        isChangeScene = true;
        fadePanel.IsFadeOut = true;
        nextSceneName = name;
    }
}
