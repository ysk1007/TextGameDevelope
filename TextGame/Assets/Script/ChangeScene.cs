using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ���� ���� ����

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()    //�� ��ȯ �Լ�
    {
        switch (this.gameObject.name)   //switch case ������ gameObject.name ���� ��Ȳ�� ���� �̺�Ʈ
        {
            case "Story_btn":
                Debug.Log("���丮 ��带 ����");
                break;

            case "Continue_btn":
                Debug.Log("�̾��ϱ⸦ ����");
                break;

            case "Infinite_btn":
                Debug.Log("���Ѹ�带 ����");
                break;

            case "Collection_btn":
                Debug.Log("�÷����� ����");
                break;

        }
    }

}
