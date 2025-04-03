using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chime : MonoBehaviour
{
    public bool isSyntony;
    public int id;
    public List<Chime> neighbors = new List<Chime>();
    [SerializeField] private GameObject Wave;
    [HideInInspector] public GameObject Parent;

    public void UpdateVisualState()
    {
        Wave.SetActive(isSyntony);
    }


    private void Update()
    {
        HandleClickDetection();
        
    }



    void HandleClickDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Chime clickedChime = hit.collider.GetComponentInParent<Chime>();

                if (clickedChime != null)
                {
                    ToggleSyntony(clickedChime);
                    Parent.GetComponent<ChimeManager>().CheckFinish();
                }
            }
        }

        void ToggleSyntony(Chime baseChime)
        {
            baseChime.isSyntony = !baseChime.isSyntony;
            baseChime.UpdateVisualState();
            foreach (Chime neighbor in baseChime.neighbors)
            {
                if (neighbor != null)
                {
                    neighbor.isSyntony = !neighbor.isSyntony;
                    neighbor.UpdateVisualState();
                }
            }
        }
    }
}
