using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public void LoadSceneName(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
