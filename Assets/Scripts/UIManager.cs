using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject finishPrompt;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowFinishPrompt(bool show)
    {
        if (finishPrompt != null)
        {
            finishPrompt.SetActive(show);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
