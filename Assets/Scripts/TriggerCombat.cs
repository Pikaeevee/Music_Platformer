using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCombat : MonoBehaviour
{
    CombatManager cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("CombatManager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cm.enemy = this.gameObject.transform.parent.gameObject;
        cm.Invoke("StartCombat", 0.0f);
    }
}
