using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSpriteRender : BaseObject {

    
	SpriteRenderer mainSpriteRender;    // @brief SpriteRendererを格納する変数

    /* 使用するスプライトの変数 */
	[SerializeField] private Sprite spriteRankFirst;
    [SerializeField] private Sprite spriteRankSecond;
    [SerializeField] private Sprite spriteRankThird;
    [SerializeField] private Sprite spriteRankFource;

    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	protected override void AppendListConstructor()
	{
		base.AppendListConstructor();
		mainSpriteRender = GetComponent<SpriteRenderer>();
	}
 
    /// <summary>
	/// @brief ランク計算に基づいてCanvas上のスプライトを変更する
    /// </summary>
    /// <param name="rank"> プレイヤーのランク </param>
	public void ChangeRankSprite(int rank)
    {
        switch (rank)
        {
            case 1:
                mainSpriteRender.sprite = spriteRankFirst;
                break;

            case 2:
                mainSpriteRender.sprite = spriteRankSecond;
                break;

            case 3:
                mainSpriteRender.sprite = spriteRankThird;
                break;

            case 4:
                mainSpriteRender.sprite = spriteRankFource;
                break;  
        }
    }
    
}
