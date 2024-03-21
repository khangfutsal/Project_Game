using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdated = true;
    private void Update()
    {
        if (isFirstUpdated)
        {
            LoadSceneManagement.LoadCallBack();
            isFirstUpdated = false;
        }
    }
}
