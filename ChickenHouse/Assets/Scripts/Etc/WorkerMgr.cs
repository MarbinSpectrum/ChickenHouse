using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMgr : AwakeSingleton<WorkerMgr>
{
    [SerializeField] private Dictionary<EWorker, WorkerData> workerData = new Dictionary<EWorker, WorkerData>();
    public const float WORKER_SKILL_1_VALUE = 50;
    public const float WORKER_SKILL_2_VALUE = 100;
    public const float WORKER_SKILL_3_VALUE = 100;
    public const float WORKER_SKILL_4_VALUE = 100;
    public const float WORKER_SKILL_5_VALUE = 25;

    public WorkerData GetWorkerData(EWorker pWorker)
    {
        //직원 정보 얻기
        if (workerData.ContainsKey(pWorker))
            return workerData[pWorker];
        return null;
    }

    public static string GetWorkSkillNameString(WorkerSkill pWorkerSkill)
    {
        //직원 스킬 이름
        switch (pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                return LanguageMgr.GetText("WORKER_SKILL_1");
            case WorkerSkill.WorkerSkill_2:
                return LanguageMgr.GetText("WORKER_SKILL_2");
            case WorkerSkill.WorkerSkill_3:
                return LanguageMgr.GetText("WORKER_SKILL_3");
            case WorkerSkill.WorkerSkill_4:
                return LanguageMgr.GetText("WORKER_SKILL_4");
            case WorkerSkill.WorkerSkill_5:
                return LanguageMgr.GetText("WORKER_SKILL_5");
            case WorkerSkill.WorkerSkill_6:
                return LanguageMgr.GetText("WORKER_SKILL_6");
            case WorkerSkill.WorkerSkill_7:
                return LanguageMgr.GetText("WORKER_SKILL_7");
            case WorkerSkill.WorkerSkill_8:
                return LanguageMgr.GetText("WORKER_SKILL_8");
        }
        return string.Empty;
    }

    public static string GetWorkSkillInfoString(WorkerSkill pWorkerSkill)
    {
        //직원 스킬 내용
        switch (pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_1");
            case WorkerSkill.WorkerSkill_2:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_2");
            case WorkerSkill.WorkerSkill_3:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_3");
            case WorkerSkill.WorkerSkill_4:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_4");
            case WorkerSkill.WorkerSkill_5:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_5");
            case WorkerSkill.WorkerSkill_6:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_6");
            case WorkerSkill.WorkerSkill_7:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_7");
            case WorkerSkill.WorkerSkill_8:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_8");
        }
        return string.Empty;
    }

    public static string GetWorkSkillDetailString(WorkerSkill pWorkerSkill)
    {
        //직원 스킬 설명
        string detailStr = string.Empty;
        switch (pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                detailStr = string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_3"), WORKER_SKILL_1_VALUE);
                break;
            case WorkerSkill.WorkerSkill_2:
                detailStr = string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_3"), WORKER_SKILL_2_VALUE);
                break;
            case WorkerSkill.WorkerSkill_3:
                detailStr = string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_5"), WORKER_SKILL_3_VALUE);
                detailStr += "\n";
                detailStr += LanguageMgr.GetText("WORKER_SKILL_FORMAT_6");
                break;
            case WorkerSkill.WorkerSkill_4:
                detailStr = string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_2"), WORKER_SKILL_4_VALUE);
                break;
            case WorkerSkill.WorkerSkill_5:
                detailStr = string.Format(LanguageMgr.GetText("WORKER_SKILL_FORMAT_4"), WORKER_SKILL_5_VALUE);
                break;
            case WorkerSkill.WorkerSkill_6:
                detailStr = LanguageMgr.GetText("WORKER_SKILL_FORMAT_7");
                break;
            case WorkerSkill.WorkerSkill_7:
                detailStr = LanguageMgr.GetText("WORKER_SKILL_FORMAT_8");
                break;
            case WorkerSkill.WorkerSkill_8:
                detailStr = LanguageMgr.GetText("WORKER_SKILL_FORMAT_9");
                break;
        }
        return detailStr;
    }
}