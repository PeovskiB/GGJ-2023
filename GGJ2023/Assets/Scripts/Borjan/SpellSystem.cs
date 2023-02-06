using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSystem : MonoBehaviour
{
    private Mana mana;
    [SerializeField]private Image barImage;

    private bool freezeToggled;
    public static float freezeEffect;

    //Drain for the freeze spell
    float manaDrain = 30f;

    public static SpellSystem instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }  
        else{
            Destroy(gameObject);
        }

    }

    void Start(){
        int maxMana = PlayerStats.instance.stats.GetMaxMana();
        float manaRegen  = PlayerStats.instance.stats.GetManaRegen();
        mana = new Mana(maxMana, manaRegen, barImage);
	freezeEffect = 1f;
    }

    void Update(){   

        ToggleFreeze();
        
        if(freezeToggled)
            mana.Drain(manaDrain);
        else
           mana.Regen();

    }

    void ToggleFreeze(){
        //Get input
        if(Input.GetKeyDown(KeyCode.F))
            freezeToggled = !freezeToggled;
        //Check if has mana to maintain
        if(freezeToggled)
            freezeToggled = mana.CheckIfCanDrain();
        //For testing puproses
        if(freezeToggled)
            Time.timeScale = 0.5f;  
        else
            Time.timeScale = 1f;  
    }

    public void UpdateManaStats(){
        int maxMana = PlayerStats.instance.stats.GetMaxMana();
        float manaRegen  = PlayerStats.instance.stats.GetManaRegen();
        mana.SetMaxMana(maxMana);
        mana.SetManaRegen(manaRegen);
    }

}

public class Mana{
    public int maxMana = 100;
    private float manaAmount;
    private Image barImage;
    
    public float ManaAmount{
        get{
            return manaAmount;
        }
        set{
            if(value > maxMana)
                value = maxMana;
            else if(value < 0)
                value = 0;    
            manaAmount = value;
            barImage.fillAmount = GetManaNormalized();    
        }
    }

    private float manaRegenAmount;

    public Mana(int maxMana, float manaRegen, Image img){
        this.maxMana = maxMana;
        manaAmount = 0;
        manaRegenAmount = manaRegen;
        barImage = img;
    }

    public void Regen(){
        ManaAmount += manaRegenAmount * Time.deltaTime;
    }

    public void Drain(float drainAmount){
        ManaAmount -= drainAmount * Time.deltaTime;
    }

    public float GetManaNormalized(){
        return ManaAmount/maxMana;
    }

    public bool CheckIfCanDrain(){
        if(ManaAmount == 0){
            return false;
        }
        return true;        
    }

    public void SetMaxMana(int x){
        maxMana = x;
    }
    public void SetManaRegen(float x){
        manaRegenAmount = x;
    }

    public void SetBarImage(Image img){
        barImage = img;
    }

}
