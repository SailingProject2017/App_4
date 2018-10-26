/**************************************************************************************/
/*! @file   UpdateTimeText.cs
***************************************************************************************
@brief      TimeManagerのタイムを取得して、テキストに反映させます
*********************************************************************************************
* @note     2018-06-29 制作
*********************************************************************************************
* @author   Tsuchida Shun
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateTimeText : BaseObject
{
    [SerializeField] private Text text;              // @brief タイマーの時間を反映させたいテキスト
    [SerializeField] private GameObject obj;         // @brief テキストに反映させたい、TimeManagerの入ったオブジェクト
    [SerializeField] private List<Sprite> TimeSprite; // @brief 画像格納用
    private TimeManager seconds;                     // @brief 秒
    private TimeManager minute;                      // @brief 分 
    private SpriteRenderer spriteRenderer;           // @brief SpriteRendererを格納する変数

    /// <summary>
    /// @brief objのTimeManagerをアタッチします
    /// </summary>
    private void Start()
    {
        seconds = obj.GetComponent<TimeManager>();
        minute = obj.GetComponent<TimeManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TimeSprite = new List<Sprite>();
    }

    /// <summary>
    /// @brief TimeManagerのMillTimeを取得してテキストに代入します。
    /// </summary>
    public void UpdateText()
    {
        string text = minute.Minute.ToString("00") + "." + seconds.MillTime.ToString("0#.##");

     for(int i = 0;i< text.Length; i++)
        {
            switch (text[i]){
                case '0':
                    spriteRenderer.sprite = TimeSprite[0];
                    break;
                case '1':
                    spriteRenderer.sprite = TimeSprite[1];
                    break;
                case '2':
                    spriteRenderer.sprite = TimeSprite[2];
                    break;
                case '3':
                    spriteRenderer.sprite = TimeSprite[3];
                    break;
                case '4':
                    spriteRenderer.sprite = TimeSprite[4];
                    break;
                case '5':
                    spriteRenderer.sprite = TimeSprite[5];
                    break;
                case '6':
                    spriteRenderer.sprite = TimeSprite[6];
                    break;
                case '7':
                    spriteRenderer.sprite = TimeSprite[7];
                    break;
                case '8':
                    spriteRenderer.sprite = TimeSprite[8];
                    break;
                case '9':
                    spriteRenderer.sprite = TimeSprite[9];
                    break;
                case '.':
                    spriteRenderer.sprite = TimeSprite[10];
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// @brief UpdateTextを呼びます。
    /// </summary>
    public override void OnUpdate()
    {
        UpdateText();
    }

}
    