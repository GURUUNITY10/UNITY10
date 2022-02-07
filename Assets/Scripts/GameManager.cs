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
    public GameObject menuPanel;
    
    public Text CbestScoreTxt;
    public Text playTimeTxt;

    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // 게임 상태 상수
    public enum GameState
    {
        Ready,
        Run,
        Gameover
    }

    // 현재의 게임 상태 변수
    public GameState gState;

    // 게임 상태 UI 오브젝트 변수
    public GameObject gameLabel;

    // 게임 상태 UI 텍스트 컴포넌트 변수
    Text gameText;

    // PlayerMove 클래스 변수
    //public PlayerMove player;

    IEnumerator ReadyToStart()
    {
        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 상태 텍스트의 내용을 'Go!'
        gameText.text = "Go!";

        // 0.5초간 대기
        yield return new WaitForSeconds(0.5f);

        // 상태 텍스트를 비활성화
        gameLabel.SetActive(false);

        // 상태를 '게임 중' 상태로 변경
        gState = GameState.Run;
    }

    void Update()
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
    }

    public void GameStart()
    {
        startCam.SetActive(false);
        gameCam.SetActive(true);

        startPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

    public void WaitStart()
    {
        // 초기 게임 상태는 준비 상태로 설정
        gState = GameState.Ready;

        // 게임 상태 UI 오브젝트에서 Text 컴포넌트를 가져오기
        gameText = gameLabel.GetComponent<Text>();

        // 상태 텍스트의 내용을 ‘Ready...
        gameText.text = "Ready...";

        // 상태 텍스트의 색상을 주황색으로
        gameText.color = new Color32(255, 185, 0, 255);

        // 게임 준비 -> 게임 중 상태로 전환
        StartCoroutine(ReadyToStart());

        // 플레이어 오브젝트를 찾은 후 플레이어의 PlayerMove 컴포넌트 받아오기
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
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
