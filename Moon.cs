using UnityEngine;

public class Moon : MonoBehaviour
{
    public float gravityRadius = 5f;
    public float gravityStrength = 9.81f;
    public Transform gorillaPlayer;

    private Quaternion originalRotation;
    private bool isInGravityField = false;

    private void Start()
    {
        originalRotation = gorillaPlayer.localRotation;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(gorillaPlayer.position, transform.position);

        if (distance < gravityRadius)
        {
            Vector3 gravityUp = (gorillaPlayer.position - transform.position).normalized;
            Vector3 localUp = gorillaPlayer.up;

            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * gorillaPlayer.localRotation;
            gorillaPlayer.localRotation = Quaternion.Lerp(gorillaPlayer.localRotation, targetRotation, Time.deltaTime * 15f);

            Rigidbody gorillaRigidbody = gorillaPlayer.GetComponent<Rigidbody>();
            float distanceRatio = 1 - (distance / gravityRadius);
            float currentGravityStrength = gravityStrength * distanceRatio;
            gorillaRigidbody.AddForce(-gravityUp * currentGravityStrength, ForceMode.Acceleration);

            isInGravityField = true;
        }
        else
        {
            if (isInGravityField)
            {
                gorillaPlayer.rotation = Quaternion.identity;
            }

            isInGravityField = false;
        }
    }
}
