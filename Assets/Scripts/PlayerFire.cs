using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //if (anim.GetFloat("MoveMotion") == 0)
            //{
              //  anim.SetTrigger("Attack");
            //}

            // ���̸� ������ �� �߻�� ��ġ�� ���� ������ �����Ѵ�.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // ���̰� �ε��� ����� ������ ������ ������ �����Ѵ�.
            RaycastHit hitInfo = new RaycastHit();

            // ���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ�� ǥ���Ѵ�.
            // ���� ���̿� �ε��� ����� ���̾ ��Enemy����� ������ �Լ��� �����Ѵ�.
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //<�ֳʹ� �ٿ���� �� ����>  EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                //<�ֳʹ� �ٿ���� �� ����>  eFSM.HitEnemy(weaponPower);
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
    }
}

