using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankImageRender : BaseObject {

    
	private Image image;    // @brief SpriteRendererを格納する変数

    /* 使用するスプライトの変数 */
	[SerializeField] private Sprite sourceRankFirst;
	[SerializeField] private Sprite sourceRankSecond;
	[SerializeField] private Sprite sourceRankThird;
	[SerializeField] private Sprite sourceRankFource;



    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	protected override void OnAwake()
	{
		base.OnAwake();
		image = GetComponent<Image>();

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
				image.sprite = sourceRankFirst;

                break;

            case 2:
				image.sprite = sourceRankSecond;
                break;

            case 3:
				image.sprite = sourceRankThird;
                break;

            case 4:
				image.sprite = sourceRankFource;
                break;  
        }
    }
    
}
