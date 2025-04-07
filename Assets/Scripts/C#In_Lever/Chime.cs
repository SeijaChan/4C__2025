using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chime : MonoBehaviour
{
    public bool isSyntony;
    public int id;
    public List<Chime> neighbors = new List<Chime>();
    [SerializeField] private GameObject Wave;
    public void UpdateVisualState()
    {
        Wave.SetActive(isSyntony);
    }

    public void ToggleSyntony(Chime baseChime)
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
