# ApeOut
Make a ApeOut Game Project

2023/04/10 Init Set, Project Start


## [Junil]
23/04/11 - Make CursorImg and Camera Move    
23/04/11 - Make Player Move and Player Input    
23/04/12 - Fix Camera perspective and Player Mouse Look    
23/04/12 - Fix Camera Move to mouse postion    
23/04/12 - Make Base Map    
23/04/13 - Make Player Attack and Camera Test    
23/04/14 - Make player Grab and Proto TitleScene    
23/04/17 - Make PauseUI and UIManager    
23/04/18 - First Team Build ok and Title Album UI making    
23/04/19 - Add Option UI And Add enemyBullet pooling, Make PlayerHp Bar    
23/04/20 - Make Screen Size Select and FullScreen Option    
23/04/20 - Make UI Move easing and PlayerChkPos Line Renderer    
23/04/21 - Make Camera Shaking    
23/04/21 - Add TMP Text shake effect and proto Album Select    
23/04/24 - Fix Option    
23/04/24 - Make Album Stage    
23/04/25 - Add proto DeadUI    
23/04/25 - Make DeadUI    
23/04/26 - Make Runtime NavBake and DeadUI func    
23/04/27 - Make LoadUI and LoadScene    
23/04/28 - Make Sound and MapManager    
23/04/28 - final proto    
23/04/29 - Make SoundManager and Build proto    
23/04/30 - Make a Final    



## [YMG]
04/11 - 기본세팅 On    
04/11 - Use the new keyword if hiding was intended. 오류    
예약어를 쓰면 나옴    
예) Rigidbody rigidbody 를 rigb로 고쳐봤더니 오류 고침    

04/12    
NullReferenceException: Object reference not set to an instance of an object - 오류    
해결    
component를 정의하지 않음    
애니메이터 변수 선언만 해놓고 component를 가져오지 않아서 생김    
GetComponent<Animator>() 를 해주니 해결    
시야 범위 Gizmos 비주얼    

04/13    
OnDrawGizmos() 오류
Gizmos.DrawRay(transform.position, (Target.position - transform.position).normalized * maxRadius);    
Target을 태그를 찾는 형식으로 했는데 플레이 전에 NullReferenceException 오류 나옴    
해결    
if (Target != null)    
    
04/14    
일반형 기초 완성  

04/17    
파티클 시스템 종료되지 않고 반복되는 오류    
폭발하는 파티클 시스템 오브젝트를 사용하고 종료할 때    
이펙트가 다 안꺼지고 반복되는 현상을 목격했다.    

해결)    
해결방안을 찾아본 결과 Stop Action 기능이 제대로 작동하지 않았다.    
Stop Action은 파티클 시스템이 정지한 후의 행동을 설정하는데 이는 파티클 시스템이 현재 정지되지 않았음을 나타낸다.    
정보를 찾아보니 이펙트 오브젝트가 여러개인 경우 모두 루프를 종료해야 Stop Action이 제대로 작동된다.    
    
04/18    
몬스터 일반형 상속형 완성    

04/19    
패트롤 기능 추가    
맵 제작 시작    
    
04/24

04/25    
    몬스터 무차별 파괴 현상    
    원작에 따라서 몬스터를 밀쳐 벽에 부딯치면 파괴되는 기능을 OnCollisionEnter(Collision collision)으로    
    구현해놓았었는데 몬스터가 플레이어에게 이동하는 도중 벽에 부딪치면 똑같이 파괴되는 현상을 확인.    
        
    해결 1)    
    bool 변수 하나를 만들어서 몬스터의 리지드바디를 포함하는 함수에 넣는다.    
    플레이어가 공격 할 때에만 true가 된 상태에서 부딪칠 때만 파괴되게 하여 해결했다.        
    

04/26    
    몬스터 밀림 현상    
    플레이어와 몬스터가 충돌할 시 밀림 현상으로 인해 Navmesh 정상작동 안함    
    플레이어가 몬스터를 미는 방식은 AddForce 방식인데 이 힘이 끝나지 않고 계속 이어지는 것을 알아냄.    
        
    해결 1)    
    몬스터 부모 클래스에 리지드바디.velocity = Vector3.zero; 가 1초 뒤에 실행하는 코루틴과 이를 포함하는 함수를 만든 뒤    
    플레이어가 공격 할 때 호출하여 몬스터가 받는 물리현상을 구현했다.    
    덤으로 마찰력을 만들어주는 Physic Material 과 무게를 나타내는 NavMesh의 Mass 도 사용해봤다.    
        
04/27    
    몬스터 밀려나는 오류    
    몬스터와 플레이어가 부딪치면 밀려나는 현상이 제대로 해결이 안됨    
        
    해결 1)    
    벽에 부딪치면 죽게만든 기능인 void OnCollisionEnter(Collision collision) 에 새로운 기능 추가    
    if (collision.collider.CompareTag("Player") 
        {
            Debug.Log("Player");
            enemyRigid.velocity = Vector3.zero;
            enemyRigid.isKinematic = true;
            enemyRigid.isKinematic = false;
        }    
    플레이어의 콜라이더가 부딪치는 동안은 isKinematic을 발동시켜 밀려나지 않게하고 움직임도 이상 없음.    
        
    문제 2)    
    라인 렌더러 표현이 제대로 안됨    
        
    해결1)    
    Alignment transform z -> view    
    Use World Space 체크    
    

04/28
    final proto
    
    
    
    
    
