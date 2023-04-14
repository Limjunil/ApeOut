using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNote : MonoBehaviour
{

    enum State
    {
        Idle, // 대기하다가 최소 감지 범위안에 들어온 적을 본다
        Move, // 시야 범위 안에서 감지된 적을 따라간다
        Engage // 적이 공격 범위 안에 있으면 공격한다
    }

    enum State2 
    {
        Guard,
        Action
    }

    State2 state2;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        if (state2 == State2.Guard)
        {
            Debug.Log("대기 상태");

        }

        else if (state2 == State2.Action)
        {
            Debug.Log("행동 상태");

        }

    }

    void Range() { } // 시야 범위 안 내비메시
    void Guard() { } // 최소거리 안 이라면 감지하여 바라본다
    void Engage() { } // 공격 상태 공격 거리를 벗어나면 이동상태

}
