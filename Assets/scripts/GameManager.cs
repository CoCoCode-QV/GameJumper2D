using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player PlayerPrefab;
    public PlatformID PlatformPrefab;

    public float minSpawX;
    public float maxSpawX;
    public float minSpawY;
    public float maxSpawY;

    public CameraController mainCam;

    Player m_player;
    int m_Score;

    public float PowerBarUp;
    bool m_isGameStarted;

    public bool IsGameStarted { get => m_isGameStarted; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
        GameGuiManager.Ins.UpdateScoreCounting(m_Score);

        GameGuiManager.Ins.UpdatePowerBar(0, 1);
        AudioController.Ins.PlayBackgroundMusic();
    }
    public void PlayerGame()
    {
        StartCoroutine(PlatFormInit());
        GameGuiManager.Ins.showGameGui(true);
    }
    IEnumerator PlatFormInit()
    {
        PlatformID PlatformClone = null;
        if (PlatformPrefab)
        {
            PlatformClone = Instantiate(PlatformPrefab, new Vector2(0, Random.Range(minSpawY, maxSpawY)), Quaternion.identity);
            PlatformClone.ID = PlatformClone.gameObject.GetInstanceID();
        }
        yield return new WaitForSeconds(0.5f);
        if (PlayerPrefab)
        {
            m_player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            m_player.lastPlatFormID = PlatformClone.ID;
        }
        if (PlatformPrefab)
        {
            float spawnX = m_player.transform.position.x + minSpawX;
            float SpawnY = Random.Range(minSpawY, maxSpawY);

            PlatformID PlatformClone1 = Instantiate(PlatformPrefab, new Vector2(spawnX, SpawnY), Quaternion.identity);
            PlatformClone1.ID = PlatformClone1.gameObject.GetInstanceID();
        }
        yield return new WaitForSeconds(0.5f);
        m_isGameStarted = true;
    }

    public void CreatePlatform()
    {
        if (!PlatformPrefab || !m_player)
            return;
        float spawnX = Random.Range(m_player.transform.position.x + minSpawX, m_player.transform.position.x + maxSpawX);
        float spawnY = Random.Range(minSpawY, maxSpawY);
        PlatformID PlatformClone = Instantiate(PlatformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        PlatformClone.ID = PlatformClone.gameObject.GetInstanceID();
    }
    public void CreatePlatformAndLerp(float playerPosX)
    {
        if (mainCam)
        {
            mainCam.LearpTrigger(playerPosX + minSpawX);
        }
        CreatePlatform();

    }
    public void AddScore()
    {
        m_Score++;
        GameGuiManager.Ins.UpdateScoreCounting(m_Score);
        SaveData.BestScore = m_Score;
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);

     }
}
