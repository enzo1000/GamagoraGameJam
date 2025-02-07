using UnityEngine;
using System.Collections.Generic;

public class Draw : MonoBehaviour
{
    
    public Camera cam;

    public GameObject brush;
    private GameObject brushInstance;
    private LineRenderer brushLR;
    private Vector2 lastMousePos;
    private List<Vector2> drawPoints;

    private void Start()
    {
        drawPoints = new();
    }

    private void Update()
    {
        Drawing();
    }

    private void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos != lastMousePos)
            {
                AddAPoint(mousePos);
                lastMousePos = mousePos;
            }
        }
        else
        {
            Destroy(brushInstance);
        }
    }

    /// <summary>
    /// Fonction dédiée à la création du pinceau et donc au commencement du tracé
    /// </summary>
    private void CreateBrush()
    {
        brushInstance = Instantiate(brush);
        brushLR = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Size du brush à 2 donc deux points à l'initialisation
        brushLR.SetPosition(0, mousePos);
        brushLR.SetPosition(1, mousePos);
    }

    /// <summary>
    /// Assure la continuité du tracé en stockant ses pts dans un tableau
    /// </summary>
    /// <param name="pointPos"></param>
    private void AddAPoint(Vector2 pointPos)
    {
        brushLR.positionCount++;
        int positionIndex = brushLR.positionCount - 1;
        brushLR.SetPosition(positionIndex, pointPos);
        
        //Stocke les points du tracé dans le tableau drawPoints
        drawPoints.Add(pointPos);
    }

    /// <summary>
    /// Process tout les points stockés dans la liste et essaye de définir un point central au tracé
    /// </summary>
    private void averageCenterEnjoyer()
    {

    }
}
