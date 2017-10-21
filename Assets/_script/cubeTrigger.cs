using UnityEngine;
using System.Collections;

public class cubeTrigger : MonoBehaviour {
    public GameObject cube;
    // Use this for initialization
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {

    }


    void OnTriggerEnter ( Collider other ) {

        if ( ( !game.objects.redDTO ) & game.objects.GuessSwitch & !game.objects.blueDTO ) {
            Debug.Log ( "GREEN: " + "GUESS: " + game.objects.GuessSwitch + "\tBlue: " + game.objects.blueDTO + "\tRed: " + game.objects.redDTO );
            //game.objects.redDTO = false;
            game.objects.blueDTO = true;
            Debug.Log ( "I touched the BLUE!" );
            cube.GetComponent<game> ( ).blueTouch ( );
        }
    }

}
