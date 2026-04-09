using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
	public void hitplay()
	{
		SceneManager.LoadScene("DwayneTheRockJohnson");
	}

}
public class EndButton: MonoBehavior
{
	// This method will be called when button is clicked 
	public void End()
	{
		#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running as a built application
        Application.End();
#endif
	}
}