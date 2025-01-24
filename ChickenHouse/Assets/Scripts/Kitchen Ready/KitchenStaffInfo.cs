using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenStaffInfo : Mgr
{
    [SerializeField] private RectTransform  faceRect;
    [SerializeField] private Image          face;
    [SerializeField] private RectTransform  infoRect;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI detailText;

    public void SetUI(EWorker pWorker)
    {
        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        if (workerData == null)
        {
            faceRect.gameObject.SetActive(false);
            infoRect.gameObject.SetActive(false);
            return;
        }
        faceRect.gameObject.SetActive(true);
        infoRect.gameObject.SetActive(true);

        face.sprite = workerData.face;
        LanguageMgr.SetString(nameText, workerData.nameKey);
        LanguageMgr.SetString(infoText, workerData.infoKey);

        string detailStr = string.Empty;
        detailStr += string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_1"), workerData.salary);
        detailStr += "\n\n";
        foreach(WorkerSkill skill in workerData.skill)
        {
            detailStr += string.Format("<{0}>", WorkerMgr.GetWorkSkillNameString(skill));
            detailStr += "\n";
            detailStr += WorkerMgr.GetWorkSkillDetailString(skill);
            detailStr += "\n\n";
        }
        LanguageMgr.SetText(detailText, detailStr);
    }
}
