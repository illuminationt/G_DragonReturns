using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour {

    [SerializeField] public GameObject m_firstMenu;
    [SerializeField] private GameObject m_iconAttack;
    [SerializeField] private GameObject m_iconItem;
    public int IconFirstPos { get; private set; }


    [SerializeField] private GameObject m_jankenMenu;




    private void Start()
    {
        m_firstMenu.SetActive(false);
        m_iconAttack.SetActive(true);
        m_iconItem.SetActive(false);
    }

    //選んだらtrueを返す
    public bool CtrlFirstMenu()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            IconFirstPos++;
            if (IconFirstPos >= 2)
            {
                IconFirstPos = 0;
            }

            switch (IconFirstPos)
            {
                case 0:
                    m_iconAttack.SetActive(true);
                    m_iconItem.SetActive(false);
                    break;
                case 1:
                    m_iconAttack.SetActive(false);
                    m_iconItem.SetActive(true);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            m_firstMenu.SetActive(false);
            return true;
        }

        return false;
    }
}
