using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider loadSlider;
    private float _target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _loaderCanvas.SetActive(false);
        }
        else{
            Destroy(gameObject);
        }
    }

    public IEnumerator LoadScene(string sceneName)
{
    var scene = SceneManager.LoadSceneAsync(sceneName);
    scene.allowSceneActivation = false;

    _loaderCanvas.SetActive(true);

    while (scene.progress < 0.9f)
    {
        float targetValue = scene.progress / 0.9f;

        // Smoothly move slider toward target
        loadSlider.value = Mathf.MoveTowards(loadSlider.value, targetValue, Time.deltaTime * 0.3f);

        yield return null; // 🔥 REQUIRED
    }

    // Fill the rest slowly to 1
    while (loadSlider.value < 1f)
    {
        loadSlider.value = Mathf.MoveTowards(loadSlider.value, 1f, Time.deltaTime * 0.3f);
        yield return null;
    }

    yield return new WaitForSeconds(0.5f);

    scene.allowSceneActivation = true;
    _loaderCanvas.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        //loadSlider.fillAmount = Mathf.MoveTowards(loadSlider.fillAmount, _target, 3*Time.deltaTime);
    }
}
