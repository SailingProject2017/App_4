using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class TimeManager : BaseObject
{
    private bool isStart;

    [SerializeField]
    Text timeText;

    protected override void OnAwake()
    {
        base.OnAwake();
        RemoveObjectToList(this);
    }

    public string GetTimeText
    {
        get { return timeText.text; }
    }

    private System.TimeSpan time = System.TimeSpan.Zero;
    public System.TimeSpan GetTime
    {
        get { return time; }
    }

    public void StartTimer()
    {
        isStart = true;
        time = System.TimeSpan.Zero;
        timeText.text = "00:00:00";
        StartCoroutine(OnTimerStart());
    }
  
    public void StopTimer()
    {
        isStart = false;
    }

    private IEnumerator OnTimerStart()
    {
        System.DateTime startTime = System.DateTime.Now;

        while (isStart)
        {
            var diff = System.DateTime.Now - startTime;

            timeText.text =
                  ((diff.Minutes <= 10) ? "0" + diff.Minutes.ToString() : diff.Minutes.ToString()) + ":"
                + ((diff.Seconds <= 10) ? "0" + diff.Seconds.ToString() : diff.Seconds.ToString()) + ":"
                + ((diff.Milliseconds <= 10) ? "0" + diff.Milliseconds.ToString()[0] : diff.Milliseconds.ToString()[0].ToString() + diff.Milliseconds.ToString()[1].ToString());

            time = diff;
            yield return null;
        }
    }
}

