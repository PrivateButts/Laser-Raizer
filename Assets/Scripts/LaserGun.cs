using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {
    public GameObject LaserStart;
    public LayerMask laserMask;
    public DecalController decalController;
    private GameObject mainCamera;
    private LineRenderer lineRenderer;
    private int BounceLimit = 5;
    private List<Vector3> bounces = new List<Vector3>();

    // public bool applyJudder = false;
    // public float judderStrength = 5f;
    // public float maxJoltTime = 1f;


    private void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        lineRenderer = LaserStart.GetComponent<LineRenderer>();

        // StartCoroutine("Judder");
    }

    void Laser(int numBounces, Vector3 start, Vector3 direction){
        if(numBounces < BounceLimit){
            RaycastHit hit;
            if(Physics.Raycast(start, direction, out hit, Mathf.Infinity, laserMask)){
                // lineRenderer.SetPosition(1, Vector3.forward*hit.distance);
                bounces.Add(hit.point);

                if(hit.transform.gameObject.CompareTag("Breakable")){
                    hit.transform.gameObject.GetComponent<BreakableObject>().TakeDamage(hit);
                }else if(hit.transform.gameObject.CompareTag("Reflective")){
                    Vector3 incomingVector = hit.point - transform.position;
                    Vector3 reflectVector = Vector3.Reflect(incomingVector, hit.normal);

                    // Debug.DrawLine(transform.position, hit.point, Color.red);
                    // Debug.DrawRay(hit.point, reflectVector, Color.green);
                    // Debug.Log(hit.point);

                    Laser(numBounces+1, hit.point, reflectVector);
                }else{
                    decalController.SpawnDecal(hit);
                }
            }else{
                bounces.Add(start+(direction*25));
            }
        }
    }

    // IEnumerable Judder(){
    //     float startTime, endTime;
    //     float judderBounds = judderStrength/2;
    //     Vector3 eularDestination;

    //     while (true){
    //         if(applyJudder){
    //             // Random rotate
    //             startTime = Time.time;
    //             endTime = Random.Range(0, maxJoltTime) + startTime;
    //             eularDestination = new Vector3(
    //                 transform.rotation.eulerAngles.x + Random.Range(-judderBounds, judderBounds),
    //                 transform.rotation.eulerAngles.y + Random.Range(-judderBounds, judderBounds),
    //                 transform.rotation.eulerAngles.z + Random.Range(-judderBounds, judderBounds)
    //             );
    //             while(Time.time < endTime){
    //                 Vector3.Lerp(
    //                     transform.rotation.eulerAngles,
    //                     eularDestination,
    //                     (Time.time - startTime) / (endTime - startTime)
    //                 );
    //                 yield return null;
    //             }

    //             // Back to center
    //             startTime = Time.time;
    //             endTime = Random.Range(0, maxJoltTime) + startTime;
    //             eularDestination = Vector3.zero;
    //             while(Time.time < endTime){
    //                 Vector3.Lerp(
    //                     transform.rotation.eulerAngles,
    //                     eularDestination,
    //                     (Time.time - startTime) / (endTime - startTime)
    //                 );
    //                 yield return null;
    //             }
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update () {
        bounces.Clear();
        bounces.Add(LaserStart.transform.position);
        Laser(0, mainCamera.transform.position, mainCamera.transform.forward);
        lineRenderer.positionCount = bounces.Count;
        lineRenderer.SetPositions(bounces.ToArray());
    }
}