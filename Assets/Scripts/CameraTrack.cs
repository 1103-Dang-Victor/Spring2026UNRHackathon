using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform target;
    public Transform cameraTarget;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
        cameraTarget.position = smoothPosition;
    }

}
