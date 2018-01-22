using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : BaseObjectSingleton<GameInstance> {

    private StageType stageType;

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        StageType = StageType.NULL;
    }


    /// <summary>
    /// @brief  読み込むステージを判断する変数のアクセサー
    /// @set    none
    /// @get    入力されたデータを渡す
    /// </summary>
    public StageType StageType
    {
        set { stageType = value; }
        get { return stageType; }
    }

}
