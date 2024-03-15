using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    bool _recordTime;
    float _timeElapsed;

    private void Update()
    {
        if (_recordTime)
        {
            _timeElapsed += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        _recordTime = true;
    }

    public int EndTimer()
    {
        _recordTime = false;
        return (int)_timeElapsed;

/*        int minutesElapsed = (int)(_timeElapsed / 60);
        int secondsElapsed = (int)((_timeElapsed) - minutesElapsed % 60);*/
    }
}
