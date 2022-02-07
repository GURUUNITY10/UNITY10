using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startCam;
    public GameObject gameCam;

    public PlayerMove player;
    public EnemyFSM enemy;

    public float playTime;
    public float CbestScore;

    public bool isBattle;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject helpPanel;

    // overPanel -> gameOverPanel �ٲ�
    public GameObject gameOverPanel;

    // text �߰�
    public GameObject gameOverText;


    // update �Լ� �ּ� ����, if�� ���� ����
    void Update()
    {
        // �÷��̾� hp ���� �� ���� ����
        if (player.hp <= 0)
        {
            gameOverText.SetActive(true);
            gameOverPanel.SetActive(true);
        }

        if (isBattle)
            playTime += Time.deltaTime;
    }

    public void GameStart()
    {
        startCam.SetActive(false);
        gameCam.SetActive(true);

        startPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

    public void Help()
    {
        startPanel.SetActive(false);
        helpPanel.SetActive(true);

        player.gameObject.SetActive(false);
    }

    public void Back()
    {
        helpPanel.SetActive(false);

        // overPanel -> gameOverPanel �̸� ����
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);

        player.gameObject.SetActive(false);
    }

    /*
    void LateUpdate()
    {
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 60) / 60);
        int sec = (int)(playTime % 60);
        int uns = (int)((min - sec) / 100);

        playTimeTxt.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec)
            + ":" + string.Format("{0:00}", uns);
    }*/
}
