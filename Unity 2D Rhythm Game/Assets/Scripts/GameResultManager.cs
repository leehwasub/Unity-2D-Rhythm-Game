using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    public List<Text> rankUI;

    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "점수: " + (int)PlayerInformation.score;
        maxComboUI.text = "최대 콤보: " + PlayerInformation.maxBombo;
        //리소스에서 비트 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //첫번째 줄과 두번째 줄을 무시합니다.
        reader.ReadLine();
        reader.ReadLine();
        // 세번째 줄에 적인 비트정보 점수 기준을 읽습니다.
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        // 성적에 맞는 랭크이미지 로드
        if(PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }
        else if (PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
        initRankText("데이터를 불러오는 중입니다.");
        DatabaseReference reference;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-rhythm-game-tutori-72cdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.GetReference("ranks")
            .Child(PlayerInformation.selectedMusic);
        //데이터 셋의 모든 데이터를 json형태로 가져온다.
        reference.OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            //성공적으로 데이터를 가져온경우
            if (task.IsCompleted)
            {
                List<string> rankList = new List<string>();
                List<string> emailList = new List<string>();
                DataSnapshot snapshot = task.Result;
                //json 데이터의 각원소에 접근합니다.
                foreach(DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    emailList.Add(rank["email"].ToString());
                    rankList.Add(rank["score"].ToString());
                }
                //정렬 이후 내림차순 정렬합니다.
                emailList.Reverse();
                rankList.Reverse();
                // 최대 상위 3명의 순위를 차례대로 화면에 출력한다.
                initRankText("플레이 한 사용자가 없습니다.");
                List<Text> textList = new List<Text>();
                for (int i = 0; i < 3; i++)
                {
                    textList.Add(rankUI[i]);
                }
                for(int i = 0; i < rankList.Count && i < 3; i++)
                {
                    textList[i].text = (i+1) + "위: " + emailList[i] + " (" + rankList[i] + " 점)";
                }
            }
        });
    }

    public void initRankText(string text)
    {
        for (int i = 0; i < 3; i++)
        {
            rankUI[i].text = text;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }

    void Update()
    {
        
    }
}
