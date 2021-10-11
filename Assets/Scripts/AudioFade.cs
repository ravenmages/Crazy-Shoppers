using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioFade: MonoBehaviour
{

    [SerializeField] float delay;
    [SerializeField] float maxVol;

    void Start() {

        StartCoroutine(fadeIn(delay, maxVol));
    }


    public static IEnumerator fadeIn(float delay, float maxVol) {

        float elapsedTime = 0;
        float currentVolume = 0;

        AudioListener.volume = 0;

        while (elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, maxVol, elapsedTime / delay);
            yield return null;
        }

    }

}
