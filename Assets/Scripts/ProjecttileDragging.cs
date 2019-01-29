using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileDragging : MonoBehaviour
{
    public LineRenderer catLineFront;
    public LineRenderer catLineBack;

    public float maxStretch = 3.0f;

    SpringJoint2D spring;

    Vector2 prevVelocity;

    bool clickOn;

    Ray leftCatToProjectile;
    Ray rayToMouse;

    float circleRadius;
    float maxStretchSqr;

    Transform catTransform;

    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        catTransform = spring.connectedBody.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxStretchSqr = maxStretch * maxStretch;

        LineRendererSetup();
        rayToMouse = new Ray(catTransform.position, Vector3.zero);
        leftCatToProjectile = new Ray(catLineFront.transform.position, Vector3.zero);

        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circle.radius;
    }

    // Update is called once per frame
    void Update()
    {

        if (clickOn)
        {
            Dragging();
        }

        if (spring != null)
        {
            if (!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
            {
                Destroy(spring);
                GetComponent<Rigidbody2D>().velocity = prevVelocity;
            }
        }
        else if(catLineFront.enabled)
        {
            catLineFront.enabled = false;
            catLineBack.enabled = false;
        }

        LineRendererUpdate();
        prevVelocity = GetComponent<Rigidbody2D>().velocity;

    }

    private void OnMouseDown()
    {
        clickOn = true;


    }

    private void OnMouseUp()
    {

        GetComponent<Rigidbody2D>().isKinematic = false;
        clickOn = false;

    }

    void Dragging()
    {

        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catToMouse = mouseWorldPoint - catTransform.position;

        if (catToMouse.sqrMagnitude > maxStretchSqr)
        {

            rayToMouse.direction = catToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);

        }


        mouseWorldPoint.z = 0;
        transform.position = mouseWorldPoint;

    }

    void LineRendererSetup()
    {

        catLineFront.SetPosition(0, catLineFront.transform.position);
        catLineBack.SetPosition(0, catLineBack.transform.position);

        catLineFront.sortingLayerName = "ForeGround";
        catLineBack.sortingLayerName = "ForeFround";

        catLineFront.sortingOrder = 3;
        catLineBack.sortingOrder = 1;
    }


    void LineRendererUpdate()
    {

        Vector2 catToProjectile = transform.position - catLineFront.transform.position;
        leftCatToProjectile.direction = catToProjectile;
        Vector3 holdPoint = leftCatToProjectile.GetPoint(catToProjectile.magnitude + circleRadius);

        catLineFront.SetPosition(1, holdPoint);
        catLineBack.SetPosition(1, holdPoint);

    }
}

