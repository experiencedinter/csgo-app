using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundLoop : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    public bool loop = false;
    private Transform target;

    public void StartLoop()
    {
        loop = true;
    }

    public void StopLoop()
    {
        loop = false;
    }

    void Update() 
    {
        if (loop)
        {
            _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
        }
    }
}
