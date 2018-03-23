using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class HandCamera : MonoBehaviour {

    public Image blackPixel;
    public string loadLevel;


	IEnumerator Start () {

        blackPixel.canvasRenderer.SetAlpha(1.0f);

        CameraShaker.Instance.ShakeOnce(4f, 1f, 10f, 10f);

        FadeOut();
        yield return new WaitForSeconds(5.0f);

        FadeIn();
        yield return new WaitForSeconds(7.0f);

        SceneManager.LoadScene(loadLevel);
    }

    void FadeIn()
    {
        blackPixel.CrossFadeAlpha(1.0f, 5.0f, false);
    }

    void FadeOut()
    {
        blackPixel.CrossFadeAlpha(0.0f, 15.0f, false);
    }
}
