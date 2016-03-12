using UnityEngine;
using System.Collections;

// adjusts camera for different aspect ratios
// size is based off of largest screen: iphone 6s (1080x1920)
// 	keeps a constant width in portrait mode (5.4) and landscape mode (9.6)
public class CameraFixer : MonoBehaviour 
{
	public enum ViewMode { PORTRAIT = 0, LANDSCAPE = 1 };
	public enum FixedDimension { WIDTH = 0, HEIGHT = 1 };
	public ViewMode view = ViewMode.PORTRAIT;	// portrait by default
	public FixedDimension fixedDimension = FixedDimension.WIDTH;	// width by default

	// tolerance for aspect ratios to be equal to
	private const float TOLERANCE = 0.10f;

	// values to keep constant (depending on portrait/landscape)
	// in unity units (1u = 100px)
	private const float MAX_WIDTH = 6.40f;
	private const float MAX_HEIGHT = 7.68f;


	// aspect ratios to support:
	// tall
	private const float aspect2_3 = 2.0f/3.0f;		// iphone 1,2,3,4,4s
	private const float aspect3_4 = 3.0f/4.0f;		// ipad
	private const float aspect9_16 = 9.0f/16.0f;	// iphone 5,6,6s
	private const float aspect3_5 = 3.0f/5.0f;		// other common resolution
	// wide
	private const float aspect3_2 = 3.0f/2.0f;		// iphone 1,2,3,4,4s
	private const float aspect4_3 = 4.0f/3.0f;		// ipad
	private const float aspect16_9 = 16.0f/9.0f;	// iphone 5,6,6s
	private const float aspect5_3 = 5.0f/3.0f;		// other common resolution

	// -----

	// "magic numbers" used to assign orthographic view derived in notes
	//	based on iphone 6+ screen resolution (1242x2208)
	void Start () 
	{
		// camera size based on screen size - should work for ANY device, in theory...
		float width = Screen.width;
		float height = Screen.height;
		float aspect = width/height;

		// if portrait, fix in portrait aspect, else fix in landscape
		if (view == ViewMode.PORTRAIT) {
			Screen.orientation = ScreenOrientation.Portrait;

			if (inTolerance(aspect, aspect2_3)) {		// 3:2 (iphone 3,4)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 9.315f;
				else // fix height
					Camera.main.orthographicSize = 11.04f;
				Camera.main.aspect = aspect2_3;
			}
			else if (inTolerance(aspect, aspect3_4)) {	// 4:3 (ipad)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 8.28f;
				else // fix height
					Camera.main.orthographicSize = 11.04f;				
				Camera.main.aspect = aspect3_4;
			}
			else if (inTolerance(aspect, aspect9_16)) {	// 16:9 (iphone 5,6,6p)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 11.0f;
				else // fix height
					Camera.main.orthographicSize = 11.04f;
				Camera.main.aspect = aspect9_16;
			}
			else if (inTolerance(aspect, aspect3_5)) {	// 5:3 (random devices)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 10.35f;
				else // fix height
					Camera.main.orthographicSize = 11.04f;
				Camera.main.aspect = aspect3_5;
			}
		}
		else {	// view == ViewMode.LANDSCAPE
			Screen.orientation = ScreenOrientation.Landscape;

			if (inTolerance(aspect, aspect2_3)) {		// 3:2 (iphone 3,4)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 7.36f;
				else // fix height
					Camera.main.orthographicSize = 6.21f;
				Camera.main.aspect = aspect3_2;
			}
			else if (inTolerance(aspect, aspect3_4)) {	// 4:3 (ipad)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 8.28f;
				else // fix height
					Camera.main.orthographicSize = 6.21f;
				Camera.main.aspect = aspect4_3;
			}
			else if (inTolerance(aspect, aspect9_16)) {	// 16:9 (iphone 5,6,6p)		
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 6.21f;
				else // fix height
					Camera.main.orthographicSize = 6.21f;
				Camera.main.aspect = aspect16_9;
			}
			else if (inTolerance(aspect, aspect3_5)) {	// 5:3 (random devices)
				if (fixedDimension == FixedDimension.WIDTH)
					Camera.main.orthographicSize = 6.624f;
				else // fix height
					Camera.main.orthographicSize = 6.21f;
				Camera.main.aspect = aspect5_3;
			}
		}
	}

	// -----

	// returns true if value is within aspect +/- TOLERANCE %
	private bool inTolerance(float value, float aspect)
	{
		return value >= aspect-aspect*TOLERANCE && value <= aspect+aspect*TOLERANCE;
	}

}
