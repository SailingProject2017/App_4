/**********************************************************************************************/
/*@file   TransformExtension.cs
***********************************************************************************************
* @brief  Transform型に対しての拡張メソッドをまとめた静的クラス
***********************************************************************************************
* @author     Yuta Takatsu
***********************************************************************************************
* Copyright © 2017 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /*********************************************************************************************/
    /// <summary>
    /// @brief    対象のTransformの子供からT型のコンポーネントを取得しリストとして返す
    /// </summary>
    /// <param name="target">対象のTransform</param>
    /// <returns>子から取得したT型コンポーネントをすべて入れた配列</returns>
    public static T[] GetChildrenComponentTo<T>(Transform target)
    {
        // 対象の子オブジェクトの数を格納
        int count = target.childCount;
        Transform transform = target;
        T[] children = new T[count];
        // 子オブジェクトのT型コンポーネントを配列へ格納
        for (int i = 0; i < count; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<T>();
        }
        return children;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    アクティブ状態に関係なく子オブジェクトを取得する
    /// </summary>
    /// <param name="target">対象のTransform</param>
    /// <returns>すべての子オブジェクト</returns>
    public static GameObject[] GetChildren(this Transform target)
    {
        int count = target.childCount;
        Transform transform = target;
        GameObject[] children = new GameObject[count];
        // ターゲットの子オブジェクトを配列に格納する
        for (int i = 0; i < count; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
        return children;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    指定の名前の子オブジェクトを取得する
    ///           第三引数をfalseにするとアクティブ､非アクティブ関係なく取得可能
    /// </summary>
    /// <param name="target">対象のTransform</param>
    /// <param name="serchName">指定する名前</param>
    /// <param name="isActiveOnly">アクティブ状態</param>
    /// <returns>
    /// 成功
    ///   true:ターゲットのアクティブな子オブジェクト
    ///  false:ターゲットの全ての子オブジェクト
    /// 失敗  : null
    /// </returns>
    public static GameObject FindInChildren(this Transform target, string serchName, bool isActiveOnly)
    {
        if (isActiveOnly)
        {
            // 名前の一致する子オブジェクトをターゲットから検索
            GameObject child = target.Find(serchName).gameObject;
            return child;
        }
        else
        {
            // ターゲットの子オブジェクトを配列に格納する
            GameObject[] list = target.GetChildren();
            // 名前の一致するオブジェクトを配列から検索
            foreach (var child in list)
            {
                if (child.name == serchName)
                {
                    return child;
                }
            }
        }
        return null;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    アクティブ状態を切り替える
    /// </summary>
    /// <param name="target">対象のTransform</param>
    /// <param name="isActive">アクティブ状態</param>
    public static void SetActive(this Transform target, bool isActive)
    {
        target.gameObject.SetActive(isActive);
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    オブジェクトが存在するかの判定
    /// </summary>
    /// <param name="target">対象のTransform</param>
    /// <returns>
    ///   存在する：true
    /// 存在しない：false
    /// </returns>
    public static bool IsValid(this Transform target)
    {
        return (target != null) ? true : false;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    X座標をセットしやすくする 
    /// </summary>
    /// <param name="me">変更したい座標データ</param>
    /// <param name="val">変更するx座標の値</param>
    public static void SetPosX(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.x = val;
        me.transform.position = newPos;

    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    Y座標をセットしやすくする
    /// </summary>
    /// <param name="me">変更したい座標データ</param>
    /// <param name="val">変更するy座標の値</param>
    public static void SetPosY(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.y = val;
        me.transform.position = newPos;

    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    Z座標をセットしやすくする
    /// </summary>
    /// <param name="me">変更したい座標データ</param>
    /// <param name="val">変更するz座標の値</param>
    public static void SetPosZ(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.z = val;
        me.transform.position = newPos;

    }

}