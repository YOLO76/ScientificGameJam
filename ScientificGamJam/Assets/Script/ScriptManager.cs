using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScriptManager : MonoBehaviour
{
    [Header("Var Settingd")]
    public bool win;
    public float TranslateTime = 0.2f;
    public bool keyed;
    [Header("Place Settings")]
    public GameObject Place1G;
    public GameObject Place1C;
    public GameObject Place1D;
    public GameObject ActualEnnemi;
    public GameObject Place2G;
    public GameObject Place2C;
    public GameObject Place2D;
    [Header("RandomShow Settings")]
    public List<GameObject> ListPlace;
    public List<GameObject> ListPlace2;
    public int SpawnIndex;
    [Header("WikiEnnemi Settings")]
    public List<GameObject> WikiEnnemi;
    [Header("User Settings")]
    public Slider LifeBar;
    public int life;
    public Slider DebugSlider;
    public float TimerStep;
    public GameObject LoosePanel;


    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().Play("Ambiance");


        win = true;
        life = 11;
        TimerStep = 1f;
        ListPlace.Add(Place1G);
        ListPlace.Add(Place1C);
        ListPlace.Add(Place1D);
        ListPlace2.Add(Place2G);
        ListPlace2.Add(Place2C);
        ListPlace2.Add(Place2D);
        SpawnIndex = Random.Range(0, 3);
        ActualEnnemi = Instantiate(WikiEnnemi[Random.Range(0, WikiEnnemi.Count)], ListPlace2[SpawnIndex].transform.position, Quaternion.identity);
        ActualEnnemi.transform.SetParent(ListPlace2[SpawnIndex].transform);
    }

    // Update is called once per frame
    void Update()
    {
        
       
        TimerStep -= Time.deltaTime;
        DebugSlider.value = TimerStep;
        if (TimerStep <= 0)
        {
            NextStep();  
        }
        LifeBar.value = life;
        if(life <=0 )
        {
            Loose();
        }
    }

    public void NextStep()
    {
        if (win == true && life <10)
        {
            life += 1;
        }
        else
            life -= 1;
        Debug.Log("Step");
        TimerStep = 1f;
        if(keyed == false)
            FindObjectOfType<AudioManager>().Play("Ambiance");
        keyed = false;
        if(life > 0)
        {
            NextPlan();
            SpawnIndex = Random.Range(0, 3);
            ActualEnnemi = Instantiate(WikiEnnemi[Random.Range(0, WikiEnnemi.Count)], ListPlace2[SpawnIndex].transform.position,Quaternion.identity);
            ActualEnnemi.transform.SetParent(ListPlace2[SpawnIndex].transform);
            win = false;
        }
    }
    public void Loose()
    {
        LoosePanel.SetActive(true);
        DebugSlider.gameObject.SetActive(false);
        LifeBar.gameObject.SetActive(false);
        GameObject[] ADetruire = GameObject.FindGameObjectsWithTag("Pet");
        foreach (GameObject pet in ADetruire)
            GameObject.Destroy(pet);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            Restart();
    }

    public void Restart()
    {
        LoosePanel.SetActive(false);
        DebugSlider.gameObject.SetActive(true);
        LifeBar.gameObject.SetActive(true);
        win = true;
        life = 10;
        NextStep();
    }

    public void NextPlan()
    {
        if (Place1G.transform.childCount == 1)
        {
            Transform detruit = Place1G.transform.GetChild(0);
            detruit.parent = null;
            Destroy(detruit.gameObject);
        }
        if (Place1C.transform.childCount == 1)
        {
            Transform detruit = Place1C.transform.GetChild(0);
            detruit.parent = null;
            Destroy(detruit.gameObject);
        }
        if (Place1D.transform.childCount == 1)
        {
            Transform detruit = Place1D.transform.GetChild(0);
            detruit.parent = null;
            Destroy(detruit.gameObject);
        }
        if (Place2G.transform.childCount ==1)
        {
            Place2G.transform.GetChild(0).SetParent(Place1G.transform);
            Place1G.transform.GetChild(0).DOLocalMoveZ(0,TranslateTime).SetEase(Ease.OutSine);
            //Place2G.transform.GetChild(0).gameObject.transform.position = Place1G.transform.position;
            
            

        }
        if (Place2C.transform.childCount == 1)
        {
            Place2C.transform.GetChild(0).SetParent(Place1C.transform);
            Place1C.transform.GetChild(0).DOLocalMoveZ(0, TranslateTime).SetEase(Ease.OutSine);
            //Place2C.transform.GetChild(0).gameObject.transform.position = Place1C.transform.position;
            
        }
        if (Place2D.transform.childCount == 1)
        {
            Place2D.transform.GetChild(0).SetParent(Place1D.transform);
            Place1D.transform.GetChild(0).DOLocalMoveZ(0, TranslateTime).SetEase(Ease.OutSine);
            //Place2D.transform.GetChild(0).gameObject.transform.position = Place1D.transform.position;
            
        }
        
    }

    
}
