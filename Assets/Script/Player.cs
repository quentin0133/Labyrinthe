using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rg;
    public float movementSpeed = 1f;
    public AudioSource audioSource;

    void FixedUpdate()
    {
        rg.velocity = transform.right * movementSpeed * Input.GetAxis("Vertical");
        Vector2 Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(Target.y - transform.position.y, Target.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0f && !audioSource.isPlaying)
            audioSource.Play();

        if (Input.GetAxis("Vertical") <= 0f)
            audioSource.Stop();
    }
}
