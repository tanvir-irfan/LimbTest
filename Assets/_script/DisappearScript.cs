using UnityEngine;
using System.Collections;

public class DisappearScript : MonoBehaviour {
    static float farthestCube = -10f;
    
    // Use this for initialization
    void Start ( ) {
        this.gameObject.SetActive ( true );
    }

    // Update is called once per frame
    void Update ( ) {

    }

    void OnTriggerEnter ( Collider other ) {
        //Debug.Log ( "This = " + this.gameObject.name + " Other = " + other.gameObject.name );
        if ( other.gameObject.tag != "CALIBRATION_BOX" ) {
            
            if(this.gameObject.transform.position.z > farthestCube) {
                PlayerPrefs.SetFloat ( "farthestCube" , this.gameObject.transform.position.z );
                Debug.Log ( "Current pos = " + this.gameObject.transform.position.z );
            }
            this.gameObject.SetActive ( false );
        }
    }
}
