using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControler : MonoBehaviour
{
    public float rotation = 50.0f;
    public float speed = 25.0f;
    private float moveSpeed = 45.0f;
    public float zRange = 25.0f;

    public float maxRayDistance = 100f;
    public Color rayColor = Color.red;

    public Vector3 targetMovePosition = new Vector3(-34, 0, 27);

    public GameObject newPlayer;
    public GameObject bucketEmpty;
    public GameObject bucketFull;

    private float horizontalInput;
    private float verticalInput;
    private float currentZPos;

    void Start()
    {
        currentZPos = transform.position.z;
        newPlayer.SetActive(false);
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
            if (hit.transform.CompareTag("Fish"))
            {
                hit.transform.tag = "Move";
                StartCoroutine(MoveToTarget(hit.transform.gameObject));
                ScoreManager.instance.AddScore(10);

            }
            else if (hit.transform.CompareTag("Anchovy"))
            {
                StartCoroutine(MoveNewPlayer());
                StartCoroutine(MoveCurrentPlayerBack());
                (this.gameObject.GetComponent("PlayerControler") as MonoBehaviour).enabled = false;
            }
        }
    }
    //Move Fish to bucket
    IEnumerator MoveToTarget(GameObject hit)
    {
        (hit.GetComponent("FishWiggle") as MonoBehaviour).enabled = false;
        while (hit.transform.position != targetMovePosition)
        {
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, targetMovePosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        if(ScoreManager.instance.getScore()<11)
        {
            Instantiate(bucketFull, bucketEmpty.transform.position, bucketFull.transform.rotation);
            Destroy(bucketEmpty);
        }
        Destroy(hit);
    }
    //Move PowerfulPlayer to player Position
    IEnumerator MoveNewPlayer()
    {
        newPlayer.SetActive(true);
        Vector3 target = transform.position;
        while (newPlayer.transform.position != target)
        {
            newPlayer.transform.position = Vector3.MoveTowards(newPlayer.transform.position, target, 30 * Time.deltaTime);
            PowerfulFishermanController.instance.setCurrentZPos(newPlayer.transform.position.z);

            yield return null;
        }
    }
    //Move Player back for powerful player
    IEnumerator MoveCurrentPlayerBack()
    {
        Vector3 p = new Vector3(transform.position.x-10, transform.position.y, transform.position.z);
        while (transform.position != p )
        {
            transform.position = Vector3.MoveTowards(transform.position, p, 10 * Time.deltaTime);
            yield return null;
        }
    }
}