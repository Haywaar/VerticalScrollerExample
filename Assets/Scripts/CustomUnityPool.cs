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
        var obj = _pool.Get();
        return obj;
    }

    public void Release(Interactable obj)
    {
        _pool.Release(obj);
    }

    /// <summary>
    /// КАК уничтожаем объект
    /// </summary>
    /// <param name="obj"></param>
    private void OnInteractableDestroy(Interactable obj)
    {
       GameObject.Destroy(obj);
    }

    /// <summary>
    /// Что делаем с объектом когда его деактивируем
    /// </summary>
    /// <param name="obj"></param>
    private void OnRelease(Interactable obj)
    {
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// Что делаем с объектом когда достаём его из пула
    /// </summary>
    /// <param name="obj"></param>
    private void OnGetInteractable(Interactable obj)
    {
        obj.gameObject.SetActive(true);
    }

    /// <summary>
    /// КАК мы создаём объект
    /// </summary>
    /// <returns></returns>
    private Interactable OnCreateInteractable()
    {
        return GameObject.Instantiate(_prefab);
    }
}