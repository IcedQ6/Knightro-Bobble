using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject user;

    

    void Start()
    {
        Instantiate(user, transform.position, Quaternion.identity);
        
    }

    
    void Update()
    {
        
    }

    


}
