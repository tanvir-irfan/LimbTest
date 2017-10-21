using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using VRStandardAssets.Utils;

// The intro scene takes users through the basics of interacting through VR in the other scenes.
// This manager controls the steps of the intro scene.
public class IntroController : MonoBehaviour {
	[SerializeField]
	private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
	[SerializeField]
	private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
	[SerializeField]
	private UIFader m_HowToUseFader;                   // This fader controls the UI showing how to use SelectionSliders.
	[SerializeField]
	private SelectionSlider[] m_HowToUseSlider;          // This is the slider that is used to demonstrate how to use them.
	

	private IEnumerator Start ( ) {
		m_Reticle.Show ( );

		m_Radial.Hide ( );

        // In order, fade in the UI on how to use sliders, wait for the slider to be filled then fade out the UI.
        if( m_HowToUseFader != null)
            yield return StartCoroutine ( m_HowToUseFader.InteruptAndFadeIn ( ) );

        for (int i = 0; m_HowToUseSlider != null && i < m_HowToUseSlider.Length; i++ ) {
            //Debug.Log ( "m_HowToUseSlider.Length = " + m_HowToUseSlider.Length );
            yield return StartCoroutine ( m_HowToUseSlider [ i ].WaitForBarToFill ( ) );
        }
		    
        if ( m_HowToUseFader != null )
            yield return StartCoroutine ( m_HowToUseFader.InteruptAndFadeOut ( ) );

	}
}