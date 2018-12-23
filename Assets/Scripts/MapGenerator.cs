using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour {

    [SerializeField] private TextAsset[] m_mapData;

    [SerializeField] private GameObject m_dragonWorld;
    [SerializeField] private GameObject[] m_enemiesWorld;
    [SerializeField] private GameObject[] m_bossesWorld;
    [SerializeField] private GameObject[] m_item;

    //ドラゴンの初期位置はここで調整してください
    //後でCSVファイルの中に入れるかも
    [SerializeField] private Vector2[] m_dragonInitPos;

    [NonSerialized]public DragonWorld Dragon = null;
    [NonSerialized] public List<EnemyWorld> EnemyList = new List<EnemyWorld>();
    [NonSerialized] public List<Item> DropItemList = new List<Item>();
    [NonSerialized] public Vector3 MapSize = Vector3.zero;

    //
    public void MakeMap(int mapID)
    {
        StringReader stringReader = new StringReader(m_mapData[mapID].text);

        //最初は敵の生成（ボス含む)
        
        int x = 0, z = 0,zMax = 0;
        //ExcelのCSVファイルを1行ずつ切り取る
        while (stringReader.Peek() > -1)
        {
            string col = stringReader.ReadLine();
            //","で区切る。
            string[] data = col.Split(',');
            
            foreach(string datum in data)
            {
                GameObject gridObj = GetObjectFromData(datum);
                if (gridObj != null)
                {
                    EnemyWorld e = gridObj.GetComponent<EnemyWorld>();
                    if (e != null)
                    {
                        e.GridPosition = new Vector3(x, 0, z);
                        e.SetPosition();
                        EnemyList.Add(e);
                    }
                    
                    Item item = gridObj.GetComponent<Item>();
                    if (item != null)
                    {
                        item.GridPosition = new Vector3(x, 0, z);
                        item.transform.position = item.GridPosition;
                        DropItemList.Add(item);
                    }
                    

                }
                z++;
            }
            x++;
            zMax = z;
            z = 0;

        }
        MapSize = new Vector3(x, 0, zMax);

        //プレイヤーの生成
        Dragon = Instantiate(m_dragonWorld).GetComponent<DragonWorld>();
        m_dragonWorld.GetComponent<DragonWorld>().GridPosition = m_dragonInitPos[mapID];

    }
    
    //エクセルに入れた文字によって生成するオブジェクト（敵の種類、ボスの種類)を返します
    private GameObject GetObjectFromData(string datum)
    {
        GameObject obj = null;
        switch (datum)
        {
            case "0":break;
            case "e0":obj = m_enemiesWorld[0];break;
            case "b0":obj = m_bossesWorld[0];break;
            case "i0":obj = m_item[0];break;
            default:
                Debug.LogError("Excelファイルに変な値が書き込まれています");
                break;
        }
        if (obj != null)
        {
            return Instantiate(obj);
        }
        else
        {
            return null;
        }
    }

}
