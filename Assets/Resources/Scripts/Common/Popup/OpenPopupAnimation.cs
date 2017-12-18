using UnityEngine;
using System.Collections;

public class OpenPopupAnimation : MonoBehaviour
{


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

    public static bool activeMode = false;


    /// <summary>
    /// アニメーションさせたいオブジェクトをアタッチさせる。
    /// </summary>
    [SerializeField]
    private RectTransform rectTransform;

    void Start()
    {
        
        StartAnimation();
        activeMode = false;
    }

    public void StartAnimation()
    {
        // コルーチンを開始する
        StartCoroutine(AnimationCoroutine());

    }

    
    IEnumerator AnimationCoroutine()
    {
        Vector3 initScale = new Vector3(0, 0, 1);
        float startTime = 0;
        float passTime = 0;
        float x;
        float y;

        while (passTime - startTime <= endTime)
        {
            x = scaleCurveX.Evaluate(passTime - startTime); // endTime);
            y = scaleCurveY.Evaluate(passTime - startTime);// endTime);
            rectTransform.localScale = initScale + new Vector3(x, y, 0);
            passTime += Time.deltaTime;
            yield return 0;
        }

        yield break;

        //        rectTransform.localScale = initScale + new Vector3(x, 0, 0);

    }

}