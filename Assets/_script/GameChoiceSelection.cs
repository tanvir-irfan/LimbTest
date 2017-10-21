using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

// This script is a simple example of how an interactive item can
// be used to change things on gameobjects by handling events.
public class GameChoiceSelection : MonoBehaviour {
    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;
    [SerializeField]
    private Material m_ClickedMaterial;
    [SerializeField]
    private Material m_DoubleClickedMaterial;
    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;
    [SerializeField]
    private Renderer m_Renderer;

    public GameObject gameManager;
    
    private void Awake ( ) {
        m_Renderer.material = m_NormalMaterial;
    }

    // Use this for initialization
    void Start ( ) {
        
    }

    private void OnEnable ( ) {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
    }


    private void OnDisable ( ) {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
    }


    //Handle the Over event
    private void HandleOver ( ) {
        Debug.Log ( "Show over state" );
        m_Renderer.material = m_OverMaterial;
    }


    //Handle the Out event
    private void HandleOut ( ) {
        Debug.Log ( "Show out state" );
        m_Renderer.material = m_NormalMaterial;
    }


    //Handle the Click event
    private void HandleClick ( ) {
        Debug.Log ( "Show click state" );
        m_Renderer.material = m_ClickedMaterial;

        gameManager.GetComponent<GameController> ( ).choiceSelected ( this.gameObject.tag );
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick ( ) {        
        HandleClick ( );
    }
}