using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionClip;
    void Start()
    {
        AudioManager._instance.PlaySound(explosionClip);
        Destroy(gameObject, 0.3f);
    }
}
