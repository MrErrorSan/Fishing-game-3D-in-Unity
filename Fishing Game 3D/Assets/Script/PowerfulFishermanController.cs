using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerfulFishermanController : MonoBehaviour
{
    public static PowerfulFishermanController instance;

    public float rotation = 50.0f;
    public float speed = 25.0f;
    private float itemMoveSpeed = 50.0f;
    public float zRange = 25.0f;

    public float maxRayDistance = 100f;
    public Color rayColor = Color.red;

    public Vector3 targetMovePosition = new Vector3(-34, 0, 27);

    private float horizontalInput;
    private float verticalInput;
    private float currentZPos;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentZPos = transform.position.z;
    }
   public  void setCurrentZPos(float z)
    {
        currentZPos = z;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the object along the line
        currentZPos += verticalInput * speed * Time.deltaTime;
        currentZPos = Mathf.Clamp(currentZPos, -zRange, zRange);
        transform.position = new Vector3(transform.position.x, transform.position.y, currentZPos);

        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up * horizontalInput * rotation * Time.deltaTime);

        // Keep the player within the line boundaries
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        // Draw a ray in front of the object
        Debug.DrawRay(transform.position, transform.forward * maxRayDistance, rayColor);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRayDistance))
        {
            // If the ray hits an object with the "Fish" tag, delete it and increase score
            if (hit.transform.CompareTag("Fish") || hit.transform.CompareTag("Anchovy")|| hit.transform.CompareTag("Move"))
            {
                if(hit.transform.CompareTag("Fish"))
                    ScoreManager.instance.AddScore(10);
                else if (hit.transform.CompareTag("Anchovy"))
                    ScoreManager.instance.AddScore(100);
                hit.transform.tag = "Untagged";
                StartCoroutine(MoveToTarget(hit.transform.gameObject));

            }
        }
    }
    IEnumerator MoveToTarget(GameObject hit)
    {
        (hit.GetComponent("FishWiggle") as MonoBehaviour).enabled = false;

        while (hit.transform.position != targetMovePosition)
        {
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, targetMovePosition, itemMoveSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(hit);
    }
}