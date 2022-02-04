using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 발사 위치
    public GameObject firePosition;
    // 투척 무기 오브젝트
    //public GameObject bombFactory;
    // 피격 이펙트 오브젝트
    public GameObject bulletEffect;
    // 피격 이펙트 파티클 시스템
    ParticleSystem ps;
    // 발사 무기 공격력
    public int weaponPower = 5;
    // 애니메이터 변수
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
        // 애니메이터 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 ‘게임 중’ 상태일 때만 조작할 수 있게 한다.
        //<게임매니저있을때>if (GameManager.gm.gState != GameManager.GameState.Run)
        // {
        //   return;
        //}

        // 마우스 왼쪽 버튼을 입력받는다.
        if (Input.GetMouseButtonDown(0))
        {
            // 만일 이동 블랜드 트리 파라미터의 값이 0이라면, 공격 애니메이션을 실시한다.
            //if (anim.GetFloat("MoveMotion") == 0)
            //{
              //  anim.SetTrigger("Attack");
            //}

            // 레이를 생성한 후 발사될 위치와 진행 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 레이가 부딪힌 대상의 정보를 저장할 변수를 생성한다.
            RaycastHit hitInfo = new RaycastHit();

            // 레이를 발사한 후 만일 부딪힌 물체가 있으면 피격 이펙트를 표시한다.
            // 만일 레이에 부딪힌 대상의 레이어가 ‘Enemy’라면 데미지 함수를 실행한다.
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //<애너미 다운받은 후 실행>  EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                //<애너미 다운받은 후 실행>  eFSM.HitEnemy(weaponPower);
            }
            // 그렇지 않다면, 레이에 부딪힌 지점에 피격 이펙트를 플레이한다.
            else
            {
                // 피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동시킨다.
                bulletEffect.transform.position = hitInfo.point;
                // 피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                bulletEffect.transform.forward = hitInfo.normal;
                // 피격 이펙트를 플레이한다.
                ps.Play();
            }
        }
    }
}

