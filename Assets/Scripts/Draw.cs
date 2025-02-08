using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class Draw : MonoBehaviour
{
    public Camera cam;

    public GameObject brush;
    private GameObject brushInstance;
    private LineRenderer brushLR;
    private Vector3 lastMousePos;
    private List<Vector2> drawPoints;
    private List<GameObject> debugSphereList;

    private void Start()
    {
        drawPoints = new();
        debugSphereList = new();
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
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (Vector3.Distance(mousePos, lastMousePos) > 0.3f)
            {
                AddAPoint(mousePos);
                lastMousePos = mousePos;
            }
        }
        else
        {
            debugSphereList.Clear();
            drawPoints.Clear();
            Destroy(brushInstance);
        }
    }

    /// <summary>
    /// Fonction dédiée à la création du pinceau et donc au commencement du tracé
    /// </summary>
    private void CreateBrush()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        brushInstance = Instantiate(brush, mousePos, Quaternion.identity);
        brushLR = brushInstance.GetComponent<LineRenderer>();

        //Size du brush à 2 donc deux points à l'initialisation
        brushLR.SetPosition(0, mousePos);
        brushLR.SetPosition(1, mousePos);
    }

    /// <summary>
    /// Assure la continuité du tracé en stockant ses pts dans un tableau
    /// </summary>
    /// <param name="pointPos"></param>
    private void AddAPoint(Vector3 pointPos)
    {
        brushLR.positionCount++;
        int positionIndex = brushLR.positionCount - 1;
        brushLR.SetPosition(positionIndex, pointPos);

        processCrossingGigaChad(pointPos);

        GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = pointPos;
        sph.transform.localScale *= 0.1f;
        debugSphereList.Add(sph);

        //Stocke les points du tracé dans le tableau drawPoints
        drawPoints.Add(pointPos);
        Mesh mesh = new();
        brushLR.BakeMesh(mesh, true);
        brushInstance.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    /// <summary>
    /// Process tout les points stockés dans la liste et essaye de définir un point central au tracé
    /// </summary>
    private void averageCenterEnjoyer()
    {

    }

    private void processCrossingGigaChad(Vector3 nouveauPoint)
    {
        foreach (Vector3 pts in drawPoints)
        {
            if (Vector3.Distance(pts, nouveauPoint) < 0.3f)
            {
                Debug.Log("Ouais les proche le con");
            }
        }
    }
}
