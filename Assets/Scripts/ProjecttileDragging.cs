using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileDragging : MonoBehaviour
{
    public LineRenderer catLineFront;
    public LineRenderer catLineBack;


    bool clickOn;

    // Start is called before the first frame update
    void Start()
    {

        LineRendererSetup();

    }

    // Update is called once per frame
    void Update()
    {

        if (clickOn)
        {
            Dragging();
        }

        LineRendererUpdate();

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

        Vector3 holdPoint = transform.position;

        catLineFront.SetPosition(1, holdPoint);
        catLineBack.SetPosition(1, holdPoint);

    }
}

