using UnityEngine;

public class Guardian : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform pivotPOV;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float rotationSpeed = 5f;

    void Update()
    {
        Vector3 dirToPlayer = player.position - pivotPOV.position;

        // Magnitud manual
        float distSqr = dirToPlayer.x * dirToPlayer.x
                      + dirToPlayer.y * dirToPlayer.y
                      + dirToPlayer.z * dirToPlayer.z;

        if (distSqr < detectionRange * detectionRange)
        {
            float dist = Mathf.Sqrt(distSqr);
            Vector3 dirUnit = new Vector3(dirToPlayer.x / dist,
                                          dirToPlayer.y / dist,
                                          dirToPlayer.z / dist);

            float dot = Vector3.Dot(pivotPOV.forward, dirUnit);
            Debug.Log("Dot product: " + dot);

            // Quaternion que representa "mirar hacia dirUnit"
            // FromToRotation: rotación desde el forward actual hasta la dirección objetivo
            Quaternion deltaRot = Quaternion.FromToRotation(pivotPOV.forward, dirUnit);

            // Aplicar suavemente cada frame
            pivotPOV.rotation = Quaternion.Slerp(
                pivotPOV.rotation,
                pivotPOV.rotation * deltaRot,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}