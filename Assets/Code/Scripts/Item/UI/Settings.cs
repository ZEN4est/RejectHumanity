using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _resume, _exit;

    [Inject] private SceneLoader _sceneLoader;
    [Inject] private UiService _uiService;

    private void Start()
    {
        _resume.onClick.AddListener(ShowHideScene);
        _exit.onClick.AddListener(Exit);
        if (_panel == null)
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetMusicVolume();
            }
        SetMusicVolume();

        _uiService.Change += OnUiChange;

    }


    private void OnUiChange(UiType type)
    {
        if (type == UiType.Settings)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Exit()
    {
        _sceneLoader.LoadMainMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideScene();
        }
    }

    private void ShowHideScene()
    {
        _uiService.SetUi(!_panel.activeInHierarchy ? UiType.Settings : UiType.Player);

        _panel.SetActive(!_panel.activeInHierarchy);
    }

    public void SetMusicVolume()
    {
        if (musicSlider == null) return;

        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    private void OnDestroy()
    {
        _resume.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
        _uiService.Change -= OnUiChange;

    }
}
