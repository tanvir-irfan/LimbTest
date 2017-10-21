using UnityEngine;
using System.Collections;

public class TargetDisappearScript : MonoBehaviour {

    public GameObject gameManager;
    GameController gc;

    // Use this for initialization
    void Start ( ) {
        this.gameObject.SetActive ( true );
        gc = gameManager.GetComponent<GameController> ( );
    }

    // Update is called once per frame
    void Update ( ) {

    }

    void OnTriggerEnter ( Collider other ) {
        Debug.Log ( "This = " + this.gameObject.name + " Other = " + other.gameObject.name );
        if ( gc.getIsTouchable ( ) ) {
            this.gameObject.SetActive ( false );
            gc.setIsCurrentTargetReached ( true );
        }
    }
}
