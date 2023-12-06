using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int targetSceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
