using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectNum : BaseObject
{
    // 後でなんとかする！！！！許せ！！
    //[SerializeField]
    //private int num;
    //private int _buttonNum;

    //public int ButtonNum
    //{
    //    get { return _buttonNum; }
    //    set { _buttonNum = num; }
    //}
    public eTutorialState stageID;

    public eTutorialState StageID { set; get; }

    public void OnClick(eTutorialState state)
    {
        switch (state)
        {
            case eTutorialState.eSailing:
                StageID = eTutorialState.eSailing;
                break;

            case eTutorialState.eInGameUI:
                StageID = eTutorialState.eInGameUI;
                break;

            case eTutorialState.eAccel:
                StageID = eTutorialState.eAccel;
                break;

            case eTutorialState.eCurve:
                StageID = eTutorialState.eCurve;
                break;
    
            case eTutorialState.eCPU:
                StageID = eTutorialState.eCPU;
                break;

            default:
                break;
        }
    }
}

public enum eTutorialState
{
    eSailing,
    eInGameUI,
    eAccel,
    eCurve,
    eCPU,
}