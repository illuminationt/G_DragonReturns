using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 0  1
 * 2  3
 * 
 */

public class Inventory : SingletonMonoBehaviour<Inventory> {

    public List<string> inventory = new List<string>();

    public Canvas m_canvas;
    public Text[] m_Texts;
    [SerializeField] private GameObject m_icon;
    public int IconPos = 0;

    [SerializeField] private Vector2 m_iconOffset;
    [SerializeField] private int m_iconInterval;

    protected override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        m_canvas.gameObject.SetActive(false);
        m_icon.GetComponent<RectTransform>().anchoredPosition = m_iconOffset - new Vector2(0,IconPos * m_iconInterval);
    }

    public void Open()
    {
        m_canvas.gameObject.SetActive(true);
        

        
        for(int j = 0; j < inventory.Count; j++)
        {
            m_Texts[j].text = inventory[j];
        }

    }

    //
    public void ChooseItem()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IconPos--;
            if (IconPos < 0)
            {
                IconPos = inventory.Count - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IconPos++;
            if (IconPos >= inventory.Count)
            {
                IconPos = 0;
            }

        }

        m_icon.GetComponent<RectTransform>().anchoredPosition = m_iconOffset - new Vector2(0, IconPos * m_iconInterval);
    }
}
