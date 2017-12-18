using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : BaseObject {

    [SerializeField]
    private Text _countDownText;

    private ClosePopupAnimation closeAmimation;

    public bool shipMoveFlag;

    //一度だけ呼ばれたか調べるフラグ
    private bool isCallOnce;

	void Start () {
        closeAmimation = GetComponent<ClosePopupAnimation>();
        _countDownText.text = "";
        isCallOnce = false;
    }

    public override void OnUpdate()
    {
        if (!isCallOnce && closeAmimation.ClosePop)
        {
            shipMoveFlag = false;
            Debug.Log("start count down");
            StartCountDown();
            isCallOnce = true;
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDownCoroutine());
    }

    public IEnumerator CountDownCoroutine()
    {
        _countDownText.gameObject.SetActive(true);

        _countDownText.text = "3";
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        _countDownText.text = "2";
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        _countDownText.text = "1";
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        _countDownText.text = "GO!";
        Singleton<SoundPlayer>.instance.playSE("4", 0.8f);
        shipMoveFlag = true;
        Singleton<SoundPlayer>.instance.playBGM("Wind", 0.0f, true);
        Singleton<SoundPlayer>.instance.playBGM("Water", 0.0f, true);

        yield return new WaitForSeconds(1.0f);

        _countDownText.text = "";
        _countDownText.gameObject.SetActive(false);
        
    }
}
