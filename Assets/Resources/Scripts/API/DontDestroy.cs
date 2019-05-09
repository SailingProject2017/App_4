/**************************************************************************************/
/*! @file   DontDestroy.cs
***************************************************************************************
* @brief    削除しないオブジェクト用コンポーネント。
***************************************************************************************
* @author   Ryo Sugiyama
***************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections.Generic;


public class DontDestroy : BaseObject
{

    // @brief   このコンポーネントをアタッチしているオブジェクトのリスト
    static Dictionary<string, GameObject> dontDestroyDic = new Dictionary<string, GameObject>();

    /// <summary>
    ///  @brief 変数アクセサー
    ///  @set このコンポーネントをアタッチしているオブジェクトのリストの更新(外部変更不可)
    ///  @get このコンポーネントをアタッチしているオブジェクトのリストの取得
    /// </summary>
    public static Dictionary<string, GameObject> DontDestroyList
    {
        get { return dontDestroyDic; }
        private set { dontDestroyDic = value; }
    } 
    
    /// <summary>
    /// @brief      BaseObjectの実装
    /// @note       管理リストに登録された時によばれる
    /// </summary>
    protected override void OnAwake()
    {
        base.OnAwake();
        if (!DontDestroyList.ContainsKey(this.gameObject.name))
        {
            // リストの更新
            DontDestroyOnLoad(this.gameObject);
            DontDestroyList.Add(this.gameObject.name, this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
