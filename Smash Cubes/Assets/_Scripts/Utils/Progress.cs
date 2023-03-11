using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int BestScore;
}

public class Progress : MonoBehaviour
{

    public PlayerInfo PlayerInfo;
    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    //[SerializeField] TextMeshProUGUI _playerInfoText;
    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerInfo = new PlayerInfo();
            Save();
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
#if !UNITY_EDITOR && UNITY_WEBGL 
        SaveExtern(jsonString);
#endif
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        
    }

}
