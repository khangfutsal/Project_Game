using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public static class LoadSceneManagement
{
    public class LoadMonoBehaviour : MonoBehaviour { }
    public enum Scene
    {
        Chap1,
        Chap2,
        Chap3,
        LoadingScene,
        MainMenu
    }

    private static Action OnLoad;
    public static AsyncOperation loadAsyncOperation;

    public static void LoadScene(string scene)
    {
        OnLoad = () =>
        {
            GameObject loadObj = new GameObject("Load Object");
            loadObj.AddComponent<LoadMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));

            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }
    private static IEnumerator LoadSceneAsync(string scene)
    {
        yield return null;
        loadAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while (!loadAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadAsyncOperation != null)
        {
            return loadAsyncOperation.progress;
        }
        return 1f;
    }
    public static void LoadCallBack()
    {
        if (OnLoad != null)
        {
            OnLoad();
            OnLoad = null;
        }
    }
}
