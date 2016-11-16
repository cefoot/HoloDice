using UnityEngine;
using System.Collections;

public class LaserCheck : MonoBehaviour {

    public GameObject ToLookFor;

    private Collider colliderToLookFor;
    private Collider myCollider;
    private Renderer myRenderer;

    // Use this for initialization
    void Start ()
    {
        if (ToLookFor == null) { return; }
        colliderToLookFor = ToLookFor.GetComponent<Collider>();
        myCollider = GetComponent<Collider>();
        myRenderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(ToLookFor == null) { return; }
        if (colliderToLookFor.bounds.Intersects(myCollider.bounds))
        {
            myRenderer.material.color = Color.green;
        }else
        {
            myRenderer.material.color = Color.red;
        }
	}
}
