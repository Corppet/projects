using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    [SerializeField] private float displayDuration;

    [Space]

    [SerializeField] private Image imageRenderer;
    [SerializeField] private TMP_Text loadingText;

    private AsyncOperation load;

    private IEnumerator ChangeImage()
    {
        imageRenderer.sprite = images[Random.Range(0, images.Count)];

        yield return new WaitForSeconds(displayDuration);

        StartCoroutine(ChangeImage());
    }

    private IEnumerator LoadMain()
    {

        yield return null;
    }

    private void Start()
    {
        load = SceneManager.LoadSceneAsync("Main Scene");

        StartCoroutine(ChangeImage());
    }

    private void Update()
    {
        int progress = (int)(Mathf.Clamp01(load.progress / .9f) * 100f);
        loadingText.text = progress + "%";
    }
}
