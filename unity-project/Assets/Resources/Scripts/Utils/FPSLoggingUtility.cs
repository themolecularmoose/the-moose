
using UnityEngine;
using System.Collections;
using System.Text;

public class FPSLoggingUtility : MonoBehaviour 
{

	// Code modified from http://wiki.unity3d.com/index.php?title=FramesPerSecond
	//
	// It calculates frames/second over each updateInterval,
	// so the display does not keep changing wildly.
	//
	// It is also fairly accurate at very low FPS counts (<10).
	// We do this not by simply counting frames per interval, but
	// by accumulating FPS for each frame. This way we end up with
	// correct overall FPS even if the interval renders something like
	// 5.5 frames.

	private static FPSLoggingUtility instance; 

	//Interval for each fps reading
	public float fpsCalcInterval = 1.0F; 
	//Interval for max/min/avg fps dumped to log
	public float fpsLogInterval  = 15.0F; 
	
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float calcTimeLeft; //Left time for individual fps data point
	private float logTimeLeft; // Left time for current fps log interval

	private string currentLevel; //name of the current scene
	private float logDuration; //time in seconds of fps log recording
	private float logMinimum; //minimum fps recording in log interval
	private float logMaximum; //maximum fps recording in log interval
	private float logAverage; //average fps recording in log interval
	private int	logRecordings; //number of log recordings in log iterval
	
	//for singleton
	public static FPSLoggingUtility GetInstance() {
		return instance;
	}

	//for singleton
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//sets up initial variables
	void Start()
	{
		calcTimeLeft = fpsCalcInterval;  
		ResetLogVariables ();
	}

	//performs fps calculations
	void Update()
	{
		calcTimeLeft -= Time.deltaTime;
		logTimeLeft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( calcTimeLeft <= 0.0 )
		{
			float fps = accum/frames;
			// update and output log interval stats if necessary
			UpdateLogStats( fps );
			//reset fpsReading variables
			calcTimeLeft = fpsCalcInterval;
			accum = 0.0F;
			frames = 0;
		}
	}

	void UpdateLogStats(float fps)
	{
		//Scene changed dump data & reset vars
		if ( !currentLevel.Equals (Application.loadedLevelName)) {
			OutputData ();
		}
	
		//update log variables always.
		logRecordings++;
		logDuration += fpsCalcInterval;
		if (fps < logMinimum) {
			logMinimum = fps;
		}

		if (fps > logMaximum) {
			logMaximum = fps; 
		}

		//calculates moving average
		logAverage = (logAverage * (logRecordings - 1) / logRecordings)
			+ (fps / logRecordings);

		//if log interval satisfied, dump data
		if (logTimeLeft <= 0.0) {
			OutputData ();
		}
	}

	//Resets the variables for log interval fps statistics
	void ResetLogVariables()
	{
		currentLevel = Application.loadedLevelName;
		logTimeLeft = fpsLogInterval;
		logDuration = 0;
		logMinimum = float.MaxValue;
		logMaximum = float.MinValue;
		logAverage = 0;
		logRecordings = 0;
	}

	//Writes log interval fps statistics to console and resets vars
	void OutputData()
	{
		StringBuilder sb = new StringBuilder ();
		sb.Append ("FPS Data: {");
		sb.Append ("level:" + currentLevel + ",");
		sb.Append ("dur:" + logDuration + ",");
		sb.Append ("min:" + logMinimum + ",");
		sb.Append ("max:" + logMaximum + ",");
		sb.Append ("avg:" + logAverage + "}\n");
		WriteToConsole (sb.ToString ());
		ResetLogVariables ();
	}

	//Writes to console differently depending on context.
	void WriteToConsole( string message, Object context = null )
	{
#if UNITY_EDITOR
		Debug.Log ( message, context);
#else
		Application.ExternalCall ("console.log", message);
#endif 
	}
}