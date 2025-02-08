using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public Camera cam;
    public Image crayBar;
    public Image crayBarRed;
    public GameObject brush;

    [Range(0.001f, 0.1f)] public float crayUsage;
    [Range(0.1f, 1f)] public float crayRecover;

    private GameObject brushInstance;
    private LineRenderer brushLR;
    private Vector3 lastMousePos;
    private List<Vector2> drawPoints;

    private float crayAmount;
    private float initialCrayAmount = 1f;
    private bool _isPlaying = false;
    public bool IsPlaying
    {
        get => _isPlaying; 
        set => _isPlaying = value;
    }

    //Init list at startup
    private void Start()
    {
        drawPoints = new();
        crayAmount = crayBar.fillAmount;
        crayBar.gameObject.GetComponent<Animator>().SetFloat("percent", crayAmount);
    }

    //Draw
    private void Update()
    {
        if (!_isPlaying)
            return;
        Drawing();
    }

    /// <summary>
    /// Fonction de drawing principale
    /// </summary>
    private void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialCrayAmount = crayAmount;
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
        else if (Input.GetMouseButtonUp(0))
        {
            crayAmount = initialCrayAmount;
            crayBar.gameObject.GetComponent<Animator>().SetFloat("percent", crayAmount);
            drawPoints.Clear();
            Destroy(brushInstance);
        }
        //else
           //DestroyTheCrayAndWorld();
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
    /// verifie le croisement avec les precedents points
    /// place le point central du cercle une fois le croisement effectue
    /// </summary>
    /// <param name="pointPos"></param>
    private void AddAPoint(Vector3 pointPos)
    {
        reduceCrayBar();
        if (crayAmount <= 0f)
        {
            return;
        }

        brushLR.positionCount++;
        int positionIndex = brushLR.positionCount - 1;
        brushLR.SetPosition(positionIndex, pointPos);
        
        if (processCrossingGigaChad(pointPos))
        {
            averageCenterEnjoyer();
            //DestroyTheCrayAndWorld();
        }
        
        //Debug purpose
        //GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sph.transform.position = pointPos;
        //sph.transform.localScale *= 0.1f;

        //Stocke les points du tracé dans le tableau drawPoints
        drawPoints.Add(pointPos);
        Mesh mesh = new();
        brushLR.BakeMesh(mesh, true);
        brushInstance.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    /// <summary>
    /// Process tout les points stockés dans la liste et essaye de définir un point central au tracé
    /// ce point est calcule en partant du principe que le joueur trace un cercle
    /// </summary>
    private void averageCenterEnjoyer()
    {
        Vector3 averagePoint = Vector3.zero;
        foreach(Vector3 point in drawPoints)
        {
            averagePoint += point;
        }
        averagePoint /= drawPoints.Count;

        float minScale = processMinDist(averagePoint);
        damageAllEye(minScale, averagePoint);
        drawPoints.Clear();

        //GameObject centerSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //centerSphere.transform.position = averagePoint;
    }

    /// <summary>
    /// Si le nouveauPoint represente un croisement avec les anciens points alors
    /// on return true. Sert a informer de la fin d'un cercle autour d'un oeil.
    /// </summary>
    /// <param name="nouveauPoint"></param>
    /// <returns>bool</returns>
    private bool processCrossingGigaChad(Vector3 nouveauPoint)
    {
        foreach (Vector3 pts in drawPoints)
        {
            if (Vector3.Distance(pts, nouveauPoint) < 0.25f)
            {
                brushLR.positionCount = 0;
                initialCrayAmount = crayAmount;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function dedicated to process le rayon minimal du cercle trace afin de faire des degats aux yeux
    /// </summary>
    /// <param name="averagePoint"></param>
    /// <returns></returns>
    private float processMinDist(Vector3 averagePoint)
    {
        float minDist = float.MaxValue;
        foreach (Vector3 point in drawPoints)
        {
            if (minDist > Vector3.Distance(point, averagePoint))
            {
                minDist = Vector3.Distance(point, averagePoint);
            }
        }
        return minDist;
    }

    private void damageAllEye(float actionrange, Vector3 centerPoint)
    {
        foreach (Transform eye in GameObject.Find("EyeSpawner").transform.GetComponentInChildren<Transform>())
        {
            if (Vector3.Distance(eye.position, centerPoint) < actionrange)
            {
                int compt = 0;
                Transform[] eyePoints = eye.gameObject.transform.GetComponentsInChildren<Transform>();
                foreach (Transform ptn in eyePoints)
                {
                    compt++;
                }
                if (compt >= 5)
                {
                    MusicScript.instance.PlayExorcistSFX();
                    recoverCrayBar(crayRecover);
                    eye.gameObject.GetComponent<Eye>().DoALotOfDamage();
                }
            }
        }
    }

    private void reduceCrayBar()
    {
        crayAmount -= crayUsage;
        if (crayAmount < 0f)
            crayAmount = 0f;
        
        crayBar.gameObject.GetComponent<Animator>().SetFloat("percent", crayAmount);
    }
    private void recoverCrayBar(float recoverAmount)
    {
        crayAmount += recoverAmount;
        if (crayAmount > 1f)
            crayAmount = 1f;
        crayBar.gameObject.GetComponent<Animator>().SetFloat("percent", crayAmount);
    }
}
