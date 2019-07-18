
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

	// Update is called once per frame
	void Update ()
	{

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
                detectSwipeAfterRelease=true;
			}

			//Detects Swipe while finger is still moving on screen
			else if (touch.phase == TouchPhase.Moved) {
				if (detectSwipeAfterRelease) {
					fingerDownPos = touch.position;
					DetectSwipe ();
                     detectSwipeAfterRelease=false;
				}
			}

			//Detects swipe after finger is released from screen
			else if (touch.phase == TouchPhase.Ended) {
				fingerDownPos = touch.position;
				//DetectSwipe ();
               detectSwipeAfterRelease=false;
			}
		}
	}

	void DetectSwipe ()
	{
		
		if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
			Debug.Log ("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0) {
				OnSwipeUp ();
			} else if (fingerDownPos.y - fingerUpPos.y < 0) {
				OnSwipeDown ();
			}
			fingerUpPos = fingerDownPos;

		} else if (HorizontalMoveValue () > SWIPE_THRESHOLD && HorizontalMoveValue () > VerticalMoveValue ()) {
			Debug.Log ("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0) {
				OnSwipeRight ();
			} else if (fingerDownPos.x - fingerUpPos.x < 0) {
				OnSwipeLeft ();
			}
			fingerUpPos = fingerDownPos;
        detectSwipeAfterRelease=false;
		} else {
			Debug.Log ("No Swipe Detected!");
		}
	}

	float VerticalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp ()
	{	
		playerController.command("jump");
	}

	void OnSwipeDown ()
	{
		playerController.command("duck");
	}

	void OnSwipeLeft ()
	{
		playerController.command("left");
	}

	void OnSwipeRight ()
	{
		playerController.command("right");
	}
}