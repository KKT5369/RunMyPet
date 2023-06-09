using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    private string savePath => Application.persistentDataPath + "/saves/";
    private readonly string _saveFileName = "rankData.json";
    
    // 랭크를 json 으로 저장하고 스코어 순으로 정렬 합니다.
    public void SetRankData(RankData myRankData)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        RankSaveData rankSaveData = new();
        
        if(File.Exists(savePath + _saveFileName))
        {
            string saveData = File.ReadAllText(savePath + _saveFileName);
            rankSaveData = JsonUtility.FromJson<RankSaveData>(saveData);
            List<RankData> rankDatas = rankSaveData.datas;
            rankDatas.Add(myRankData);
        
            if (rankDatas.Count > 0)
            {
                for (int i = (rankDatas.Count - 1); i > 0 ; i--)
                {
                    if (rankDatas[i - 1].score < rankDatas[i].score)
                    {
                        (rankDatas[i - 1], rankDatas[i]) = (rankDatas[i], rankDatas[i - 1]);
                    }

                    rankDatas[i].rank = i + 1;
                }

                rankDatas[0].rank = 1;
            }
            string saveJson = JsonUtility.ToJson(rankSaveData);
            File.WriteAllText(savePath + _saveFileName,saveJson);
        }
        else
        {
            myRankData.rank = 1;
            rankSaveData.datas.Add(myRankData);
            string saveJson = JsonUtility.ToJson(rankSaveData);
            File.WriteAllText(savePath + _saveFileName,saveJson);
            Debug.Log(saveJson);
        }
    }

    public RankSaveData GetRankData()
    {
        if (!Directory.Exists(savePath))
        {
            return null;
        }
        
        RankSaveData rankSaveData = new();
        if (File.Exists(savePath + _saveFileName))
        {
            string saveData = File.ReadAllText(savePath + _saveFileName);
            rankSaveData = JsonUtility.FromJson<RankSaveData>(saveData);
        }

        return rankSaveData;
    }

    public void DeleteRank()
    {
        string path = savePath + _saveFileName;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}

[Serializable]
public class RankSaveData
{
    public List<RankData> datas = new();
}

[Serializable]
public class RankData
{
    public string nicName;
    public int score;
    public int disrance;
    public int rank;
}