using Interactables;
using UnityEngine;
using UnityEngine.Pool;

public class CustomUnityPool
{
    private ObjectPool<Interactable> _pool;

    private Interactable _prefab;

    public CustomUnityPool(Interactable prefab, int prewarmObjectsCount)
    {
        _prefab = prefab;
        _pool = new ObjectPool<Interactable>(OnCreateInteractable, OnGetInteractable, OnRelease, OnInteractableDestroy, false,
            prewarmObjectsCount);
    }

    public Interactable Get()
    {
        return _pool.Get();
    }

    public void Release(Interactable obj)
    {
        _pool.Release(obj);
    }

    private void OnInteractableDestroy(Interactable obj)
    {
       GameObject.Destroy(obj);
    }

    private void OnRelease(Interactable obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGetInteractable(Interactable obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Interactable OnCreateInteractable()
    {
        return GameObject.Instantiate(_prefab);
    }
}