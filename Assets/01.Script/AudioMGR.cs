using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMGR : MonoBehaviour
{
    public static AudioMGR instance;

    public AudioSource myAudio;
    
    public AudioClip Crocodile_Attack;
    public AudioClip Crocodile_Dead;
    public AudioClip Plant_Attack;
    public AudioClip Plant_Dead;
    public AudioClip Snake_Intro;
    public AudioClip Snake_Poison;
    public AudioClip Snake_Spin;
    public AudioClip Snake_Rain;
    public AudioClip Snake_Dead;
    public AudioClip Scolpion_Attack;
    public AudioClip Scolpion_Dead;
    public AudioClip Mummy_Attack;
    public AudioClip Mummy_Dead;
    public AudioClip OWO_Intro;
    public AudioClip OWO_ATK1;
    public AudioClip OWO_ATK2;
    public AudioClip OWO_ATK3;
    public AudioClip OWO_Toin;
    public AudioClip OWO_Toout;
    public AudioClip OWO_Dead;
    public AudioClip Bear_Attack;
    public AudioClip Bear_Dead;
    public AudioClip Snowman_Attack;
    public AudioClip Snowman_Dead;
    public AudioClip Slav_Intro;
    public AudioClip Slav_Attack1;
    public AudioClip Slav_Attack2;
    public AudioClip Slav_Attack3;
    public AudioClip Slav_Dead;
    public AudioClip Player_Attack;
    public AudioClip Player_Reload;
    public AudioClip Player_Knife;
    public AudioClip Player_Dash;
    public AudioClip Player_DMG;
    public AudioClip OpenShop;
    public AudioClip QuitShop;
    public AudioClip BuyShop;
    public AudioClip GetMoney;
    public AudioClip Enter_Gate;
    public AudioClip Heal;
    public AudioClip BGM_Stage1;
    public AudioClip BGM_Stage2;
    public AudioClip BGM_Stage3;
    public AudioClip BGM_OWO;
    public AudioClip BGM_Snake;
    public AudioClip BGM_Slav;
    public AudioClip Swing;
    public AudioClip Jump;
    public AudioClip Bear_Gun;
    public AudioClip SnowDestroy;
    public AudioClip Death;
    public AudioClip Credit;
    public AudioClip Grenade_Throw;
    public AudioClip RPG_Shot;
    public AudioClip RPG_Boom;
    public AudioClip AirP_Break1;
    public AudioClip AirP_Break2;
    public AudioClip AirP_Break3;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    public void Play_Grenade_Throw()
    {
        RandomPitch();
        myAudio.PlayOneShot(Grenade_Throw);
    }
    public void Play_RPG_Shot()
    {
        RandomPitch();
        myAudio.PlayOneShot(RPG_Shot);
    }
    public void Play_RPG_Boom()
    {
        RandomPitch();
        myAudio.PlayOneShot(RPG_Boom);
    }
    public void Play_BGM_Death()
    {
        myAudio.PlayOneShot(Death);
    }
    public void Play_BGM_Credit()
    {
        myAudio.PlayOneShot(Credit);
    }
    public void Play_Snow_Break()
    {
        RandomPitch();
        myAudio.PlayOneShot(SnowDestroy);
    }
    public void Play_Bear_Gun()
    {
        RandomPitch();
        myAudio.PlayOneShot(Bear_Gun);
    }
    public void Play_Player_Jump()
    {
        RandomPitch();
        myAudio.PlayOneShot(Jump);
    }
    public void Play_Crocodile_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Crocodile_Attack);
    }
    public void Play_Crocodile_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Crocodile_Dead);
    }
    public void Play_Plant_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Plant_Attack);
    }
    public void Play_Plant_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Plant_Dead);
    }
    public void Play_Snake_Intro()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snake_Intro);
    }
    public void Play_Snake_Poison()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snake_Poison);
    }
    public void Play_Snake_Spin()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snake_Spin);
    }
    public void Play_Snake_Rain()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snake_Rain);
    }
    public void Play_Snake_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snake_Dead);
    }
    public void Play_Scolpion_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Scolpion_Attack);
    }
    public void Play_Scolpion_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Scolpion_Dead);
    }
    public void Play_Mummy_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Mummy_Attack);
    }
    public void Play_Mummy_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Mummy_Dead);
    }
    public void Play_OWO_Intro()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_Intro);
    }
    public void Play_OWO_Attack1()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_ATK1);
    }
    public void Play_OWO_Attack2()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_ATK2);
    }
    public void Play_OWO_Attack3()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_ATK3);
    }
    public void Play_OWO_Toin()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_Toin);
    }
    public void Play_OWO_Toout()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_Toout);
    }
    public void Play_OWO_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(OWO_Dead);
    }
    public void Play_Bear_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Bear_Attack);
    }
    public void Play_Bear_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Bear_Dead);
    }
    public void Play_Snowman_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snowman_Attack);
    }
    public void Play_Snowman_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Snowman_Dead);
    }
    public void Play_Slav_Intro()
    {
        RandomPitch();
        myAudio.PlayOneShot(Slav_Intro);
    }
    public void Play_Slav_Attack1()
    {
        RandomPitch();
        myAudio.PlayOneShot(Slav_Attack1);
    }
    public void Play_Slav_Attack2()
    {
        RandomPitch();
        myAudio.PlayOneShot(Slav_Attack2);
    }
    public void Play_Slav_Attack3()
    {
        RandomPitch();
        myAudio.PlayOneShot(Slav_Attack3);
    }
    public void Play_Slav_Dead()
    {
        RandomPitch();
        myAudio.PlayOneShot(Slav_Dead);
    }
    public void Play_Player_Attack()
    {
        RandomPitch();
        myAudio.PlayOneShot(Player_Attack);
    }
    public void Play_Reload_Sound()
    {
        RandomPitch();
        myAudio.PlayOneShot(Player_Reload);
    }
    public void Play_Player_Knife()
    {
        RandomPitch();
        myAudio.PlayOneShot(Player_Knife);
    }
    public void Play_Player_Dash()
    {
        RandomPitch();
        myAudio.PlayOneShot(Player_Dash);
    }
    public void Play_Player_DMG()
    {
        RandomPitch();
        myAudio.PlayOneShot(Player_DMG);
    }
    public void Play_Shop_Oper()
    {
        RandomPitch();
        myAudio.PlayOneShot(OpenShop);
    }
    public void Play_Shop_Close()
    {
        RandomPitch();
        myAudio.PlayOneShot(QuitShop);
    }
    public void Play_Shop_Buy()
    {
        RandomPitch();
        myAudio.PlayOneShot(BuyShop);
    }
    public void Play_Player_GetMoney()
    {
        RandomPitch();
        myAudio.PlayOneShot(GetMoney);
    }
    public void Play_Player_EnterGate()
    {
        RandomPitch();
        myAudio.PlayOneShot(Enter_Gate);
    }
    public void Play_Player_Heal()
    {
        RandomPitch();
        myAudio.PlayOneShot(Heal);
    }
    public void Play_BGM_Stage1()
    {
        myAudio.PlayOneShot(BGM_Stage1);
    }
    public void Play_BGM_Stage2()
    {
        myAudio.PlayOneShot(BGM_Stage2);
    }
    public void Play_BGM_Stage3()
    {
        myAudio.PlayOneShot(BGM_Stage3);
    }
    public void Play_BGM_OWO()
    {
        myAudio.PlayOneShot(BGM_OWO);
    }
    public void Play_BGM_Snake()
    {
        myAudio.PlayOneShot(BGM_Snake);
    }
    public void Play_BGM_Slav()
    {
        myAudio.PlayOneShot(BGM_Slav);
    }
    public void Play_Player_Swing()
    {
        RandomPitch();
        myAudio.PlayOneShot(Swing);
    }

    public void RandomPitch()
    {
        myAudio.pitch = Random.Range(0.9f, 1.1f);
    }
}
