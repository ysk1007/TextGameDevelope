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
                Debug.Log("������ ����");
                break;

            case "Busan_btn":
                Debug.Log("�λ��� ����");
                break;

            case "Jejudo_btn":
                Debug.Log("���ֵ��� ����");
                break;
        }
    }
}
