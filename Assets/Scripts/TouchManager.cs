
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    bool tap;

    Vector2 startTocuh, swipeDelta;

    // Update is called once per frame
    void Update()
    {

        if (SwipeInput.Instance.SwipeUp)
            OnSwipeUp();
        if (SwipeInput.Instance.SwipeDown)
            OnSwipeDown();
        if (SwipeInput.Instance.SwipeLeft)
            OnSwipeLeft();
        if (SwipeInput.Instance.SwipeRight)
            OnSwipeRight();
    }

    void OnSwipeUp()
    {
        playerController.command("jump");
    }

    void OnSwipeDown()
    {
        playerController.command("duck");
    }

    void OnSwipeLeft()
    {
        playerController.command("left");
    }

    void OnSwipeRight()
    {
        playerController.command("right");
    }
}