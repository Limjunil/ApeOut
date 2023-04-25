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

