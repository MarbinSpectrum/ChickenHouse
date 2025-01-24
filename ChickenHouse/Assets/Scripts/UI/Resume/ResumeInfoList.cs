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
        string skillNameStr = WorkerMgr.GetWorkSkillNameString(pWorkerSkill);
        string skillInfoStr = WorkerMgr.GetWorkSkillInfoString(pWorkerSkill);
        LanguageMgr.SetText(skillName, skillNameStr);
        LanguageMgr.SetText(skillInfo, skillInfoStr);
    }
}
