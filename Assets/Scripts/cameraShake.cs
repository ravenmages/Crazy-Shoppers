using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    [SerializeField] private float interval;

    [SerializeField] private AudioSource soundEffect;

    private ActionTimer timer;
    private Coroutine oldRoutine;

    private void Start() {
        timer = new ActionTimer(interval, startShake, true);
    }

    private void Update() {
        timer.countDown();
    }

    private void startShake() {

        if (oldRoutine != null) {
            soundEffect.Stop();
            StopCoroutine(oldRoutine);
        }

        soundEffect.Play();
        oldRoutine = StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = originalPos;

    }


}
