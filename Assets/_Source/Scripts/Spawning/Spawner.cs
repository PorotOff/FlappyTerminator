using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPooledObject<T>
{
    [Header("Spawner settings")]
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _prefabsContainer;

    protected IObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(OnPoolCreate, OnPoolGet, OnPoolRelease, OnPoolDestroy);
    }

    public virtual T Spawn()
    {
        T spawnable = Pool.Get();
        spawnable.Destroyed += OnPoolObjectDestroyed;

        return spawnable;
    }

    protected virtual void OnPoolObjectDestroyed(T pooledObject)
    {
        pooledObject.Destroyed -= OnPoolObjectDestroyed;
        Pool.Release(pooledObject);
    }

    private T OnPoolCreate()
    {
        T spawnable = Instantiate(_prefab, _prefabsContainer);

        return spawnable;
    }

    private void OnPoolGet(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnPoolRelease(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnPoolDestroy(T pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}