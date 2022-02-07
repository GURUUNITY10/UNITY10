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

    // ���� ���� ���
    public enum GameState
    {
        Ready,
        Run,
        Gameover
    }

    // ������ ���� ���� ����
    public GameState gState;

    // ���� ���� UI ������Ʈ ����
    public GameObject gameLabel;

    // ���� ���� UI �ؽ�Ʈ ������Ʈ ����
    Text gameText;

    // PlayerMove Ŭ���� ����
    //public PlayerMove player;

    IEnumerator ReadyToStart()
    {
        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // ���� �ؽ�Ʈ�� ������ 'Go!'
        gameText.text = "Go!";

        // 0.5�ʰ� ���
        yield return new WaitForSeconds(0.5f);

        // ���� �ؽ�Ʈ�� ��Ȱ��ȭ
        gameLabel.SetActive(false);

        // ���¸� '���� ��' ���·� ����
        gState = GameState.Run;
    }

    void Update()
    {
        // �÷��̾� hp ���� �� ���� ����
        if (player.hp <= 0)
        {
            // ���� �ؽ�Ʈ�� Ȱ��ȭ�Ѵ�.
            gameLabel.SetActive(true);

            gameText.text = "Game Over";

            // ���� �ؽ�Ʈ�� ������ ���������� �Ѵ�.
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
        // �ʱ� ���� ���´� �غ� ���·� ����
        gState = GameState.Ready;

        // ���� ���� UI ������Ʈ���� Text ������Ʈ�� ��������
        gameText = gameLabel.GetComponent<Text>();

        // ���� �ؽ�Ʈ�� ������ ��Ready...
        gameText.text = "Ready...";

        // ���� �ؽ�Ʈ�� ������ ��Ȳ������
        gameText.color = new Color32(255, 185, 0, 255);

        // ���� �غ� -> ���� �� ���·� ��ȯ
        StartCoroutine(ReadyToStart());

        // �÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
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
