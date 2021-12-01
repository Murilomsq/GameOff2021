using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    #region Singleton

    private static PlayerInteractions instance;
    public static PlayerInteractions Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private PlayerWeapData playerWeapSO;
    
    public CharacterMovement cm;
    [SerializeField] private GameObject weaponHolder;
    private IEquippable[] weapons = new IEquippable[3];
    public IEquippable[] weaponsEquipped = new IEquippable[2];

    private int numOfWeaps = 0;
    
    // Canvas related stuff
    [Header("Canvas/UI")]
    public Image weap0Img;
    public Image weap1Img;
    [SerializeField] private Sprite activeImg;
    [SerializeField] private Sprite unactiveImg;
    [SerializeField] private GameObject deathCanvas;
    


    [Header("Sound")] 
    public AudioSource ost;
    public AudioSource footstep;
    public AudioSource generalAudioSource;
    public AudioClip fire;
    public AudioClip dash;
    public AudioClip bigShotFire;
    public AudioClip machineGunFire;

    [Header("Health sprites")] 
    [SerializeField] private Image battery;
    [SerializeField] private Image weap0;
    [SerializeField] private Image weap1;
    [SerializeField] private Sprite health4;
    [SerializeField] private Sprite health3;
    [SerializeField] private Sprite health2;
    [SerializeField] private Sprite health1;
    [SerializeField] private Sprite health0;
    [SerializeField] private Sprite[] WeapImg;


    // Props
    private int health = 4;


    public void Damage()
    {
        health--;
        SetHealthImg();
        if (health == 0)
        {
            Destroy(GetComponent<CharacterController>());
            Destroy(GetComponent<CharacterMovement>());
            Destroy(weaponHolder);
            deathCanvas.SetActive(true);
        }
    }

    public void HealAll()
    {
        health = 4;
        SetHealthImg();
    }

    public void SetHealthImg() // This looks so stupid why didn't I use a damn array?
    {
        switch (health)
        {
            case 0:
                battery.sprite = health0;
                break;
            case 1:
                battery.sprite = health1;
                break;
            case 2:
                battery.sprite = health2;
                break;
            case 3:
                battery.sprite = health3;
                break;
            case 4:
                battery.sprite = health4;
                break;
        }
    }

    // Used to select player weapons at the beggining of the game
    public void EquipPlayerWeapon(int i, int j)
    {
        weap0.sprite = WeapImg[i];
        weap1.sprite = WeapImg[j];
        weaponsEquipped[0] = weapons[i];
        weaponsEquipped[1] = weapons[j];
    }

    public void SwitchWeapTo(int weap)
    {
        weaponsEquipped[weap].Equip();
        if (weap == 0)
        {
            weaponsEquipped[1].Unequip();
            //Gui stuff
            weap0Img.sprite = activeImg;
            weap1Img.sprite = unactiveImg;
        }
        else
        {
            weaponsEquipped[0].Unequip();
            //Gui stuff
            weap1Img.sprite = activeImg;
            weap0Img.sprite = unactiveImg;
        }
    }

    private void Start()
    {
        weapons[0] = weaponHolder.GetComponent<ChargeFireball>();
        weapons[1] = weaponHolder.GetComponent<FastShooting>();
        weapons[2] = weaponHolder.GetComponent<BulletRainScript>();
        EquipPlayerWeapon(playerWeapSO.weapOne, playerWeapSO.weapTwo);
        SwitchWeapTo(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapTo(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapTo(1);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            deathCanvas.SetActive(!deathCanvas.activeSelf);
        }
    }
    
    //Scene stuff (breaking literally all SOLID principles)

    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
