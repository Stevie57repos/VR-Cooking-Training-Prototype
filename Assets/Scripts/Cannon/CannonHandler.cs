using System.Collections.Generic;
using UnityEngine;

public class CannonHandler : MonoBehaviour
{
    [Header("Cannon Handles")]
    [SerializeField] private GameObject _leverX;
    [SerializeField] private GameObject _leverZ;
    private float _leverMinRange = -50f;
    private float _cannonStartRotation = 45f;

    [Header("Projectile Settings")]
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float speed;
    public Queue<GameObject> ProjectileQueue = new Queue<GameObject>();

    [Header("Cannon Visual Aim Assist")]
    public LineRenderer CannonAimAssist;
    public GameObject StartLine;
    public GameObject EndLine;

    [Header("Debugging/Testing ")]
    public bool isTestFiring = false;
    public GameObject sandwhichTestFiring;
    
    [Header("Event Channel")]
    [SerializeField] private GameManagerEventChannelSO fireCanon;
    [SerializeField] private GameManagerEventChannelSO levelLoad;

    [Header("Audio")]
    [SerializeField] private AudioSource _canonAudioSource;
    [SerializeField] private AudioClip _ExplosionClip;


    private void Awake()
    {
        _leverX.transform.rotation = Quaternion.identity;
        if(CannonAimAssist == null)
            CannonAimAssist = GetComponentInChildren<LineRenderer>();
    }
    private void OnEnable()
    {
        fireCanon.GameManagerEvent += LaunchProjectile;
        levelLoad.GameManagerEvent += ClearCanonQueue;
    }



    private void OnDisable()
    {
        fireCanon.GameManagerEvent -= LaunchProjectile;
        levelLoad.GameManagerEvent -= ClearCanonQueue;
    }

    private void ClearCanonQueue()
    {
        ProjectileQueue.Clear();
    }

    private void Start()
    {
        CannonAimAssist.enabled = false;
        if (isTestFiring)
        {
            AimAssist();
        }
    }
    public void AddToQueue(GameObject projectile)
    {
        ProjectileQueue.Enqueue(projectile);
        projectile.SetActive(false);
    }

    void AimAssist()
    {
        CannonAimAssist.enabled = true;
        Vector3 startPos = _spawnLocation.transform.position;
        Vector3 endPos = EndLine.transform.position;
        Vector3 direction = endPos - startPos;

        CannonAimAssist.SetPosition(0, startPos);
        if (Physics.Raycast(startPos, direction, out RaycastHit hit))
        {
            if (hit.collider)
            {
                CannonAimAssist.SetPosition(1, hit.point);
            }
        }
        else
        {
            CannonAimAssist.SetPosition(1, endPos);
        }
    }
    void Update()
    {       
        UpdateCannonRotation();
    }
    private void UpdateCannonRotation()
    {
        AimAssist();
        var cannonRotationX = CheckLeverRotation(_leverX);
        var cannonRotationZ = CheckLeverRotation(_leverZ);   
        Vector3 calculateAngle = new Vector3(UpdateCanonRotationX(cannonRotationX), UpdateCanonRotationZ(cannonRotationZ), 0);
        Quaternion newAngle = Quaternion.Euler(calculateAngle);
        transform.rotation = newAngle;
    }
    Quaternion CheckLeverRotation(GameObject lever)
    {
        Quaternion CurrRotation = lever.transform.rotation;
        return CurrRotation;
    }
    float UpdateCanonRotationX(Quaternion angle)
    {
        Vector3 currAngle = angle.eulerAngles;
        if (currAngle.x >= 0 && currAngle.x < 300)
        {
            float leverPercentage = Mathf.Abs(currAngle.x / _leverMinRange);
            float angleX = leverPercentage * _cannonStartRotation + 45;
            return angleX;
        }
        else
        {
            var leverPercentage = (currAngle.x - 310f) / 50f;
            float angleX = leverPercentage * _cannonStartRotation;
            return angleX;
        }
    }
    float UpdateCanonRotationZ(Quaternion angle)
    {
        Vector3 currAngle = angle.eulerAngles;
        if (currAngle.z >= 0 && currAngle.z < 300)
        {
            var leverPercentage = (currAngle.z / _leverMinRange);
            float angleY = leverPercentage * _cannonStartRotation;
            return angleY;
        }
        else
        {
            var leverPercentage = (1 - ((currAngle.z - 310f) / 50f));
            float angleY = leverPercentage * _cannonStartRotation;
            return angleY;
        }
    }
    public void LaunchProjectile()
    {
        if (ProjectileQueue.Count != 0)
        {
            GameObject projectileGO = ProjectileQueue.Dequeue();
            var rb = projectileGO.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;

            projectileGO.transform.position = _spawnLocation.transform.position;
            projectileGO.SetActive(true);

            Vector3 startPos = projectileGO.transform.position;
            Vector3 endPos = EndLine.transform.position;
            Vector3 direction = endPos - startPos;
            rb.AddForce(direction * speed);

            _canonAudioSource.PlayOneShot(_ExplosionClip);
        }
        else
            Debug.Log("Projectile Queue is empty");   

        if(ProjectileQueue.Count == 0)
        {
            CannonAimAssist.enabled = false;
        }
        // for debugging and testing cannon
        if (isTestFiring)
        {
            GameObject projectileGO = Instantiate(sandwhichTestFiring);
            projectileGO.SetActive(true);
            projectileGO.transform.position = _spawnLocation.transform.position;
            var rb = projectileGO.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(transform.up * speed);
        }
    }
}
