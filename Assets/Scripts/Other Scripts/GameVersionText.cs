using UnityEngine;
using UnityEngine.UI;

public class GameVersionText : MonoBehaviour
{
    [SerializeField]
    private string prefixVersion = "v ";

    private Text txtGameVersion;

    void Start()
    {
        txtGameVersion = GetComponent<Text>();
        WriteGameVersion();
    }

    void WriteGameVersion()
    {
        if (!txtGameVersion)
            return;

        txtGameVersion.text = TextVersion();
    }

    string TextVersion()
    {
        return prefixVersion + Application.version;
    }
}