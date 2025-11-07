using UnityEngine;

public enum EnemyType
{
    humanoid,
    tree
}
[RequireComponent(typeof(HealthController))]
public class Enemy : MonoBehaviour
{
    public EnemyType type;

    public float _baseSpeed;
    public float BaseSpeed => _baseSpeed;
    public float speed = 2f;
    public Path path { get; set; }
    public float reachEpsilon = 0.05f;
    public int rewardMoney = 5;
    public int baseDamageOnReach = 1;

    private int _index = 0;
    private HealthController _hp;

    private void Awake()
    {
        _hp = GetComponent<HealthController>();
        _hp.onDeath.AddListener(OnDeath);
    }

    private void Start()
    {
        if (path == null) Debug.LogError("Path not set!", this);
        transform.position = path.GetPoint(0);
        _index = 1;

        _baseSpeed = speed;
    }

    private void Update()
    {
        if (path == null || path.Count == 0) return;
        if (_index >= path.Count)
        {
            ReachCapture();
            return;
        }

        Vector3 target = path.GetPoint(_index);
        Vector3 dir = (target - transform.position);
        float dist = dir.magnitude;
        if (dist <= reachEpsilon)
        {
            _index++;
            return;
        }
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    private void ReachCapture()
    {
        Game.I?.LoseLife(baseDamageOnReach);
        Destroy(gameObject);
    }

    private void OnDeath()
    {
        CurrencyManager.Instance.AddCurrency(CurrencyType.Gold, rewardMoney);

        Game.I.State.enemiesAlive--;
        Destroy(gameObject);
    }
}
