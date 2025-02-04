using UnityEngine;



public class weaponScript : MonoBehaviour
{
    // The weapon has two states: it has hit something or it has not.
    private bool hasHit = false;

    // Since the object can only lose ballons, we only need to switch it to that sprite once it has lost them.
    public Sprite noBallons;

    public double weaponSpeed = 50.0;
    private double timeToLive = 8.0;


    // Update is called once per frame
    void Update()
    {
        moveWeapon();
    }

    void moveWeapon()
    {
        Vector3 newPos = transform.position.x + weaponSpeed * Time.deltaTime;
        transform.position = 
    }
}
