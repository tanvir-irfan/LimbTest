using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject targetObject;

	public GameObject choices;
	public GameObject timer;
	public Text timerText;
	public GameObject allWalls;
	//TEST_CODE
	public Text positionText;
	private bool DEBUG_FLAG = false;

	//###################################### VALUES FROM INSPECTOR WINDOW ########################
	Dictionary<String, int> conf;
	// this three values will come form the configuration file kept at the same folder of the executable
	private int POS_BELOW_MAX_REACH;
	// = 5;
	private int POS_ABOVE_MAX_REACH;
	// = 5;
	private int NUMBER_OF_TRIAL_PER_POSITION;
	// = 3;

	private int REMAINING_TIME_TO_TOUCH_TARGET;
	// = 5;
	private int NEXT_TRIAL_START_TIME;
	// = 5;

	private int LOWER_RANGE;
	// = 5;
	private int HIGHER_RANGE;
	// = 5;
	//###################################### VALUES FROM INSPECTOR WINDOW ########################


	bool isTimerShowing = false;
	float remainingTime;
	// = REMAINING_TIME;

	float farthestCube;
	float lowerLimit;
	float higherLimit;

	int totalTrials;
	int currentTrialNumber;

	bool isCurrentTargetReached = false;
	bool isTouchable = false;

	// this list will contains (NUMBER_OF_POSITION * NUMBER_OF_TRIAL_PER_POSITION) positions.
	ArrayList listOfPositions = new ArrayList ();
	TrialResult[ ] trialRes;
	// Use this for initialization
	TrialResult currentTrialResult;

	void Start ()
	{

		readConfigurationFile ();

		//tanvirirfan.utsa this needs to come from PlayerPrefs
		farthestCube = PlayerPrefs.GetFloat ("farthestCube");        

		lowerLimit = farthestCube - (LOWER_RANGE / 10.0f);
		higherLimit = farthestCube + (HIGHER_RANGE / 10.0f);
		Debug.Log ("farthestCube = " + farthestCube);


		// adding all the random positions. same position is being added "NUMBER_OF_TRIAL_PER_POSITION" times.
		for (int i = 0; i < POS_BELOW_MAX_REACH; i++) {
			float position = UnityEngine.Random.Range (lowerLimit, farthestCube);
			for (int j = 0; j < NUMBER_OF_TRIAL_PER_POSITION; j++) {
				listOfPositions.Add (position);
				Debug.Log (position);
			}
		}

		for (int i = 0; i < POS_ABOVE_MAX_REACH; i++) {
			float position = UnityEngine.Random.Range (farthestCube, higherLimit);
			for (int j = 0; j < NUMBER_OF_TRIAL_PER_POSITION; j++) {
				listOfPositions.Add (position);
				Debug.Log (position);
			}
		}

		totalTrials = (POS_ABOVE_MAX_REACH + POS_BELOW_MAX_REACH) * NUMBER_OF_TRIAL_PER_POSITION;
		currentTrialNumber = 0;
		trialRes = new TrialResult [ totalTrials + 1 ];

		targetObject.SetActive (false);
		timer.SetActive (false);
		choices.SetActive (false);
		isTimerShowing = false;

		//starting the game from here
		nextCubeLocation ();
	}

	bool isWallVisible = true;
	// Update is called once per frame
	void Update ()
	{
		remainingTime -= Time.deltaTime;

		if (isTimerShowing) {
			showTimer ();
		}

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
//			Debug.Log ("SHIFT");
			if (Input.GetKeyDown (KeyCode.W)) {
//				Debug.Log ("W");
				isWallVisible = !isWallVisible;
			}
		}


		allWalls.SetActive (isWallVisible);
	}

	void readConfigurationFile ()
	{
		conf = new Dictionary<string, int> ();
		//String conf_Path = "C:\\Users\\SAVE_UTSA\\Desktop\\Tanvir\\LimbTest_Beta\\_build\\configuration_file.txt";
		String conf_Path = "configuration_file.txt";

		StreamReader read = new StreamReader (conf_Path);
		if (read == null) {
			Debug.Log ("configuration_file.txt not found or not readable");
		} else { 
			String line = read.ReadLine ();
			while (line != null) {
				char delim = '=';
				line = read.ReadLine ();
				//Debug.Log ( line );
				if (line != null && line.Contains (delim.ToString ()) && !line.Contains ("#")) {
					String[] singleConf = line.Split (delim);
					conf.Add (singleConf [0], Convert.ToInt32 (singleConf [1]));
				}
			}
		}
		conf.TryGetValue ("POS_BELOW_MAX_REACH", out POS_BELOW_MAX_REACH);
		conf.TryGetValue ("POS_ABOVE_MAX_REACH", out POS_ABOVE_MAX_REACH);
		conf.TryGetValue ("NUMBER_OF_TRIAL_PER_POSITION", out NUMBER_OF_TRIAL_PER_POSITION);
		conf.TryGetValue ("REMAINING_TIME_TO_TOUCH_TARGET", out REMAINING_TIME_TO_TOUCH_TARGET);
		conf.TryGetValue ("NEXT_TRIAL_START_TIME", out NEXT_TRIAL_START_TIME);
		conf.TryGetValue ("LOWER_RANGE", out LOWER_RANGE);
		conf.TryGetValue ("HIGHER_RANGE", out HIGHER_RANGE);
	}

	public void choiceSelected (string tag)
	{
		timerText.text = this.gameObject.tag;
		choices.SetActive (false);
		timer.SetActive (true);

		isTimerShowing = true;
		isTouchable = true;
		switch (tag) {
		case "C_REACHABLE":
			remainingTime = REMAINING_TIME_TO_TOUCH_TARGET;
			currentTrialResult.guess = true;
			break;
		case "C_NOT_REACHABLE":
			remainingTime = NEXT_TRIAL_START_TIME;
			currentTrialResult.guess = false;
			break;
		}

		currentTrialResult.choiceSelectedTime = CurrentMillis.Millis;
		StartCoroutine (resetTimerAndChoices ((int)remainingTime));
	}

	void showTimer ()
	{
		timerText.text = (int)remainingTime + "";
	}

	public void setIsCurrentTargetReached (bool b)
	{
		isCurrentTargetReached = b;
		currentTrialResult.touchTargetTime = CurrentMillis.Millis;
	}

	public bool getIsTouchable ()
	{
		return isTouchable;
	}

	IEnumerator resetTimerAndChoices (int waitTime)
	{

		yield return new WaitForSeconds (waitTime);

		isTimerShowing = false;

		timer.SetActive (false);
		choices.SetActive (true);

		//before going to next location, save current data
		currentTrialResult.isTouched = isCurrentTargetReached;
		trialRes [currentTrialNumber] = currentTrialResult;
		nextCubeLocation ();

	}

	void nextCubeLocation ()
	{
		currentTrialResult = new TrialResult ();

		currentTrialNumber++;
        
		// to calculate choice selection time
		currentTrialResult.trialStartTime = CurrentMillis.Millis;

		if (currentTrialNumber <= totalTrials) {
			float z = getPositionZ ();
			Vector3 offset = new Vector3 (0, 0.6f, z);
			targetObject.transform.position = offset;
			targetObject.SetActive (true);

			choices.SetActive (true);

			isCurrentTargetReached = false;
			isTouchable = false;

			//logging data
			currentTrialResult.trialNumber = currentTrialNumber;
			currentTrialResult.position = z;

			//TEST_CODE
			if (DEBUG_FLAG)
				positionText.text = "CT = " + currentTrialNumber + "/" + totalTrials + " [" + targetObject.transform.position.z + "]";
			else
				positionText.text = "CT = " + currentTrialNumber;
			//positionText.text = "CT = " + currentTrialNumber + " [" + targetObject.transform.position.x + ", " + targetObject.transform.position.y + ", " + targetObject.transform.position.z + "]";
		} else {
			Debug.Log ("Thank You!");
			positionText.text = "Thank You!";

			targetObject.SetActive (false);
			timer.SetActive (false);
			choices.SetActive (false);

			writeToFile ();
		}

	}

	void writeToFile ()
	{
		//String outputPath = "C:\\Users\\abl905\\Desktop\\TanvirIrfan\\LimbTest_Beta\\_build\\test.txt";
		String outputPath = "test.txt";
		String output = "[NearRange,Reach,FarRange] = [" + lowerLimit + ", " + farthestCube + ", " + higherLimit + "]" + System.Environment.NewLine;
		output += "Trial\tPosition\tGuess\tIsReached\tTrialStart\tChoiceSelected\tTargetTouched" + System.Environment.NewLine;
		StringBuilder outputStringBuilder = new StringBuilder (output);

		foreach (TrialResult tr in trialRes) {
			if (tr != null)
				outputStringBuilder.Append (tr.trialNumber + "\t" + tr.position + "\t" + tr.guess + "\t" + tr.isTouched + "\t" + tr.trialStartTime + "\t" + tr.choiceSelectedTime + "\t" + tr.touchTargetTime + System.Environment.NewLine);
		}
		if (!File.Exists (outputPath)) {
			File.WriteAllText (outputPath, outputStringBuilder.ToString ());
		} else {
			File.AppendAllText (outputPath, outputStringBuilder.ToString ());
		}
	}

	float getPositionZ ()
	{
		int posIndex = UnityEngine.Random.Range (0, listOfPositions.Count);
		float z = (float)listOfPositions [posIndex];
		return z;
	}

	class TrialResult
	{
		public int trialNumber;
		public bool guess;
		public bool isTouched;
		public float position;
		public long trialStartTime;
		public long choiceSelectedTime;
		public long touchTargetTime;
	}

	/// <summary>Class to get current timestamp with enough precision</summary>
	static class CurrentMillis
	{
		private static readonly DateTime Jan1St1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>Get extra long current timestamp</summary>
		public static long Millis { get { return (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds); } }
	}
}
