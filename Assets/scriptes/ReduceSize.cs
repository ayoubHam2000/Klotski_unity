
using UnityEngine;

public class ReduceSize : MonoBehaviour
{
    private Setting setting = null;
    private float theAmountOfSize = 0;
    void Start()
    {
        setting = FindObjectOfType<Setting>();
        theAmountOfSize = setting.reduceSizeAmount;
        reduceTheSize();
    }

    private void reduceTheSize()
    {
        Vector3 oldScale = transform.localScale;
        oldScale.x -= theAmountOfSize;
        oldScale.y -= theAmountOfSize;
        oldScale.z -= 0;
        transform.localScale = new Vector3(oldScale.x, oldScale.y, oldScale.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
