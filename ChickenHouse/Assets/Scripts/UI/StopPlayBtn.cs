using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopPlayBtn : Mgr
{
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite stopIcon;
    [SerializeField] private Image btnImg;
    [SerializeField] private RectTransform dontClick;

    bool gameStop = false;

    public void Start()
    {
        PlayGame();
    }

    public void ToggleBtn()
    {
        //�������� & ���ӽ�����
        //��۽����� ó��
        gameStop = !gameStop;
        if (gameStop)
            StopGame();
        else
            PlayGame();
    }

    private void StopGame()
    {
        //��������
        btnImg.sprite = playIcon;
        gameMgr.StopGame(true);
        dontClick.gameObject.SetActive(true);
    }

    private void PlayGame()
    {
        //���ӽ���
        btnImg.sprite = stopIcon;
        gameMgr.StopGame(false);
        dontClick.gameObject.SetActive(false);
    }
}
