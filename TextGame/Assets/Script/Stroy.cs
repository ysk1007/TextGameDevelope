using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stroy : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject question_obj;
    public string input;

    private void Start()
    {
        input = Text.text;

        question_obj.SetActive(false);  // ������ ������ �ʰ� ����
        StartCoroutine(OnTyping());     // Ÿ���� ����Ʈ ����
    }

    IEnumerator OnTyping()
    {
        string output;
        string mainstry = Text.text;        // mainstry�� ����Ƽ���� ������ Text������ ����
        Text.text = "";                     // ����Ƽ���� ������ Text �ʱ�ȭ

        yield return new WaitForSeconds(0.5f);      // 0.5f ���� ���� ������

        for(int i = 0; i <= mainstry.Length; i++)   // mainstry�� ���� ��ŭ �ݺ�
        {
            output = mainstry.Substring(0, i);      // mainstry�ȿ� ����� ������ ó������ i��°���� ���� output�� ����
            Text.text = output;                     // ����Ƽ�� Text = output
            yield return new WaitForSeconds(0.07f); // 0.07f�� ������ �ΰ� ���ڰ� ��Ÿ��
        }

        question_obj.SetActive(true);               // ������ ���̰� ����
    }
    
    public void Skip()  // Skip ���
    {
        StopAllCoroutines();    // �ڷ�ƾ ��ü ����

        Text.text = input;      

        question_obj.SetActive(true);   // ������ ���̰� ����
    }

}