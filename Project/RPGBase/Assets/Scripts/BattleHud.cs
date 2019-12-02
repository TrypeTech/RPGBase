using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHud : MonoBehaviour {

    

    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    TempActor Actor;

    public void Start()
    {
        Actor = gameObject.GetComponentInParent<TempActor>();
    }

    public void SetHUD()
    {
        Actor = gameObject.GetComponentInParent<TempActor>();
        nameText.text = Actor.unitName;
        levelText.text = "Lvl " + Actor.unitLevel;
        hpSlider.maxValue = Actor.maxHP;
        hpSlider.value = Actor.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
