using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject weaponChooser;
    [SerializeField] private GameObject mM;

    [SerializeField] private PlayerWeapData playerSO;

    [SerializeField] private Button[] weap;
    private int weaponNum = 0;

    public void CloseGame()
    {
        Application.Quit();
    }
    public void Play()
    {
        weaponChooser.SetActive(true);
        mM.SetActive(false);
    }

    private void Click(int i)
    {
        if (weaponNum == 0)
        {
            playerSO.weapOne = i;
            weaponNum++;
        }
        else
        {
            playerSO.weapTwo = i;
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Awake()
    {
        weap[0].onClick.AddListener(() =>
        {
            Click(0);
            weap[0].interactable = false;
        });
        weap[1].onClick.AddListener(() =>
        {
            Click(1);
            weap[1].interactable = false;
        });
        weap[2].onClick.AddListener(() =>
        {
            Click(2);
            weap[2].interactable = false;
        });
    }
}
