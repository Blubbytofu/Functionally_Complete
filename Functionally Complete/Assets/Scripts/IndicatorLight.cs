using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    [SerializeField] protected Material offMat;
    [SerializeField] protected Material onMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    private bool state;

    public void ChangeMat()
    {
        this.state = !this.state;
        spriteRenderer.material = this.state ? onMat : offMat;
    }

    public void ChangeMat(bool state)
    {
        this.state = state;
        spriteRenderer.material = state ? onMat : offMat;
    }
}
