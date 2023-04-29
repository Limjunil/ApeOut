using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingUIControl : MonoBehaviour
{
    
    private Image loadingBar = default;


    private void Start()
    {
        
        loadingBar = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();

        gameObject.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
    }

    public void LoadUI()
    {        
        StartCoroutine(LoadUIEffect());
    }

    private IEnumerator LoadUIEffect()
    {
        gameObject.transform.localScale = Vector3.one;

        loadingBar.fillAmount = 0f;

        float timer = 0.0f;

        while (true)
        {
            yield return new WaitForSeconds(0.4f);

            if(1f <= timer)
            {

                gameObject.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

                yield break;
            }
            
            timer += 0.25f;
            loadingBar.fillAmount = timer;

        }
    }


}
