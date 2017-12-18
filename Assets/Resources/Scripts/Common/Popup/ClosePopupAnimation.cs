using UnityEngine;
using System.Collections;

public class ClosePopupAnimation : MonoBehaviour {


    /// <summary>
    /// コルーチン処理の終了時間。アニメーションの終了時刻に合わせる。
    /// </summary>
    [SerializeField]
    private float endTime = 0.15f;

    // AnimationCurve.Linearの引数:開始時間、初期値、終了時間、終了値
    [SerializeField]
    private AnimationCurve scaleCurveX = AnimationCurve.Linear(0, 1, 1, 1);

    [SerializeField]
    private AnimationCurve scaleCurveY = AnimationCurve.Linear(0, 1, 1, 1);

    /// <summary>
    /// アニメーションさせたいオブジェクトをアタッチさせる。
    /// </summary>
    [SerializeField]
    private RectTransform rectTransform;

    private CountDown _countDown;

    private bool isClosePop;
    public bool ClosePop
    {
        get { return isClosePop; }
    }


    private void StartAnimation()
    {
        // コルーチンを開始する
        isClosePop = false;
        StartCoroutine(AnimationCoroutine());
    }



    private IEnumerator AnimationCoroutine()
    {
        Vector3 initScale = new Vector3(0, 0, 1);
        float startTime = 0;
        float passTime = 0;
        float x;
        float y;

        while(passTime - startTime <= endTime)
        {
            x = scaleCurveX.Evaluate(passTime - startTime); // endTime);
            y = scaleCurveY.Evaluate(passTime - startTime);// endTime);
            rectTransform.localScale = initScale + new Vector3(x, y, 0);
            passTime += Time.deltaTime;
            yield return 0;
        }
        isClosePop = true;
            yield break;
        
        //        rectTransform.localScale = initScale + new Vector3(x, 0, 0);

    }


}