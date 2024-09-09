using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlChicken : Mgr
{
    [SerializeField] private ChickenLerp_Shader lerpShader;
    [SerializeField] private BowlEgg bowlEgg;

    [SerializeField] private float disW;
    [SerializeField] private float disH;
    private Vector3 basePos;
    private bool init;
    private float eggTime = 0;
    private const float EGG_DELAY = 1f;
    public bool isDrag { private set; get; }

    public void Init()
    {
        if (init == false)
        {
            init = true;
            basePos = lerpShader.transform.localPosition;
        }

        lerpShader.transform.localPosition = basePos;
        eggTime = 0;
        lerpShader.SetValue(0);
        lerpShader.gameObject.SetActive(true);
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }
        isDrag = true;

        if (CompleteEgg() == false)
        {
            eggTime += Time.deltaTime;
            float v = eggTime / EGG_DELAY;
            v = Mathf.Min(v, 1);
            lerpShader.SetValue(v);

            MoveDragPos();
            lerpShader.gameObject.SetActive(true);
        }
        else
        {
            kitchenMgr.dragState = DragState.Egg;
            lerpShader.gameObject.SetActive(false);
        }
    }

    private void MoveDragPos()
    {
        //ġŲ�� ���ȿ����� ������ �� �ֵ����Ѵ�.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, lerpShader.transform.position.z);

        double result = ((mousePos.x - transform.position.x) * (mousePos.x - transform.position.x)) /
            (disW * disW) + ((mousePos.y - transform.position.y) * (mousePos.y - transform.position.y)) / (disH * disH);

        if (result <= 1)
        {
            //Ÿ�� �������̶�� ���콺 ��ǥ��
            lerpShader.transform.position = mousePos;
        }
        else
        {
            //Ÿ�����̸� Ÿ���ȿ��� �ִ��� ���콺 ��ǥ�� ����� ������ �̵��ϵ���

            Vector3 zeroBaseMousePos = mousePos - transform.position;
            float x1 = ((disW * disH) / Mathf.Sqrt(disW * disW * zeroBaseMousePos.y * zeroBaseMousePos.y + disH * disH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.x;
            float x2 = -x1;
            float y1 = ((disW * disH) / Mathf.Sqrt(disW * disW * zeroBaseMousePos.y * zeroBaseMousePos.y + disH * disH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.y;
            float y2 = -y1;

            // �� ���� �� mousePos�� ����� �� ����
            float dist1 = Vector2.Distance(new Vector2(x1, y1), zeroBaseMousePos);
            float dist2 = Vector2.Distance(new Vector2(x2, y2), zeroBaseMousePos);

            lerpShader.transform.position = (dist1 < dist2 ? new Vector3(x1, y1) : new Vector3(x2, y2)) + transform.position;
        }
    }



    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        isDrag = false;

        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //�а��� ������ ġŲ�� �巡����
            if (kitchenMgr.trayFlour.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                bowlEgg.RemoveChicken();
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Tray_Flour2 &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //�а��� ������ ġŲ�� �巡����
            if (kitchenMgr.trayFlour2.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                bowlEgg.RemoveChicken();
                return;
            }
        }

        kitchenMgr.dragState = DragState.None;

        if (bowlEgg.IsMax())
            lerpShader.gameObject.SetActive(true);
        else
            lerpShader.gameObject.SetActive(false);
    }

    public bool CompleteEgg()
    {
        //����� ������ �۾��� �Ϸ����� ���θ� ��ȯ
        return eggTime >= EGG_DELAY;
    }

    public void WorkerEggChickenPutAway()
    {
        if (isDrag)
            return;
        Init();
        eggTime = EGG_DELAY;
        lerpShader.SetValue(1);
        lerpShader.gameObject.SetActive(true);
    }

    public void WorkerDragChicken(float v)
    {
        eggTime += v;
        lerpShader.SetValue(eggTime/ EGG_DELAY);
    }
}
