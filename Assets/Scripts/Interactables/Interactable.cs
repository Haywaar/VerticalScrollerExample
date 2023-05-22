using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Объекты с которыми может взаимодействовать Player
    /// Астероиды, бонусы, аптечки, поля замедления и тд
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        protected EventBus _eventBus;

        protected abstract void Interact();

        protected void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                Interact();
                Dispose();
            }
        }

        private void Dispose()
        {
            _eventBus.Invoke(new DisposeInteractableSignal(this));
        }
    }
}