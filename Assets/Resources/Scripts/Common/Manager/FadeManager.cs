/***********************************************************************/
/*! @file   FadeManager.cs
*************************************************************************
*   @brief  フェードの制御をするマネージャークラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FadeManager : BaseObject
{

    #region Singleton 


    private static FadeManager _instance;


    public static FadeManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (_instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return _instance;
        }
    }

 	#endregion Singleton 


    [SerializeField]
    private Image fade; // @brief 黒い画像

    private Color fadeColor = Color.black; // @brief フェードの色を指定

    private const float FADETIME = 1f; // @brief フェードにかける時間を定義

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        fade.raycastTarget = false;
    }

    /// <summary>
    /// @brief フェードをさせるコルーチン呼び出し
    /// </summary>
    public void Load(int scene)
    {
        FadeManager.instance.StartCoroutine(SceneLoad(scene));
    }

    /// <summary>
    /// @brief フェードを行うコルーチン
    /// </summary>
    IEnumerator SceneLoad(int scene)
    {
        Debug.Log("aaaaaaaaaaaa");
        fade.raycastTarget = true;
        fade.color = new Color(0, 0, 0, 0);

        // フェードイン
        while (fade.color.a < 1)
        {
            fade.color += new Color(0, 0, 0, 1f * (FADETIME * Time.deltaTime));
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 1);

        yield return null;

        SceneManager.LoadScene(scene);

        // フェードアウト
        while (fade.color.a > 0)
        {
            fade.color -= new Color(0, 0, 0, 1f * (FADETIME * Time.deltaTime));
            yield return null;
        }

        fade.color = new Color(0, 0, 0, 0);
        fade.raycastTarget = false;
    }
}