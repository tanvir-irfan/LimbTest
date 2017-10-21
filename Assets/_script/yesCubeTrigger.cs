using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class yesCubeTrigger : MonoBehaviour {
    public GameObject cube;
    int alreadyTouched = 0;
    public Text Notify;
    //private GameObject anotherGameObject;
    //private game yes;
    // Use this for initialization
    void Start ( ) {
        Notify.enabled = false;
    }

    void OnTriggerStay ( Collider other ) {
        if ( alreadyTouched % 50 == 0 ) {
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                //Debug.Log(other.gameObject.name);
                game.objects.GuessSwitch = true;
                cube.GetComponent<game> ( ).greenTouch ( );
            }

            Debug.Log ( "GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        }
        alreadyTouched++;
        if ( Notify.enabled ) {
            Notify.enabled = false;
        }
    }

    void OnTriggerEnter ( Collider other ) {
        if ( alreadyTouched % 50 == 0 ) {
            //Debug.Log("GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO);
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                //Debug.Log(other.gameObject.name);
                game.objects.GuessSwitch = true;
                cube.GetComponent<game> ( ).greenTouch ( );
            }

            Debug.Log ( "GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
        }
        alreadyTouched++;
        if ( Notify.enabled ) {
            Notify.enabled = false;
        }
    }

    void OnTriggerExit ( Collider other ) {
        if ( alreadyTouched % 50 == 0 ) {
            //Debug.Log("GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO);
            if ( other.gameObject.name == "bone1" || other.gameObject.name == "bone2" || other.gameObject.name == "bone3" ) {
                {
                    //Debug.Log(other.gameObject.name);
                    game.objects.GuessSwitch = true;
                    cube.GetComponent<game> ( ).greenTouch ( );
                }

                Debug.Log ( "GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
            }
            alreadyTouched++;
            if ( Notify.enabled ) {
                Notify.enabled = false;
            }
        }
    }
}



