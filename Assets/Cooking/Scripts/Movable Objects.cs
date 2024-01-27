using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    /*public Transform correctDropAreaParent;*/ // Set this in the inspector
    //public Transform correctDropArea;

    public GameObject edgeColliderChild;
    public GameObject arrowObject;

    [SerializeField]
    private float jitterAmount = 0.1f;
    [SerializeField]
    private float impulseAmount = 2.5f;
    [SerializeField]
    private float constantImpulseStrength = 1f;
    [SerializeField]
    private Vector2 forceOffset = new Vector2(0.5f, 0f);
    [SerializeField]
    private int objectIndex = 0;
  

    private float screenOffset = 0.1f;
    private bool isDragging = false;
    private bool isInPosition = false;
    private Vector3 offset;
    private float zIndex;
    private Rigidbody2D rb;
    private Rigidbody2D childRB; 
    private Collider2D collider;

    private float edgeColliderBelowRadius = 1.5f;
    // [SerializeField]
    // private float radius;

    private Transform correctDropArea;
    private Vector3 correctDropAreaPosition;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        childRB = edgeColliderChild.GetComponent<Rigidbody2D>();
        childRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.isKinematic = true;



    }

    void Update()
    {
        FindArrowObject();

        //Debug.Log(Vector3.Distance(GetMouseWorldPos(), correctDropAreaPosition));

        if (isDragging)
        {
            MoveObjectAndJitter();
        }

        if (!isInPosition)
        {
            ApplyImpulse(constantImpulseStrength);
        }


        // off-screen functionality 
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        // Check if the object is out of the screen bounds
        if (screenPosition.x < (0 - screenOffset) || screenPosition.x > (1 + screenOffset)
            || screenPosition.y < (0 - screenOffset) || screenPosition.y > (1 + screenOffset))
        {
            Debug.Log("Went off screen");
            DestroyAndRespawn();
        }
    }

    public void MoveObjectAndJitter()
    {
        Vector3 mousePos = GetMouseWorldPos() + offset;
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        // Apply jitter effect
        transform.position += (Vector3)Random.insideUnitCircle * jitterAmount;
    }

    public void FindArrowObject()
    {

        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
        if (arrowObject != null)
        {
            correctDropArea = arrowObject.transform;
            correctDropAreaPosition = correctDropArea.transform.position - new Vector3(0f, Settings.arrowOffset, 0f);
        }
        else
        {
            Debug.Log("No arrow in Scene");
        }

    }



    void OnMouseDown()
    {
        if (!isInPosition)
        {
            zIndex = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;

            // object not affected by physics when dragging
            rb.isKinematic = true;
            edgeColliderChild.SetActive(false);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (Vector3.Distance(GetMouseWorldPos(), correctDropAreaPosition) < 0.5f)
        {
            isInPosition = true;
            // Play animation
            // Fade out
            CompleteStep();
        }

        if (collider is CircleCollider2D)
        {
            CircleCollider2D circleCollider = (CircleCollider2D)collider;
            edgeColliderBelowRadius = circleCollider.radius;
        }

        edgeColliderChild.transform.position = new Vector3(0f, -(edgeColliderBelowRadius + 0.01f), 0f);
        Vector3 currentRotation = edgeColliderChild.transform.eulerAngles;
        currentRotation.z = 0;
        edgeColliderChild.transform.eulerAngles = currentRotation;
        edgeColliderChild.SetActive(true);
     

        if (!isInPosition)
        {
            rb.isKinematic = false;
            ApplyImpulse(impulseAmount);
        }
        else
        {
            ResetRotation();
            if (!SpawnItemManager.Instance.isKnife)
            {
                SpawnItemManager.Instance.DestroyArrowObjectAndLoadNext();
            }
         
        }
 
        
    }

    void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
        transform.position = correctDropAreaPosition;
        //rb.isKinematic = true;
        //rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Static;


    }

    void DestroyAndRespawn()
    {
        SpawnItemManager.Instance.RespawnObject(objectIndex);
        Destroy(gameObject);
    }
    void ApplyImpulse(float impulseAmount)
    {
        // Calculate impulse direction 
        Vector2 impulseDirection = Random.insideUnitCircle.normalized;
        //Vector2 impulseDirection = new Vector2(1.0f, 0f);
        Vector2 forcePoint = rb.position + forceOffset; 

        rb.AddForceAtPosition(impulseDirection * impulseAmount, forcePoint, ForceMode2D.Impulse);
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zIndex;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void CompleteStep()
    {
        // Implement step completion, like playing an animation and fading out
        Debug.Log("Completed Step");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set Gizmo color
        Gizmos.DrawWireSphere(transform.position, forceOffset.x); // Draw a wireframe sphere
    }
}
