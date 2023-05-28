using UnityEngine;

public class FishWiggle : MonoBehaviour
{
    float wiggleSpeed = 3f;  // speed of the wiggle motion
    float wiggleRange = 1.5f;  // maximum range of the wiggle motion
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // calculate a wiggle offset based on sine wave motion
        float wiggleOffset = Mathf.Sin(Time.time * wiggleSpeed) * wiggleRange;
        Vector3 wigglePosition = originalPosition + transform.right * wiggleOffset;

        // set the position of the fish game object with the wiggle offset
        transform.position = wigglePosition;
    }
}
