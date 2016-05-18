using UnityEngine;
using System.Collections;

// a window that drops down when opened, and flies up when closed
public class PopUpWindow : MonoBehaviour 
{
	// top of the screen in pixels: window is offscreen if rect y = SCREEN_TOP + height
	private const int SCREEN_TOP = 1000;

	// how fast the window will scroll down/up
	private const int SPEED = 100;

	private RectTransform rect;	// the RectTransform that sets position
	public int positionY;		// the y-position to drop the window down to

	private Vector2 destination;
	private bool atDestination;

	// ---

	// starts the window off-screen
	// must be in Awake due to Instantiate calls - Start ends up being called after OpenWindow in LevelUIManager.Load*Message()
	void Awake () {
		rect = this.GetComponent<RectTransform>();
		rect.localPosition = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		destination = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		atDestination = true;

	}

	// ---

	// if window is not at destination, move it (up or down, for open/close)
	void Update () {
		if (!atDestination) {
			rect.localPosition = Vector2.MoveTowards(new Vector2(rect.localPosition.x, rect.localPosition.y), destination, SPEED);
			if (rect.localPosition.x == destination.x && rect.localPosition.y == destination.y) {
				atDestination = true;
			}
		}
	}

	// ---

	// drop window down from off-screen
	public void OpenWindow() {
		//rect.localPosition = new Vector2(rect.localPosition.x, positionY);
		destination = new Vector2(rect.localPosition.x, positionY);
		atDestination = false;
	}

	// drop window in place instantly
	public void InstantOpenWindow() {
		rect.localPosition = new Vector2(rect.localPosition.x, positionY);
		destination = new Vector2(rect.localPosition.x, positionY);
		atDestination = true;
	}

	// ---

	// bring window up to off-screen
	public void CloseWindow() {
		//rect.localPosition = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		destination = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		atDestination = false;
	}

	// move window offscreen instantly
	public void InstantCloseWindow() {
		rect.localPosition = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		destination = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
		atDestination = true;
	}

	// ---
}
