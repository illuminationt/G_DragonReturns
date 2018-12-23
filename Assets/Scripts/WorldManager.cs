using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    [SerializeField] private string m_BattleSceneName;

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
            sceneSequencer.ChangeScene(m_BattleSceneName);
        }
    }
}
