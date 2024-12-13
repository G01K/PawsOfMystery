using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private string saveFilePath;

    // 냄새킁킁
        // 최종 리셋 날짜
        // 금일 사용힌 횟수
    // 플레이어 상태
        // 수집한 증거 목록
        // 실행한 선택지들(진행상태)
        // NPC와의 관계?

    private void Awake()
    {
        // Singleton 패턴 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 저장 파일 경로 설정
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    public void SaveGame(GameData data)
    {
        // 데이터를 JSON으로 변환하여 저장
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("게임 저장 완료: " + saveFilePath);
    }

    public GameData LoadGame()
    {
        // 저장 파일이 없으면 null 반환
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("저장 파일이 없습니다.");
            return null;
        }

        // 저장된 JSON 파일을 읽어 GameData로 변환
        string json = File.ReadAllText(saveFilePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        Debug.Log("게임 불러오기 완료");
        return data;
    }
}

// 게임 데이터 구조 예제
[System.Serializable]
public class GameData
{
    public int playerLevel;
    public int score;
    public string currentScene;
    public float[] playerPosition; // [x, y, z]
}