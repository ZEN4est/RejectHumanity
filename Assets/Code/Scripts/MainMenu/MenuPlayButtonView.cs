using UnityEngine;
using Zenject;

public class MenuPlayButtonView : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;

    public void Click()
    {
        _sceneLoader.LoadGame();
    }

}