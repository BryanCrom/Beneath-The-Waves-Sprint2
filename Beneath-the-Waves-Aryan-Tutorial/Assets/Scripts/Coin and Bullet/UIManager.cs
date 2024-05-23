using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCount(int value, TextMeshProUGUI textUI, string prefix)
    {
        if (textUI != null)
        {
            textUI.text = prefix + value;
        }
        else
        {
            Debug.LogError("Text component is null!");
        }
    }

    public static void SafeUpdateCount(int value, TextMeshProUGUI textUI, string prefix)
    {
        if (Instance != null && textUI != null)
        {
            Instance.UpdateCount(value, textUI, prefix);
        }
        else
        {
            Debug.LogError("UIManager instance or text component is null!");
        }
    }
}
