using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public Transform DeadObj;

    private int Count = 0;

    [SerializeField] GameObject cube;
    [SerializeField] GameObject ball;

    private void Start()
    {
        Count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        CounterText.text = "Count : " + Count;

        if(other.name == "Ball") {
            CreateNewPrefab(ball, "Ball");
        }
        if(other.name == "Cube") {
            CreateNewPrefab(cube, "Cube");
        }

        if(other.name == "Player") {
            DeadObj.gameObject.SetActive(true);
        }
    }

    private void CreateNewPrefab(GameObject p_obj, string p_name) {
        GameObject _obj =  Instantiate(p_obj);
        _obj.name = p_name;
    }

    public void OnClickRestart() {
        SceneManager.LoadScene(0);
    }
}
