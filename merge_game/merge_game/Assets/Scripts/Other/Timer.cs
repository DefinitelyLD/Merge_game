using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance; 

    public static float secondsPasssed;
    public static int minutesPassed;
    private bool isOpened = true;

    private void Awake() => instance = this;
    public void InitializeTimer(float seconds, int minutes) 
    {
        secondsPasssed = seconds;
        minutesPassed = minutes;
    }
    private void Update()
    {
        if (isOpened) secondsPasssed += Time.deltaTime;

        if (secondsPasssed >= 60f) 
        {
            minutesPassed += 1;
            secondsPasssed -= 60;
            Debug.Log("Minutes in game: " + minutesPassed.ToString());
        } 
    }
    private void OnApplicationFocus(bool focus) => isOpened = focus;
}
