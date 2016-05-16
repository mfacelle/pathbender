using UnityEngine;
using System.Collections;

// a window that drops down when opened, and flies up when closed
public class PopUpWindow : MonoBehaviour 
{
	// top of the screen in pixels: window is offscreen if rect y = SCREEN_TOP + height
	private const int SCREEN_TOP = 1000;

	private RectTransform rect;	// the RectTransform that sets position
	public int positionY;		// the y-position to drop the window down to


	// ---

	// starts the window off-screen
	void Start () {
		rect = this.GetComponent<RectTransform>();
		rect.localPosition = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
	}

	// ---

	// if window is not at destination, move it (up or down, for open/close)
	void Update () {
	
	}

	// ---

	// drop window down from off-screen
	// TODO PATH-19: implement drop-down effect
	public void OpenWindow() {
		rect.localPosition = new Vector2(rect.localPosition.x, positionY);
	}

	// ---

	// bring window up to off-screen
	// TODO PATH-19: implement fly-up effect
	public void CloseWindow() {
		rect.localPosition = new Vector2(rect.localPosition.x, SCREEN_TOP + rect.rect.height);
	}

	// ---
}
