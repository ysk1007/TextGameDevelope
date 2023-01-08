using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 제어 참조 선언

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()    //씬 전환 함수
    {
        switch (this.gameObject.name)   //switch case 문으로 gameObject.name 별로 상황에 따른 이벤트
        {
            case "Story_btn":
                Debug.Log("스토리 모드를 선택");
                break;

            case "Continue_btn":
                Debug.Log("이어하기를 선택");
                break;

            case "Infinite_btn":
                Debug.Log("무한모드를 선택");
                break;

            case "Collection_btn":
                Debug.Log("컬렉션을 선택");
                break;

        }
    }

}
