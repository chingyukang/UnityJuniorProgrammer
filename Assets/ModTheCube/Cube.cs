using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    Material material;
    [SerializeField] float rotateSpeed = 10.0f;
    [SerializeField] List<Color> colors = new List<Color>();
    [SerializeField] Vector3 targetPos = new Vector3(3, 4, 1);

    bool needMove = false;
    
    void Start()
    {
        transform.position = targetPos;
        transform.localScale = Vector3.one * 1.3f;

        material = Renderer.material;
        ChangeColor();
    }
    
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0.0f, 0.0f);

        if(needMove) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);

            if(transform.position == targetPos) {
                needMove = false;
                ChangeColor();
            }
        }
    }

    private void ChangeColor() {
        int colorCount = colors.Count;
        if(colorCount > 0) {
            material.color = colors[Random.Range(0, colorCount)];
        } else {
            material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);
        }

        MoveToTarget();
    }

    private void MoveToTarget() {
        targetPos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        needMove = true;
    }
}
