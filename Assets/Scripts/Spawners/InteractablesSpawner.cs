using System;
using System.Collections.Generic;
using Examples.VerticalScrollerExample.Scripts.MyPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Examples.VerticalScrollerExample
{
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

        private readonly Dictionary<string, CustomPool<Interactable>> _pools = new Dictionary<string, CustomPool<Interactable>>();

        public void Awake()
        {
            EventBus.Instance.SpawnInteractable += Spawn;
            EventBus.Instance.DisposeInteractable += Dispose;
        }

        private void Spawn(Interactable interactable)
        {
            var pool = GetPool(interactable);
            
            var item = pool.Get();
            item.transform.parent = _parent;
            item.transform.position = RandomizeSpawnPosition();
            EventBus.Instance.InteractableActivated?.Invoke(item);
        }

        private void Dispose(Interactable interactable)
        {
            var pool = GetPool(interactable);
            pool.Release(interactable);
            EventBus.Instance.InteractableDisposed?.Invoke(interactable);
        }

        private CustomPool<Interactable> GetPool(Interactable interactable)
        {
            var objectTypeStr = interactable.GetType().ToString();
            CustomPool<Interactable> pool;
            
            // Такого пула нет - создаём новый пул
            if (!_pools.ContainsKey(objectTypeStr))
            {
                pool = new CustomPool<Interactable>(interactable, 5);
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
            EventBus.Instance.SpawnInteractable -= Spawn;
            EventBus.Instance.DisposeInteractable -= Dispose;
        }
    }
}