using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public Vector2 grapplePoint;
    public Rigidbody2D player;
    public int grapple_radius;
    public bool boostAvailable;
    private GameObject hitObject;
    private Vector3 offset;

    // Start is called before the first frame update

    void Start()
    {
        _distanceJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0) && )
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        */
        if (!GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>().hasWon) {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                deGrapple();
            }
            if (_distanceJoint.enabled)
            {
                _lineRenderer.SetPosition(1, transform.position);
                _lineRenderer.SetPosition(0, hitObject.transform.position - offset);
                _distanceJoint.connectedAnchor = hitObject.transform.position - offset;
                float x_diff = Mathf.Abs((hitObject.transform.position - offset).x - transform.position.x);
                float y_diff = Mathf.Abs((hitObject.transform.position - offset).y - transform.position.y);
                if (Mathf.Sqrt(x_diff * x_diff + y_diff * y_diff) > grapple_radius)
                {
                    deGrapple();
                }
            }
        }
    }

    public void deGrapple()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Physics2D.Raycast(transform.position, distanceVector.normalized))
        {

            RaycastHit2D _hit = Physics2D.Raycast(transform.position, distanceVector.normalized);
            hitObject = _hit.transform.gameObject;
            if (_hit.transform.gameObject.layer == 6)
            {
                grapplePoint = _hit.point;
                offset = hitObject.transform.position - new Vector3(_hit.point.x, _hit.point.y, 0);
                float x_diff = Mathf.Abs(grapplePoint.x - transform.position.x);
                float y_diff = Mathf.Abs(grapplePoint.y - transform.position.y);
                if (Mathf.Sqrt(x_diff * x_diff + y_diff * y_diff) < grapple_radius) {
                    _lineRenderer.SetPosition(0, grapplePoint);
                    _lineRenderer.SetPosition(1, transform.position);
                    _distanceJoint.connectedAnchor = grapplePoint;
                    _distanceJoint.enabled = true;
                    _lineRenderer.enabled = true;
                    boostAvailable = true;
                }
            }
        }
    }
}