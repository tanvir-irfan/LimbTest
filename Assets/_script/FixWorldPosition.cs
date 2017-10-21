using UnityEngine;
using System.Collections;

public class FixWorldPosition : MonoBehaviour {

    GameObject player, head;
    bool isPositionNotSet = true;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag ( "EXP_ROOM" );
        head = GameObject.FindWithTag ( "MainCamera" );
        //player.transform.Translate ( 0f , -1.5f , 0.25f );
    }
	// Update is called once per frame
	void Update () {
        //if ( isPositionNotSet ) {
            //player.transform.Translate ( head.transform.position.x , head.transform.position.y , head.transform.position.z );
            //player.transform.Translate ( 0f, -1.5f, 0.25f); 
            //isPositionNotSet = false;
        //}
    }
}
