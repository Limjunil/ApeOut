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


