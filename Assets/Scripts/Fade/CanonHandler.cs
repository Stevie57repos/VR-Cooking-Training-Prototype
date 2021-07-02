using System.Collections.Generic;
using UnityEngine;

public class CanonHandler : MonoBehaviour
{
    [SerializeField] GameObject _leverX;
    [SerializeField] GameObject _leverZ;
    float _leverMinRange = -50f;
    float _cannonStartRotation = 45f;

    [SerializeField] Transform _spawnLocation;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float speed;
    public Queue<GameObject> projectileQueue = new Queue<GameObject>();

    private void Awake()
    {
        _leverX.transform.rotation = Quaternion.identity;
    }

    private void Start()
    {

    }

    public void AddToQueue(GameObject projectile)
    {
        projectileQueue.Enqueue(projectile);
        projectile.SetActive(false);
        LaunchProjectile();
    }

    void Update()
    {       
        UpdateCannonRotation();
    }

    private void UpdateCannonRotation()
    {
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
        if (projectileQueue.Count != 0)
        {
            GameObject projectileGO = projectileQueue.Dequeue();
            projectileGO.SetActive(true);
            projectileGO.transform.position = _spawnLocation.transform.position;
            var rb = projectileGO.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * speed);
        }
        else
            Debug.Log("Projectile Queue is empty");   
    }
}
