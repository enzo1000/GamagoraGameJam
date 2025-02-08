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
    /// Fonction d�di�e � la cr�ation du pinceau et donc au commencement du trac�
    /// </summary>
    private void CreateBrush()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        brushInstance = Instantiate(brush, mousePos, Quaternion.identity);
        brushLR = brushInstance.GetComponent<LineRenderer>();

        //Size du brush � 2 donc deux points � l'initialisation
        brushLR.SetPosition(0, mousePos);
        brushLR.SetPosition(1, mousePos);
    }

    /// <summary>
    /// Assure la continuit� du trac� en stockant ses pts dans un tableau
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

        //Stocke les points du trac� dans le tableau drawPoints
        drawPoints.Add(pointPos);
        Mesh mesh = new();
        brushLR.BakeMesh(mesh, true);
        brushInstance.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    /// <summary>
    /// Process tout les points stock�s dans la liste et essaye de d�finir un point central au trac�
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
