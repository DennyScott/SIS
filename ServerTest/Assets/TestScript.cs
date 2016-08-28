using UnityEngine;
using System.Collections;
using Zephyr.EventSystem.Core;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            EventManager.Instance.QueueEvent(new OutgoingSISEvent("KEYPRESS", "Space pressed"));
        }
    }
}
