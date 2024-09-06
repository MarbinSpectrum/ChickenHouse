using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResumeInfoList : Mgr
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillInfo;

    public void SetUI(WorkerSkill pWorkerSkill)
    {
        string skillNameStr = workerMgr.GetWorkSkillNameString(pWorkerSkill);
        string skillInfoStr = workerMgr.GetWorkSkillInfoString(pWorkerSkill);
        LanguageMgr.SetText(skillName, skillNameStr);
        LanguageMgr.SetText(skillInfo, skillInfoStr);
    }
}
