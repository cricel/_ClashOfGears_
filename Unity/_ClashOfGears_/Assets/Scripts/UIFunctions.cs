using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject login;
    public GameObject stake;
    public GameObject menu_icon;
    public GameObject menuSelection;
    public GameObject DAI_input;
    public GameObject ConferenceUI;

    private bool menuVisiable = false;
    private bool leaderboardVisiable = false;
    private bool conferenceUIVisiable = false;
    private int DAI_number;
    public void MetamaskLogin()
    {
        login.SetActive(false);
        stake.SetActive(true);
    }

    public void GuestLogin()
    {
        login.SetActive(false);
        stake.SetActive(true);
    }

    public void approveDAI()
    {
        //Do something
        DAI_number = int.Parse(DAI_input.GetComponent<Text>().text);
        Debug.Log("you enter: " + DAI_number + " DAI");
    }

    public void stakeDAI()
    {
        stake.SetActive(false);
        menu_icon.SetActive(true);
    }

    public void MenuSelect()
    {
        menuVisiable = !menuVisiable;
        menuSelection.SetActive(menuVisiable);
    }

    public void Leaderboard()
    {
        leaderboardVisiable = !leaderboardVisiable;
        leaderboard.SetActive(leaderboardVisiable);
    }

    public void ConferenceUIFunction()
    {
        conferenceUIVisiable = !conferenceUIVisiable;
        ConferenceUI.SetActive(conferenceUIVisiable);
    }
}
