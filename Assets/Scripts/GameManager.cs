using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject deskPrefab;
    GameObject deskObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++){
            for (int j = 1; j <= 2; j++){
                deskObject = Instantiate(deskPrefab);
                deskObject.transform.position = new Vector2(i * 2, j * (-2f) - 1f);
            }
        }
    }

    // Update is called once per frame
    void Update(){
    }
}
