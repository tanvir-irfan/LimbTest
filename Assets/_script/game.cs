using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class game : MonoBehaviour {

    /// <summary>
    /// 1, objects class, we can manipulate the objects in the game by creating a instance of this class
    /// </summary>
    public class objects {
        private string playerName;
        private GameObject cube;
        //private BoxCollider cubeCollider;
        private GameObject yesCube;
        //private BoxCollider yesCubeCollider;
        private GameObject noCube;
        //private BoxCollider noCubeCollider;
        private int cubeLocations;
        private float steps;
        private bool noTrigger;
        private int cubeNum;
        private StringBuilder outputStringBuilder;
        //private StringBuilder outputPerRound; 
        private bool [ ] cubeTriggerBool;
        public int issueCounter;

        //data transfer between cubeTest and those three gameObjects' colliders
        public static bool redDTO = false;
        //public static bool greenDTO = false;
        public static bool GuessSwitch = false;
        public static bool blueDTO = false;

        /// <summary>
        /// constructor
        /// </summary>
        public objects ( ) {
            playerName = "Albe";
            cubeLocations = 7;
            steps = 0.01f;
            noTrigger = false;
            //outputStringBuilder = new StringBuilder()
            outputStringBuilder = new StringBuilder ( Time.time.ToString ( ) + "\t" );
            issueCounter = 0;
        }

        /// <summary>
        ///accessor methods
        /// </summary>
        /// <returns></returns>
        public string getPlayerName ( ) {
            return playerName;
        }
        public void setPlayerName ( string playerName ) {
            this.playerName = playerName;
            outputStringBuilder.AppendLine ( playerName );
        }

        public GameObject getCube ( ) {
            return cube;
        }
        public void setCube ( GameObject cube ) {
            this.cube = cube;
        }

        public int getCubeLocations ( ) {
            return cubeLocations;
        }
        public void setCubeLocations ( int cubeLocations ) {
            this.cubeLocations = cubeLocations;
        }

        public bool getNoTrigger ( ) {
            return noTrigger;
        }
        public void setNoTrigger ( bool noTrigger ) {
            this.noTrigger = noTrigger;
        }

        public int getCubeNum ( ) {
            return cubeNum;
        }
        public void setCubeNum ( int cubeNum ) {
            this.cubeNum = cubeNum;
        }

        public void setOutput ( StringBuilder outputStringBuilder ) {
            this.outputStringBuilder.Append ( outputStringBuilder.ToString ( ) );
        }

        public void setYesCube ( GameObject yesCube ) {
            this.yesCube = yesCube;
        }
        public GameObject getYesCube ( ) {
            return yesCube;
        }

        public void setNoCube ( GameObject noCube ) {
            this.noCube = noCube;
        }
        public GameObject getNoCube ( ) {
            return noCube;
        }

        public void setSteps ( float steps ) {
            this.steps = steps;
        }
        public float getSteps ( ) {
            return steps;
        }

        public void initiCubeStatus ( int size ) {
            Debug.Log ( "size = " + size );
            cubeTriggerBool = new bool [ size ];
            for ( int i = 0; i < size; i++ ) {
                cubeTriggerBool [ i ] = false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void issueCube ( ) {

            //bool[] cubeTrigger = new bool[cubeLocations];
            int cubeNum_temp = UnityEngine.Random.Range ( 0 , cubeLocations );
            while ( cubeTriggerBool [ cubeNum_temp ] ) {
                cubeNum_temp = UnityEngine.Random.Range ( 0 , cubeLocations );
            }
            cubeTriggerBool [ cubeNum_temp ] = true;
            Vector3 offset = new Vector3 ( 0 , 0 , steps * ( cubeNum_temp - cubeLocations / 2 ) );
            cube.transform.Translate ( offset );
            Debug.Log ( "cube's position: " + cube.transform.position + "\t cubeNum_temp is: " + cubeNum_temp + "\t final placement: "
                + steps * ( cubeNum_temp - cubeLocations / 2 ) );
            setCubeNum ( cubeNum_temp );

            //return getCubeNum();
            cube.SetActive ( true );
            issueCounter++;
            Debug.Log ( "issue counter: " + issueCounter );

        }

        public void output ( ) {

        }
    }




    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 2. Use this for initialization
    /// </summary>

    public string outputPath;
    public String path;

    public GameObject cube;
    public BoxCollider cubeCollider;
    public GameObject yesCube;
    public BoxCollider yesCubeCollider;
    public GameObject noCube;
    public BoxCollider noCubeCollider;

    public string playerName;
    public int cubeLocations;
    public float steps;
    public StringBuilder outputStringBuilder;
    public StringBuilder outputPerRound;
    public static bool guessTrigger;
    //public float countDown;
    private bool countDownTrigger;
    public int waitforsecondperround;

    public Text CountDowntimerText;
    private float timeLeft;

    public Text roundNotice;

    private int issueCounter;


    objects cubeTest;

    void Start ( ) {
        if ( outputPath.Length == 0 ) {
            //path = "C:\\Users\\jzs328\\Downloads\\LimbTest_Beta_04_26\\LimbTest_Beta\\Output\\test.txt";
            path = "C:\\Users\\abl905\\Desktop\\TanvirIrfan\\LimbTest_Beta\\Output\\test.txt";
            outputPath = @path;
        }

        roundNotice.enabled = false;
        cubeTest = new objects ( );


        if ( playerName.Equals ( "" ) ) {
            cubeTest.getPlayerName ( );
        } else {
            cubeTest.setPlayerName ( playerName );
        }
        cubeTest.setCube ( cube );

        if ( cubeLocations == 0 ) {
            cubeTest.setCubeLocations ( 7 );
        } else {
            cubeTest.setCubeLocations ( cubeLocations );
        }
        cubeTest.initiCubeStatus ( cubeTest.getCubeLocations ( ) );

        if ( steps == 0 ) {
            cubeTest.setSteps ( 0.01f );
        } else {
            cubeTest.setSteps ( steps );
        }
        cubeTest.setNoCube ( noCube );
        cubeTest.setNoTrigger ( false );
        cubeTest.setYesCube ( yesCube );

        if ( !File.Exists ( outputPath ) ) {
            outputStringBuilder = new StringBuilder ( cubeTest.getPlayerName ( ) + "\t" + "Steps: " + cubeTest.getSteps ( ) + "\tNum of Locations: " + cubeTest.getCubeLocations ( ) +
                "\t" + System.Environment.NewLine +
                "Cube Num" + "\t" + "Guess" + "\t" + "Touch" + System.Environment.NewLine );
            File.WriteAllText ( outputPath , outputStringBuilder.ToString ( ) );
        } else {
            File.AppendAllText ( outputPath , System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                 cubeTest.getPlayerName ( ) + "\t" + "Steps: " + cubeTest.getSteps ( ) + "\tNum of Locations: " + cubeTest.getCubeLocations ( ) + System.Environment.NewLine +
                 "Cube Num" + "\t" + "Guess" + "\t" + "Touch" + System.Environment.NewLine );
        }

        outputPerRound = new StringBuilder ( "" );
        //cubeTest.setOutput(outputStringBuilder);
        guessTrigger = false;
        CountDowntimerText.enabled = false;

        if ( waitforsecondperround == 0 ) {
            waitforsecondperround = 2;
        }
        
        shuffle ( );

    }

    /*
                    guess <-----------------------------                           
                    /    \                             ^
                   /yes   \no                          |
                  /        \                           |
              green(true)   red(true)                  |
                /            \                         |
           touch?             green(true)          shuffle
           /    \                |                     ^
          /yes   \no             |                     |
         /        \              |                     |
     blue(true)    blue(false)   |                     |
         |             |         |                     |
         |             |         |                     |
         |_____________|_________|_____________________|


    */


    //call this function when green cube is touched
    public void greenTouch ( ) {
        if ( game.objects.GuessSwitch & game.objects.redDTO ) {
            //========take care of the cubes' switches====
            game.objects.redDTO = false;
            game.objects.GuessSwitch = false;
            game.objects.blueDTO = false;
            //============================================
            cubeTest.getYesCube ( ).SetActive ( false );
            cubeTest.getNoCube ( ).SetActive ( false );
            cubeTest.getCube ( ).SetActive ( false );
            //output GUESS NO
            outputPerRound.Append ( cubeTest.getCubeNum ( ) + "\t\t" + "No" + "\t" + "No" + System.Environment.NewLine );
            StartCoroutine ( timeDelay ( ) );

        }
        if ( game.objects.GuessSwitch & !game.objects.redDTO ) {
            //game.objects.greenDTO = false;
            //========take care of the cubes' switches====
            //keep GuessSwitch on for Blue Cube
            //============================================
            //finished guess, go and try to touch the blue one.
            //in 5 seconds
            Debug.Log ( cubeTest.getYesCube ( ) );
            cubeTest.getYesCube ( ).SetActive ( false );
            //Debug.Log(cubeTest.getYesCube().activeSelf);
            cubeTest.getNoCube ( ).SetActive ( false );
            /*
            if (cubeTest.getCube().activeSelf & !cubeTest.getYesCube().activeSelf & !cubeTest.getNoCube().activeSelf)
            {
                StartCoroutine(timeForFecthingWithYes());
            }
            */
        }
    }

    /*
    public void redTouch()
    {
        //game.objects.redDTO = true;
    }
    */

    public void blueTouch ( ) {
        //output
        if ( game.objects.GuessSwitch & game.objects.blueDTO & !game.objects.redDTO ) {
            //========take care of the cubes' switches====
            game.objects.GuessSwitch = false;
            game.objects.blueDTO = false;
            game.objects.redDTO = false;
            //============================================
            cubeTest.getCube ( ).SetActive ( false );
            outputPerRound.Append ( cubeTest.getCubeNum ( ) + "\t\t" + "Yes" + "\t" + "Yes" + System.Environment.NewLine );
            //System.Console.WriteLine(outputStringBuilder.ToString());
            CountDowntimerText.enabled = false;
            StartCoroutine ( timeDelay ( ) );
        }
    }

    public void shuffle ( ) {
        //StartCoroutine(timeForFecthingWithYes());
        timeLeft = waitforsecondperround + 3;

        cubeTest.getNoCube ( ).SetActive ( true );
        cubeTest.getYesCube ( ).SetActive ( true );
        if ( cubeTest.issueCounter < cubeTest.getCubeLocations ( ) ) {
            cubeTest.issueCube ( );
        } else {
            roundNotice.text = "This is the End for this Round!";
            roundNotice.enabled = true;
            cubeTest.getCube ( ).SetActive ( false );
            cubeTest.getNoCube ( ).SetActive ( false );
            cubeTest.getYesCube ( ).SetActive ( false );
            File.AppendAllText ( outputPath , outputPerRound.ToString ( ) );
        }
    }

    //Count Down Timer for player, Guess Green and use this time to touch Blue
    void Update ( ) {
        if ( game.objects.GuessSwitch & !game.objects.redDTO & !game.objects.blueDTO ) {
            CountDowntimerText.enabled = true;
            CountDowntimerText.text = " Time left: " + Mathf.Round ( timeLeft );
            timeLeft -= Time.deltaTime;
        }
        if ( timeLeft <= 0 ) {
            CountDowntimerText.enabled = false;
            game.objects.GuessSwitch = false;
            //outputPerRound.Append(cubeTest.getCubeNum() + "\t\t" + "Yes" + "\t" + "No" + System.Environment.NewLine);
            shuffle ( );
        }
    }


    //WaitForSeconds
    IEnumerator timeDelay ( ) {
        yield return new WaitForSeconds ( waitforsecondperround );
        shuffle ( );
    }

    /*
    IEnumerator timeForFecthingWithYes()
    {
        yield return new WaitForSeconds(waitforsecondperround + 3);
        outputPerRound.Append(cubeTest.getCubeNum() + "\t\t" + "Yes" + "\t" + "No" + System.Environment.NewLine);
        //System.Console.WriteLine(outputStringBuilder.ToString());
        shuffle();
    }
    */
}