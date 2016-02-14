﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Events : MonoBehaviour {

    private GameManager gm; //많이 쓸것같아서 만들어둠

    public GameObject UnlockUpBasic_Box;
    public GameObject UserLimitExcess_Box;
    public GameObject RivalGameRelease_Box;
    public GameObject MacroEvent_Box;
    public GameObject TreeOfSavior_Box;
    public GameObject GettingFamous_Box;

    void Start ()
    {
        gm = GameManager.gm;

        gm.EventCheck += UnlockUpBasic;
        gm.EventCheck += UserLimitExcess;
        gm.EventCheck += RivalGameRelease;
        gm.EventCheck += MacroEvent;
        gm.EventCheck += TreeOfSavior;
        gm.EventCheck += GettingFamous;
    }

    void UnlockUpBasic()
    {
        if (GameManager.gm.Fame >= 5000)
        {
            GameManager.gm.EventCheck -= UnlockUpBasic;
 
            Instantiate(UnlockUpBasic_Box);
            LogText.WriteLog("인기에 힘입어 LV.2 다람쥐를 개발했다!");
            UserChat.CreateChat("GM: 고레벨 다람쥐가 새롭게 등장합니다!!", 5);
            
            gm.FameChange += gm.FameDaram2;
            gm.UserChange += gm.UserLevel2;
            gm.EventCheck += UserChat.uc.Daram2Number;
            Unlockables.SetBool("UnlockDaram2", true);
            
        }
    }

    void UserLimitExcess() {
        if (GameManager.gm.UserAllCount() > Unlockables.GetInt("UserLimit")) {

            Instantiate(UserLimitExcess_Box);
            LogText.WriteLog("서버가 충당 가능한 유저 수를 초과했습니다.");
            LogText.WriteLog("유저들이 접속 불량을 호소합니다. (유저 수와 인기도가 감소합니다.)");
            
            
            //유저채팅 추가
            gm.UserChat += UserChat.uc.UserLimitExcess;

            GameManager.gm.UserCount[User.level1] -= (int)( GameManager.gm.UserCount[User.level1] * Random.Range(0.3f, 0.5f));
            GameManager.gm.UserCount[User.level2] -= (int)(GameManager.gm.UserCount[User.level2] * Random.Range(0.3f, 0.5f));
            GameManager.gm.Fame -= (int)(GameManager.gm.Fame * (0.2 - 0.015 * Mathf.Min(10, GameManager.gm.DeveloperCount[Developer.Customer])));

        }
    }

    void RivalGameRelease() {
        if (Random.value < 1f/5401f) {
            Instantiate(RivalGameRelease_Box);
            LogText.WriteLog("경쟁작 '전설의 어둠'이 베타 테스트를 시작했다!");
            LogText.WriteLog("(유저 수가 감소합니다.)");
            UserChat.CreateChat("전설의 어둠하러 갑시다.", 5);
            UserChat.CreateChat("ㄱㄱㄱ", 5);
            UserChat.CreateChat("이 게임 접으려는데 아이디 사실 분?", 5);

            GameManager.gm.UserCount[User.level1] -= 1500 + (int)(GameManager.gm.UserCount[User.level1] * 0.1f) - 150 * Mathf.Min(10, GameManager.gm.DeveloperCount[Developer.Publicity]);
            GameManager.gm.UserCount[User.level2] -= (int)(GameManager.gm.UserCount[User.level2] * 0.1f);

            gm.EventCheck -= RivalGameRelease;
        }
    }

    void MacroEvent()
    {
        if (gm.UserAllCount() >= 10000 && Random.value < 1f/3601f)
            Instantiate(MacroEvent_Box);
    }

    void TreeOfSavior()
    {
        if (gm.time >= 200)
        {
            Instantiate(TreeOfSavior_Box);
            gm.EventCheck -= TreeOfSavior;
        }
    }

    void GettingFamous()
    {
        if (gm.Fame >= 10000)
        {
            Instantiate(GettingFamous_Box);
            gm.UserCount[User.level1] += 1000 + 100 * GameManager.gm.DeveloperCount[Developer.Publicity];
            gm.UserCount[User.level2] += 100;
            gm.EventCheck -= GettingFamous;
        }
    }
}
