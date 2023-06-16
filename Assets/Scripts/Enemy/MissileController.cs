using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRPlugin;

public class MissileController : MonoBehaviour
{
    public Transform targetTransform;

    public float forwardSpeed = 1f;
    public float maxForwardSpeed;
    public float rotationSpeed = 1f;
    public float maxMoveDistanceDelta = 1f;
    public float distanceFromPlayerToActivate = 65f;
    public float distanceFromStartPointToTurn = 10f;
    [Header("Sounds")]
    public AudioClip firingSound;
    //public AudioClip aliveSound;
    public AudioClip deathSound;

    private bool isAlive = false;
    private bool hasExploded = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private ParticleSystem particleSystem;
    private Rigidbody myRigidbody;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = transform.Find("Graphics/Particles_LazerEnd").GetComponent<ParticleSystem>();

        startPosition = transform.position;
        startRotation = transform.rotation;
        GameManagerChasm.resetEnemyCollisions.AddListener(ResetMissile);
    }

    void ResetMissile() {
        transform.position = startPosition;
        transform.rotation = startRotation;
        myRigidbody.position = startPosition;
        myRigidbody.rotation = startRotation;
        particleSystem.Clear();

        isAlive = false;
        hasExploded = false;
        myRigidbody.velocity = Vector3.zero;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasExploded == true) {
            return;
        }
        if (isAlive == false) {
            if (GameManagerChasm.playerCol.transform.position.z > transform.position.z - distanceFromPlayerToActivate) {
                isAlive = true;
                GameManagerChasm.SpawnLoudAudio(firingSound);
            }

            return;
        }

        //if(audioSource.isPlaying == false) {
        //    audioSource.PlayWebGL(aliveSound);
        //}
        //Vector3 targetDirection = targetTransform.position - transform.position;

        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetTransform.position, rotationSpeed, 0f);

        // let the missile fly for a bit before turning
        if (Vector3.Distance(startPosition, transform.position) < distanceFromStartPointToTurn) {
            Quaternion rotateDestination = Quaternion.LookRotation(targetTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateDestination, rotationSpeed);

            myRigidbody.AddForce(transform.forward * forwardSpeed, ForceMode.Force);
            myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxForwardSpeed);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, maxMoveDistanceDelta);
            transform.LookAt(targetTransform.position);
        }

        if (Vector3.Distance(targetTransform.position, transform.position) < 1f) {
            GameManagerChasm.SpawnLoudAudio(deathSound);
            hasExploded = true;
            GameObject explosion = GameManagerChasm.pool_Explosions.Spawn(transform.position);
            explosion.GetComponent<Animator>().SetTrigger("StartExplosion");
        }
    }
}
