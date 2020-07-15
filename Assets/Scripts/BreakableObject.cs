using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakableObject : MonoBehaviour {
    public float Value = 0f;
    public float Health = 0f;
    public float DestroyDelay = 0f;
    private bool destroying = false;


    // protected abstract void OnDamaged<T> (T hit);

    public void TakeDamage (RaycastHit hit) {
        Health--;
        // OnDamaged(hit);
        if(Health <= 0){
            if(!destroying){
                Destroyed();
                GameManager.instance.score += Value;
                destroying = true;
            }
        }
    }

    protected abstract void OnDeath ();

    protected void Destroyed () {
        OnDeath();
        Destroy (gameObject, DestroyDelay);
    }
}