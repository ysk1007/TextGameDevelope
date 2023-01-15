using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public void ChoiceOne()
    {
        switch (this.gameObject.name)
        {
            case "Seoul_btn":
                Debug.Log("서울을 선택");
                break;

            case "Busan_btn":
                Debug.Log("부산을 선택");
                break;

            case "Jejudo_btn":
                Debug.Log("제주도를 선택");
                break;
        }
    }
}
