using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // ���ʹ� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    
    // ���ʹ� ���� ����
    EnemyState m_State;

    // �÷��̾� �߰� ����
    public float findDistance = 8f;
    // �÷��̾� Ʈ������
    Transform player;

    // ���� ���� ����
    public float attackDistance = 2f;
    // �̵� �ӵ�
    public float moveSpeed = 5f;
    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;

    // ���� �ð�
    float currentTime = 0;
    // ���� ����� �ð�
    float attackDelay = 2f;

    // ���ʹ� ���ݷ�
    public int attackPower = 3;

    // �̵� ���� ����
    public float moveDistance = 20f;
    // ���ʹ��� ü��
    public int hp = 15;
    // ���ʹ��� �ִ� ü��
    int maxHp = 15;
    // ���ʹ� hp Slider ����
    public Slider hpSlider;

    // �ִϸ����� ����
    Animator anim;

    // ������̼� ������Ʈ ����
    UnityEngine.AI.NavMeshAgent smith;

    // ����
    public AudioClip audioMove;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioDie;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // ������ ���ʹ� ���´� ���� �Ѵ�.
        m_State = EnemyState.Idle;
        // �÷��̾��� Ʈ������ ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").transform;

        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ڽ� ������Ʈ�κ��� �ִϸ����� ���� �޾ƿ���
        anim = transform.GetComponentInChildren<Animator>();

        // ����
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
        // ���� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        // ����, �÷��̾���� �Ÿ��� �׼� ���� ���� �̳���� Move ���·� ��ȯ�Ѵ�.
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Idle -> Move");

            // �̵� �ִϸ��̼����� ��ȯ�ϱ�
            anim.SetTrigger("IdleToMove");
            PlaySound("Move");
        }

    }
    void Move()
    {
        // ����, �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵��Ѵ�.
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // �̵� ���� ����
            Vector3 dir = (player.position - transform.position).normalized;
            // ĳ���� ��Ʈ�ѷ��� �̿��� �̵��ϱ�
            cc.Move(dir * moveSpeed * Time.deltaTime);
            // �÷��̾ ���� ������ ��ȯ�Ѵ�.
            transform.forward = dir;
        }
        // �׷��� �ʴٸ� ���� ���¸� �������� ��ȯ�Ѵ�.
        else
        {
            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");
            // ���� �ð��� ���� ����� �ð���ŭ �̸� ������� ���´�.
            currentTime = attackDelay;
            // ���� ��� �ִϸ��̼� �÷���
            anim.SetTrigger("MoveToAttackDelay");
        }
    }
    void Attack()
    {
        // ����, �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ �����Ѵ�.
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ������ �ð����� �÷��̾ �����Ѵ�.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;

                // ���� �ִϸ��̼� �÷���
                anim.SetTrigger("StartAttack");
                PlaySound("Attack");
            }
        }
        // �׷��� �ʴٸ�, ���� ���¸� �̵�(Move)���� ��ȯ�Ѵ�(���߰� �ǽ�).
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
            currentTime = 0;

            // �̵� �ִϸ��̼� �÷���
            anim.SetTrigger("AttackToMove");
            PlaySound("Move");
        }
    }
    // �÷��̾��� ��ũ��Ʈ�� ������ ó�� �Լ��� �����ϱ�
    public void AttackAction()
    {
        player.GetComponent<PlayerMove>().DamageAction(attackPower);
    }
    void Damaged()
    {
        // �ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DamageProcess());
    }
    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(1.0f);
        // ���� ���¸� �̵� ���·� ��ȯ�Ѵ�.
        m_State = EnemyState.Move;
        print("���� ��ȯ: Damaged -> Move");
    }
    // ������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        // �÷��̾��� ���ݷ¸�ŭ ���ʹ��� ü���� ���ҽ�Ų��.
        hp -= hitPower;
        // ���ʹ��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ�Ѵ�.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ: Any state -> Damaged");

            // �ǰ� �ִϸ��̼��� �÷����Ѵ�.
            anim.SetTrigger("Damaged");
            Damaged();
            PlaySound("Damaged");
        }
        // �׷��� �ʴٸ� ���� ���·� ��ȯ�Ѵ�.
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ: Any state -> Die");

            // ���� �ִϸ��̼��� �÷����Ѵ�.
            anim.SetTrigger("Die");
            Die();
            PlaySound("Die");
        }
    }
    // ���� ���� �Լ�
    void Die()
    {
        // ���� ���� �ǰ� �ڷ�ƾ�� �����Ѵ�.
        StopAllCoroutines();
        // ���� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� ��Ȱ��ȭ��Ų��.
        cc.enabled = false;
        // 2�� ���� ��ٸ� �Ŀ� �ڱ� �ڽ��� �����Ѵ�.
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Move":
                audioSource.clip = audioMove;
                break;
            case "Attack":
                audioSource.clip = audioAttack;
                break;
            case "Damaged":
                audioSource.clip = audioDamaged;
                break;
            case "Die":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }
}
