using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Vital : MonoBehaviour {

    public Slider Health_Bar;
    public int Max_Health;
    private int Fall_Rate_Health;

    public Slider Hunger_Bar;
    public int Max_Hunger;
    private int Fall_Rate_Hunger;

    public Slider Thirst_Bar;
    public int Max_Thirst;
    private int Fall_Rate_Thirst;

    public bool Emergency_Flag;
    public bool Health_Alert, Hunger_Alert, Thirst_Alert;

    public Text Health_Text, Hunger_Text, Thirst_Text;
    public AudioManager sound;

    //使用16進位方便疊加狀態
    enum Status
    {
        Normal = 0x00,
        Wounded = 0x01,
        ill = 0x02,
        Starve = 0x04,
        Dehydrated = 0x08
    }
    Status myStatus = new Status();

    void Start ()
    {
        Health_Bar.maxValue = Max_Health;
        Health_Bar.value = GlobalManager.Instance.Health;

        Hunger_Bar.maxValue = Max_Hunger;
        Hunger_Bar.value = GlobalManager.Instance.Hunger;

        Thirst_Bar.maxValue = Max_Thirst;
        Thirst_Bar.value = GlobalManager.Instance.Thirst;

        myStatus = Status.Normal;
        Emergency_Flag = false;

        Fall_Rate_Health = 0;

        sound = FindObjectOfType<AudioManager>();
    }
	
	void Update ()
    {

        if (Health_Bar.value <= 0) Die();

        Check_Status();
        Value_Monitor();
    }

    //在切換場景時將數值上傳至全域變數
    void OnDisable()
    {
        GlobalManager.Instance.Health = (int)Health_Bar.value;
        GlobalManager.Instance.Hunger = (int)Hunger_Bar.value;
        GlobalManager.Instance.Thirst = (int)Thirst_Bar.value;

        Debug.Log(GlobalManager.Instance.Health);
        Debug.Log(GlobalManager.Instance.Hunger);
        Debug.Log(GlobalManager.Instance.Thirst);

    }

    void Die()
    {


    }

    ////檢查狀態////
    void Check_Status()
    {
        //顯示數值
        Health_Text.text = Health_Bar.value.ToString() + "%";
        Hunger_Text.text = Hunger_Bar.value.ToString() + "%";
        Thirst_Text.text = Thirst_Bar.value.ToString() + "%";

        //確認是否受傷或生病
        if ((myStatus & Status.Normal) == Status.Normal)
        {
            Fall_Rate_Hunger = 1;
            Fall_Rate_Thirst = 2;
        }
        else if ((myStatus & Status.ill) == Status.ill || (myStatus & Status.Wounded) == Status.Wounded) 
        {
            if ((myStatus & Status.ill) == Status.ill && (myStatus & Status.Wounded) == Status.Wounded)
            {
                Fall_Rate_Hunger = 3;
                Fall_Rate_Thirst = 4;
            }
            else
            {
                Fall_Rate_Hunger = 2;
                Fall_Rate_Thirst = 3;
            }
        }

        //確認是否脫水或飢餓
        if (Hunger_Bar.value <= 0) myStatus = myStatus | Status.Starve;
        else if ((myStatus & Status.Starve) == Status.Starve) myStatus = myStatus ^ Status.Starve; // 若沒加條件會變添加狀態

        if (Thirst_Bar.value <= 0) myStatus = myStatus | Status.Dehydrated;
        else if((myStatus & Status.Dehydrated) == Status.Dehydrated) myStatus = myStatus ^ Status.Dehydrated;

        if ((myStatus & Status.Starve) == Status.Starve || (myStatus & Status.Dehydrated) == Status.Dehydrated) // 若沒加條件會變添加狀態
        {
            if ((myStatus & Status.Starve) == Status.Starve && (myStatus & Status.Dehydrated) == Status.Dehydrated)
            {
                Fall_Rate_Health = 6;
                Emergency_Flag = true;
            }
            else
            {
                Fall_Rate_Health = 3;
                Emergency_Flag = true;
            }
        }
        else Emergency_Flag = false;

    }

    ////監測各數值的變化////
    void Value_Monitor()
    {
        //避免數值超過Max
        if (Health_Bar.value > Max_Health) Health_Bar.value = Max_Health;
        if (Hunger_Bar.value > Max_Hunger) Hunger_Bar.value = Max_Hunger;
        if (Thirst_Bar.value > Max_Thirst) Thirst_Bar.value = Max_Thirst;

        //播放警示音效
        if (Health_Bar.value <= 50 && Health_Alert == false)
        {
            sound.Play("heart");
            Health_Alert = true;
        }
        else if (Health_Bar.value > 50) Health_Alert = false;

        if (Hunger_Bar.value <= 50 && Hunger_Alert == false)
        {
            sound.Play("hungry");
            Hunger_Alert = true;
        }
        else if (Hunger_Bar.value > 50) Hunger_Alert = false;

        if (Thirst_Bar.value <= 50 && Thirst_Alert == false)
        {
            sound.Play("thirsty");
            Thirst_Alert = true;
        }
        else if (Thirst_Bar.value > 50) Thirst_Alert = false;
    }

    ////////////////數值增加////////////////
    public void Heal(int heal_value)
    {
        if (Health_Bar.value < Max_Health) Health_Bar.value += heal_value;
        else if (Health_Bar.value >= Max_Health) Health_Bar.value = Max_Health;
        sound.Play("heart");
    }

    public void Eat(int food_value)
    {
        if (Hunger_Bar.value < Max_Hunger) Hunger_Bar.value += food_value;
        else if (Hunger_Bar.value >= Max_Hunger) Hunger_Bar.value = Max_Hunger;
        sound.Play("eat1");
    }

    public void Drink(int hydration_value)
    {
        if (Thirst_Bar.value < Max_Thirst) Thirst_Bar.value += hydration_value;
        else if (Thirst_Bar.value >= Max_Thirst) Thirst_Bar.value = Max_Thirst;
        sound.Play("drink");
    }
    ////////////////數值增加////////////////


    ////////////////數值減少////////////////
    public void Health_Drop()
    {
        if(Health_Bar.value > 0) Health_Bar.value -= Fall_Rate_Health; 
        else if (Health_Bar.value <= 0) Health_Bar.value = 0;
    }

    public void Hurt()
    {
        Fall_Rate_Health = 20;
        if (Health_Bar.value > 0) Health_Bar.value -= Fall_Rate_Health;
        else if (Health_Bar.value <= 0) Health_Bar.value = 0;
    }

    public void Hunger_Drop()
    {
        if (Hunger_Bar.value > 0) Hunger_Bar.value -= Fall_Rate_Hunger;
        else if (Hunger_Bar.value <= 0) Hunger_Bar.value = 0 ;      
    }

    public void Thirst_Drop()
    {
        if (Thirst_Bar.value > 0) Thirst_Bar.value -= Fall_Rate_Thirst;
        else if (Thirst_Bar.value <= 0) Thirst_Bar.value = 0;
    }
    ///////////////數值減少////////////////
}
