using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// При поступлении сигнала Spawn достаёт нужный объект из пула
/// Или генирирует новый
/// </summary>
public class InteractablesSpawner : MonoBehaviour, IService
{
    [SerializeField] private Transform _parent;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _defaultY;

    public float MinX => _minX;
    public float MaxX => _maxX;

    private readonly Dictionary<string, CustomUnityPool> _pools =
        new Dictionary<string, CustomUnityPool>();

    private EventBus _eventBus;

    public void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Subscribe<DisposeInteractableSignal>(Dispose);
    }

    private void Spawn(SpawnInteractableSignal signal)
    {
        var pool = GetPool(signal.Interactable);

        var item = pool.Get();
        item.transform.parent = _parent;
        item.transform.position = RandomizeSpawnPosition();

        _eventBus.Invoke(new InteractableActivatedSignal(item));
    }

    private void Dispose(DisposeInteractableSignal signal)
    {
        var interactable = signal.Interactable;
        var pool = GetPool(interactable);
        pool.Release(interactable);

        _eventBus.Invoke(new InteractableDisposedSignal(interactable));
    }

    private CustomUnityPool GetPool(Interactable interactable)
    {
        var objectTypeStr = interactable.GetType().ToString();
        CustomUnityPool pool;

        // Такого пула нет - создаём новый пул
        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new CustomUnityPool(interactable, 5);
            _pools.Add(objectTypeStr, pool);
        }
        // Пул есть - возвращаем пул
        else
        {
            pool = _pools[objectTypeStr];
        }

        return pool;
    }

    private Vector3 RandomizeSpawnPosition()
    {
        float x = Random.Range(_minX, _maxX);
        return new Vector3(x, _defaultY, 0);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Unsubscribe<DisposeInteractableSignal>(Dispose);
    }
}