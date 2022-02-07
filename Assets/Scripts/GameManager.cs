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
    public GameObject overPanel;
    

/*    void Update()
    {
        // 플레이어 hp 없을 시 게임 종료
        if (player.hp <= 0)
        {
            // 상태 텍스트를 활성화한다.
            gameLabel.SetActive(true);

            gameText.text = "Game Over";

            // 상태 텍스트의 색상을 붉은색으로 한다.
            gameText.color = new Color32(225, 0, 0, 225);

            gState = GameState.Gameover;
        }

        if (isBattle)
            playTime += Time.deltaTime;
    }*/

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
        overPanel.SetActive(false);
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
