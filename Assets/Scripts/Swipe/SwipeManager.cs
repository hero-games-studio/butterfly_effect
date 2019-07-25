
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{

    void Update()
    {
        if (SwipeInput.Instance.DoubleTap)
            OnDoubleTap();
        if (SwipeInput.Instance.Tap)
            OnTap();
        if (SwipeInput.Instance.SwipeUp)
            OnSwipeUp();
        if (SwipeInput.Instance.SwipeDown)
            OnSwipeDown();
        if (SwipeInput.Instance.SwipeLeft)
            OnSwipeLeft();
        if (SwipeInput.Instance.SwipeRight)
            OnSwipeRight();
    }

    void OnDoubleTap()
    {
        Debug.Log("DoubleTap");
    }

    void OnTap()
    {
        Debug.Log("Tap");
    }

    void OnSwipeUp()
    {
        Debug.Log("SwipeUp");
    }

    void OnSwipeDown()
    {
        Debug.Log("SwipeDown");
    }

    void OnSwipeLeft()
    {
        Debug.Log("SwipeLeft");
    }

    void OnSwipeRight()
    {
        Debug.Log("SwipeRigth");
    }
}