using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class noCubeTrigger : MonoBehaviour {
    public GameObject cube;
    public Text Notify;
    int alreadyTouched = 0;
    //int redCounter = 1;
    // Use this for initialization
    void Start ( ) {
        //cube.GetComponent<game>();
        Notify.enabled = false;
    }


    void OnTriggerEnter ( Collider other ) {
        //Debug.Log(redCounter++);

        if ( alreadyTouched % 50 == 0 ) {
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                //Debug.Log(other.gameObject.name);
                game.objects.redDTO = true;
                //cube.GetComponent<game>().redTouch();
                Notify.enabled = true;
            }

            Debug.Log ( "RED: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        }
        alreadyTouched++;
    }

    void OnTriggerStay ( Collider other ) {
        //Debug.Log(redCounter++);
        //Debug.Log("RED: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO);
        if ( alreadyTouched % 50 == 0 ) {
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                //Debug.Log(other.gameObject.name);
                game.objects.redDTO = true;
                //cube.GetComponent<game>().redTouch();
                Notify.enabled = true;
            }

            Debug.Log ( "RED: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        }
        alreadyTouched++;
    }

    void OnTriggerExit ( Collider other ) {
        //Debug.Log(redCounter++);
        Debug.Log ( "RED: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        if ( alreadyTouched % 50 == 0 ) {
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                //Debug.Log(other.gameObject.name);
                game.objects.redDTO = true;
                game.objects.GuessSwitch = true;
                //cube.GetComponent<game>().redTouch();
                Notify.enabled = true;
            }

            Debug.Log ( "RED: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        }
        alreadyTouched++;
    }

}
