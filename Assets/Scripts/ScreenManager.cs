﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [Header("Transition")]
    [SerializeField]
    private Animator m_FaderAnimator;

    [Header("Loading")]
    [SerializeField]
    private GameObject m_LoadingPanel;

    [SerializeField]
    private float m_DelayAfterLoading = 2.0f;

    public void LoadLevel(string nextSceneName)
    {
        StartCoroutine(ChangeScene(nextSceneName, false));
    }

    public void LoadLevelLoading(string nextSceneName)
    {
        StartCoroutine(ChangeScene(nextSceneName, true));
    }

    public IEnumerator ChangeScene(string nextSceneName, bool loading)
    {
        m_FaderAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(m_FaderAnimator.GetCurrentAnimatorStateInfo(0).length);

        if (nextSceneName.Equals("Quit"))
        {
            // Helpers.Quit();
        }
        else
        {
            if (loading)
            {
                m_LoadingPanel.SetActive(true);

                m_FaderAnimator.SetTrigger("Open");
                yield return new WaitForSeconds(m_FaderAnimator.GetCurrentAnimatorStateInfo(0).length);
            }

            AsyncOperation asyncScene = SceneManager.LoadSceneAsync(nextSceneName);
            asyncScene.allowSceneActivation = false;

            while (!asyncScene.isDone)
            {
                if (asyncScene.progress >= 0.9f)
                {
                    if (loading)
                    {
                        yield return new WaitForSeconds(m_DelayAfterLoading);

                        m_FaderAnimator.SetTrigger("Close");
                        yield return new WaitForSeconds(m_FaderAnimator.GetCurrentAnimatorStateInfo(0).length);

                        m_LoadingPanel.SetActive(false);
                    }

                    asyncScene.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}