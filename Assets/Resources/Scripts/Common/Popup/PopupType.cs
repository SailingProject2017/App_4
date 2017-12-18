using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupType : PopupBase
{

    [SerializeField]
    private GameObject m_contens;
    [SerializeField]
    private EPopupType m_type;
    [SerializeField]
    private GameObject[] m_popupType;


    public void OpenEnd()
    {
        m_contens.SetActive(true);
        switch (m_type)
        {
            case EPopupType.Tutorial:
                m_popupType[0].SetActive(true);
                break;
            case EPopupType.Stage:
                m_popupType[1].SetActive(true);
                break;
            case EPopupType.Matching:
                m_popupType[2].SetActive(true);
                break;
        }
    }

    public void CloseEnd()
    {
        m_contens.SetActive(false);
        switch (m_type)
        {
            case EPopupType.Tutorial:
                m_popupType[0].SetActive(false);
                break;
            case EPopupType.Stage:
                m_popupType[1].SetActive(false);
                break;
            case EPopupType.Matching:
                m_popupType[2].SetActive(false);
                break;
        }
    }

    void PopupAction(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.OK:
                Close();
                break;
            case EButtonId.Cancel:
                Close();
                break;
        }
    }
    
    public void GetPopupType(EPopupType type)
    {
        m_type = type;
    }

    public void Open()
    {
        mButtonSet = EButtonSet.Set2;
        PopupButton.mOnClickCallback = PopupAction;

        base.Open(null, null, OpenEnd);
    }

    public void Close()
    {
        base.Close(CloseEnd, null, null);
    }
}
