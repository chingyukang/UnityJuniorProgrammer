using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Out : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject ball;
    public Transform DeadObj;

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Ball") {
            Destroy(other.gameObject);
            CreateNewPrefab(ball, "Ball");
        }
        if(other.name == "Cube") {
            Destroy(other.gameObject);
            CreateNewPrefab(cube, "Cube");
        }

        if(other.name == "Player") {
            DeadObj.gameObject.SetActive(true);
        }
    }

    private void CreateNewPrefab(GameObject p_obj, string p_name) {
        GameObject _obj = Instantiate(p_obj);
        _obj.name = p_name;
    }
}
