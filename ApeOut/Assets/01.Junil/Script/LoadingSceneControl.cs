using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : MonoBehaviour
{
    static string nextScene;

    public Image loadingbarImg = default;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");
        GameObject loadingBar_ = gameUIObj_.transform.GetChild(2).gameObject;
        loadingbarImg = loadingBar_.transform.GetChild(1).gameObject.GetComponent<Image>();

        StartCoroutine(LoadSceneBar());
    }

    public static void LoadSceneScene(string sceneName_)
    {
        nextScene = sceneName_;
                
        GFunc.LoadScene(RDefine.LOADING_SCENE);
    }

    IEnumerator LoadSceneBar()
    {
        
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;


            if (op.progress < 0.9f)
            {
                loadingbarImg.fillAmount = Mathf.Lerp(loadingbarImg.fillAmount, op.progress, timer);
                if (loadingbarImg.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                loadingbarImg.fillAmount = Mathf.Lerp(loadingbarImg.fillAmount, 1f, timer);
                if (loadingbarImg.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
