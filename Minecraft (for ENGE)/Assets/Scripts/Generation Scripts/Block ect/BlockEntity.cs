using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : MonoBehaviour
{
    public float textIndex;
    float rot = 0;

    // Update is called once per frame
    void Update()
    {
        rot -= 50f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f,rot, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
