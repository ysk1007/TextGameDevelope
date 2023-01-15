using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUi : MonoBehaviour, IBeginDragHandler ,IDragHandler, IEndDragHandler
{
    public GameManager gameManager;
    private Transform canvas;           //Ui�� �ҼӵǾ� �ִ� �ֻ���� canvas Transform
    public Transform previousParent;   //�ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transform
    private RectTransform rect;         //Ui ��ġ ��� ���� RectTransform
    private CanvasGroup canvasGroup;    //Ui�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //���� ������Ʈ�� �巡���ϱ� ������ �� 1ȸ ȣ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
        previousParent = transform.parent;

        //���� �巡������ ui�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
        transform.SetParent(canvas);    //�θ� ������Ʈ��  canvas�� ����
        transform.SetAsLastSibling();   //���� �տ� ���̵��� ������ �ڽ����� ����


        //�巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� ���� �ֱ� ������ CanvasGroup���� ����
        //���İ��� 0.6���� �����ϰ� ���� �浹 ó���� ���� �ʵ��� �Ѵ�
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        //���� ��ũ������ ���콺 ��ġ�� Ui ��ġ�� ���� (Ui�� ���콺�� �Ѿƴٴϴ� ����)
        rect.position = eventData.position;
    }

    //���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        //�巡�׸� �����ϸ� �θ� canvas�� �����Ǳ� ������
        //�巡�׸� ������ �� �θ� canvas�̸� ������ ������ �ƴ� ������ ����
        //����� �ߴٴ� ���̱� ������ �巡�� ������ �ҼӵǾ� �ִ� ������ �������� �̵�
        if ( transform.parent == canvas  )
        {
            //�������� �ҼӵǾ� �־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        else
        {
            string Ui = previousParent.GetComponent<DropUi>().uitype.ToString();
            string current_sloat = previousParent.GetComponent<RectTransform>().gameObject.name;
            string sloat_name = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().GetComponentInParent<DropUi>().name;
            int item_str = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_str;
            int item_dex = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_dex;
            int item_agi = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_agi;
            int item_luk = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_luk;
            string type = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_type.ToString();

            if (type == Ui)
            {
                gameManager.item_dequip(item_str, item_dex, item_agi, item_luk);
            }
        }


        //���İ��� 1�� �����ϰ� ���� �浹 ó���� ���� �ʵ��� �Ѵ�
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void posreset()
    {
        //�������� �ҼӵǾ� �־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
        transform.SetParent(previousParent);
        rect.position = previousParent.GetComponent<RectTransform>().position;
    }
}
