using UnityEngine;
using UnityEngine.UI;

using GameManagement.Behaviour;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;

    private void Awake()
    {
        GameState_Loading.OnSceneLoadProgressChanged += SetProgress;
    }

    private void OnDestroy()
    {
        GameState_Loading.OnSceneLoadProgressChanged -= SetProgress;
    }

    public void SetProgress(float progress) => _loadingImage.fillAmount = progress;
}
