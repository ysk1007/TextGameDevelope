using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropUi : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public enum DropUiType{ inven, weapon, cloths, ring, hat }
    public DropUiType uitype;
    private Image image;
    private RectTransform rect;
    public GameManager gameManager;

    private void Awake()
    {
        
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    //���콺 �����Ͱ� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        //������ ������ ���� ����
        image.color = Color.yellow;
    }



    //���콺 �����Ͱ� ���� ������ ���� ������ ���� ���� �� 1ȸ ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        //������ ������ ���� ����
        image.color = Color.white;
    }   
    
    //���� ������ ���� ���� ���ο��� ��� ���� �� 1ȸ ȣ��
    public void OnDrop(PointerEventData eventData)
    {
        //pointerDrag�� ���� �巡�� �ϰ��ִ� ���(=������)
        if ( eventData != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            string past_sloat = eventData.pointerDrag.GetComponent<DragUi>().previousParent.GetComponent<RectTransform>().gameObject.name;
            eventData.pointerDrag.GetComponent<DragUi>().previousParent.GetComponent<Image>().color = Color.white;

            DropUi drop_slot = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().GetComponentInParent<DropUi>();
            Debug.Log(eventData.pointerDrag.transform.GetComponentInParent<item_stats>().name);
            if(drop_slot.GetComponentInChildren<item_stats>().name != eventData.pointerDrag.transform.GetComponentInParent<item_stats>().name)
            {
                Debug.Log("��ø");
                DragUi ui = eventData.pointerDrag.GetComponent<DragUi>();
                gameManager.item_overlap = true;
                ui.posreset();
            }

            else if (drop_slot.GetComponent<DropUi>().uitype != DropUiType.inven &&
                drop_slot.GetComponent<DropUi>().uitype.ToString() != eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_type.ToString())
            {
                Debug.Log("��ĭ �ٸ�");
                DragUi ui = eventData.pointerDrag.GetComponent<DragUi>();
                gameManager.item_overlap = true;
                ui.posreset();
            }
            else
            {
                int item_str = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_str;
                int item_dex = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_dex;
                int item_agi = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_agi;
                int item_luk = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_luk;
                string type = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_type.ToString();
                string Ui = uitype.ToString();

                // �巡�� �ϰ��ִ� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;


                if (Ui == type && past_sloat != Ui)
                {
                    gameManager.item_equip(item_str, item_dex, item_agi, item_luk);
                }
            }


        }

    }
}
