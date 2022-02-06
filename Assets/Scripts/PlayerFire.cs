using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // �߻� ��ġ
    public GameObject firePosition;
    // ��ô ���� ������Ʈ
    //public GameObject bombFactory;
    // �ǰ� ����Ʈ ������Ʈ
    public GameObject bulletEffect;
    // �ǰ� ����Ʈ ��ƼŬ �ý���
    ParticleSystem ps;
    // �߻� ���� ���ݷ�
    public int weaponPower = 5;
    // �ִϸ����� ����
    Animator anim;
    // �� �߻� ȿ�� ������Ʈ �迭
    public GameObject[] eff_Flash;

    // Start is called before the first frame update
    void Start()
    {
        // �ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();
        // �ִϸ����� ������Ʈ ��������
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ������ �ߡ� ������ ���� ������ �� �ְ� �Ѵ�.
        //<���ӸŴ���������>if (GameManager.gm.gState != GameManager.GameState.Run)
        // {
        //   return;
        //}

        // ���콺 ���� ��ư�� �Է¹޴´�.
        if (Input.GetMouseButtonDown(0))
        {
            // ���� �̵� ���� Ʈ�� �Ķ������ ���� 0�̶��, ���� �ִϸ��̼��� �ǽ��Ѵ�.
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }

            // ���̸� ������ �� �߻�� ��ġ�� ���� ������ �����Ѵ�.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // ���̰� �ε��� ����� ������ ������ ������ �����Ѵ�.
            RaycastHit hitInfo = new RaycastHit();

            // ���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ�� ǥ���Ѵ�.
            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���� ���̿� �ε��� ����� ���̾ ��Enemy����� ������ �Լ��� �����Ѵ�.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                // �׷��� �ʴٸ�, ���̿� �ε��� ������ �ǰ� ����Ʈ�� �÷����Ѵ�.
                else
                {
                    // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵���Ų��.
                    bulletEffect.transform.position = hitInfo.point;
                    // �ǰ� ����Ʈ�� forward ������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                    bulletEffect.transform.forward = hitInfo.normal;
                    // �ǰ� ����Ʈ�� �÷����Ѵ�.
                    ps.Play();
                }
            }
            // �� ����Ʈ�� �ǽ��Ѵ�.
            StartCoroutine(ShootEffectOn(0.05f));
        }
    }
    // �ѱ� ����Ʈ �ڷ�ƾ �Լ�
    IEnumerator ShootEffectOn(float duration)
    {
        // �����ϰ� ���ڸ� �̴´�.
        int num = Random.Range(0, eff_Flash.Length - 1);
        // ����Ʈ ������Ʈ �迭���� ���� ���ڿ� �ش��ϴ� ����Ʈ ������Ʈ�� Ȱ��ȭ�Ѵ�.
        eff_Flash[num].SetActive(true);
        // ������ �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(duration);
        // ����Ʈ ������Ʈ�� �ٽ� ��Ȱ��ȭ�Ѵ�.
        eff_Flash[num].SetActive(false);
    }
}

