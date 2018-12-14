/**********************************************************************************************
* @file   PopupEnum.cs
***********************************************************************************************
* @brief  PopupWindow関係のenum定義所
***********************************************************************************************
* @author     Yuta Takatsu
***********************************************************************************************
* Copyright © 2017 Yuta Takatsu All Rights Reserved.
*********************************************************************************************/

public enum EPopupState
{
    OpenBegin,
    Openning,
    OpenEnd,
    CloseBegin,
    Closing,
    CloseEnd
}

// ボタンのID
public enum EButtonId
{
    OK,
    Cancel
}

public enum EButtonSet
{
    SetNone,
    Set1,
    Set2
}

// ポップアップの種類
public enum EPopupType
{
    Tutorial,
    Stage,
    Matching
}