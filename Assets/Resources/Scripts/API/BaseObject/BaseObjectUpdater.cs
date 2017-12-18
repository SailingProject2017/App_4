/**********************************************************************************************/
/*! @file   BaseObjectUpdater.cs
*********************************************************************************************
* @brief      すべてのオブジェクトのアップデートを管理
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;

public class BaseObjectUpdater : BaseObject
{

    
    #region MonoBehavaiourの実装
    /***************************************************************************************/

    /// <summary>
    /// @brief このソリューション唯一のUpdate関数
    /// @note ここ以外でUpdate関数は使わないでください
    /// </summary>
    void Update()
    {
        foreach (var obj in BaseObjectManagerList)
        {
            if (obj.IsPresence())
                obj.OnFastUpdate();
        }

        foreach (var obj in BaseObjectList)
        {
            if (obj.IsPresence())
                obj.OnUpdate();
        }

    }

    /// <summary>
    /// @brief このソリューション唯一のLateUpdate関数
    /// @note ここ以外でLateUpdate関数は使わないでください
    /// </summary>
    void LateUpdate()
    {
        foreach (var obj in BaseObjectList)
        {
            if (obj.IsPresence())
                obj.OnLateUpdate();
        }
    }

    /// <summary>
    /// @brief このソリューション唯一のFixedUpdate関数
    /// @note ここ以外でFixedUpdate関数は使わないでください
    /// </summary>
    void FixedUpdate()
    {
        foreach (var obj in BaseObjectList)
        {
            if (obj.IsPresence())
                obj.OnFixedUpdate();
        }
    }
    #endregion
}
