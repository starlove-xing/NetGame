using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public Text name;
    void Start()
    {
        name = gameObject.transform.GetComponentInChildren<Text>();
    }

    void Update()
    {

    }
    public void Set(string name)
    {
        this.name.text = name;
    }
}
