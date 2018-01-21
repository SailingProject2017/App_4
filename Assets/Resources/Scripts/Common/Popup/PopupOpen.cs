/**************************************************************************************/
/*! @file   PopupOpen.cs
***************************************************************************************
@brief      Popupを開くスクリプト(デバック用)
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOpen : BaseObject {

    [SerializeField]
    private ModeSelectTutorialPopUp popup;

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
    }
    public void Open()
    {
        popup.Open();
    }
}
