using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 1;
    public Transform[] waypoints;

    public int currentWayPointIndex = 0;
    void Update()
    {
        if(waypoints == null || waypoints.Length == 0)return;

        Transform target = waypoints[currentWayPointIndex];
        Vector3 dir = (target.position - transform.position).normalized;
        
        transform.position += dir * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWayPointIndex++;

            if(currentWayPointIndex >= waypoints.Length)
            {
                HealthManager.instance.UpdateHealth(-1); // Decrease health by 1 when enemy reaches the end
                Destroy(gameObject); // Enemy reached the end
            }
        }
    }
}
