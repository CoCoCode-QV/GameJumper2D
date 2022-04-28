using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConsts.Platform))
        {
            Destroy(collision.gameObject);
        }
    }
}
