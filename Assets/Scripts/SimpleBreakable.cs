using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SimpleBreakable : BreakableObject {
    public Shader DeathVFXShader;
    public float DisableCollisionTimeout = 1f;
    public GameObject[] ObjsToAddRBsOnDeath;
    public AudioClip[] BurnSFXs;
    private Renderer renderer;
    private AudioSource audioSource;

    void Start () {
        renderer = GetComponent<Renderer> ();
        audioSource = GetComponent<AudioSource>();
    }

    // protected override void OnDamaged<RaycastHit> (RaycastHit hit) {

    // }

    protected IEnumerator DeathVFX (Material mat) {
        float startTime = Time.time;
        float endTime = Time.time + DestroyDelay;

        while (endTime > startTime) {
            mat.SetFloat (
                "Vector1_652048FC",
                (Time.time - startTime) / (endTime - startTime)
            );
            yield return null;
        }
    }

    private void DisableCollision(){
        gameObject.GetComponent<Collider>().enabled = false;
    }

    protected override void OnDeath () {
        foreach (Material mat in renderer.materials) {
            Texture texture = mat.mainTexture;
            // Color color = mat.GetColor("Color_EBBC19D4");
            mat.shader = DeathVFXShader;
            mat.SetTexture ("Texture2D_4ED82883", texture);
            // mat.SetColor("Color_EDB0A47E", color);
            StartCoroutine (DeathVFX (mat));
        }
        foreach (GameObject obj in ObjsToAddRBsOnDeath) {
            obj.AddComponent<Rigidbody>();
        }
        Invoke("DisableCollision", DisableCollisionTimeout);

        audioSource.clip = BurnSFXs[Random.Range(0,BurnSFXs.Length)];
        audioSource.Play();
    }
}