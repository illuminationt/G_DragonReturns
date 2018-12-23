using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour {

    [SerializeField] private TextAsset[] m_mapData;

    [SerializeField] private GameObject m_space;
    [SerializeField] private GameObject m_boss;


    public void MakeMap(int mapID)
    {
        StringReader stringReader = new StringReader(m_mapData[mapID].text);

        



    }
}
