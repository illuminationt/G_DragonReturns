using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class TransitionManager : SingletonMonoBehaviour<TransitionManager>
{
    [SerializeField] private GameObject m_fadePlane = null;
    public delegate void voidDelegate();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //場面反転後に実行したい関数を第４変数にいれよう
    //ただし、返り値void,引数なし
    public void transite(string sceneName, float fadeOutTime, float fadeInTime,voidDelegate afterTransDel=null)
    {
        StartCoroutine(transition(sceneName, fadeOutTime, fadeInTime,afterTransDel));
    }
    private IEnumerator transition(string sceneName, float fadeOutTime, float fadeInTime = 0.1f,voidDelegate afterTransDel=null)
    {
        float time = 0;
        while (true)
        {
            Color color = m_fadePlane.GetComponent<Image>().color;
            color.a = time / fadeOutTime;
            m_fadePlane.GetComponent<Image>().color = color;
            time += Time.deltaTime;

            if (color.a > 1f)
            {
                break;
            }
            yield return null;
        }


        //シーン切替
        SceneManager.LoadScene(sceneName);
        yield return null;
        //シーン切り替え後に呼び出される関数
        if (afterTransDel != null)
        {
            afterTransDel();
        }

        //だんだん明るく
        time = 0;
        while (true)
        {
            Color color = m_fadePlane.GetComponent<Image>().color;
            color.a = 1f - time / fadeOutTime;
            m_fadePlane.GetComponent<Image>().color = color;
            time += Time.deltaTime;

            if (color.a < 0f)
            {
                break;
            }
            yield return null;
        }

    }


}

