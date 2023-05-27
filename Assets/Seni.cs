using UnityEngine;
using UnityEngine.SceneManagement;

public class Seni : MonoBehaviour
{

	public void OnClickStartButton()
	{
		SceneManager.LoadScene("MainScene");
	}
}