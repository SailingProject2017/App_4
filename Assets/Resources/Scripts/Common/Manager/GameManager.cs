using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseObject
{

    private bool _activeMode = false;
    private GameObject GameObj;


    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        RemoveObjectToList(this);
        AppendManagerObjectToList(this);
    }

    public bool ActiveMode
    {
        get{ return _activeMode; }
        set { _activeMode = value; }
    }

    // ゲームオブジェクトを止めるかの判断を行う
    void GameState()
    {
        // 動いている状態
        if (_activeMode)
        {
            Time.timeScale = 1;
        }
        // 止めている状態
        else
        {
            Time.timeScale = 0;
        }
        _activeMode = !_activeMode;
    }
}
