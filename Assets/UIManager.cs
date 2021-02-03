using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Dictionary<SpawnZone, Text> monsterTimers = new Dictionary<SpawnZone, Text>();
    public Text northMonsterTimer;
    public Text southMonsterTimer;
    public Text westMonsterTimer;
    public Text easthMonsterTimer;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }

            return _instance;
        }
    }


    void Awake()
    {

        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        monsterTimers.Add(SpawnZone.East, easthMonsterTimer);
        monsterTimers.Add(SpawnZone.West, westMonsterTimer);
        monsterTimers.Add(SpawnZone.South, southMonsterTimer);
        monsterTimers.Add(SpawnZone.North, northMonsterTimer);
        
    }

    // Update is called once per frame

    public void UseMonsterUI(string changeTo,SpawnZone zone)
    {

        monsterTimers[zone].text=changeTo;
    }
    public void MonsterUiSwitch(bool isOn,SpawnZone zone)
    {

        monsterTimers[zone].gameObject.SetActive(isOn);
            
             
            
       
    }

}
