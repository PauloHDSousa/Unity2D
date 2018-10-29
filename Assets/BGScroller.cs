using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {
	
    public List<GameObject> backgorunds;
    int numeroBG =0;

    void Start () {
	}
	
	void Update () {
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if(outro.transform.tag == "Player"){
            numeroBG = backgorunds.Count;
            float larguraBack = backgorunds[0].GetComponent<BoxCollider2D>().size.x;
            Vector3 posicao = backgorunds[0].GetComponent<BoxCollider2D>().transform.position;
            posicao.x  = larguraBack * numeroBG;

            backgorunds[0].GetComponent<BoxCollider2D>().transform.position= posicao;
        }
    }
}
