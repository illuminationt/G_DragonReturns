using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour {

    [SerializeField] private string m_worldSceneName;

    private SceneSequencer sceneSequencer;

    // Use this for initialization
    void Start()
    {
        sceneSequencer = GameObject.Find("SceneSequencer").GetComponent<SceneSequencer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneSequencer.ChangeScene(m_worldSceneName);
        }
    }

}
