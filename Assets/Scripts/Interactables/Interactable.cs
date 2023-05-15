using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Объекты с которыми может взаимодействовать Player
    /// Астероиды, бонусы, аптечки, поля замедления и тд
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        protected abstract void Interact();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                Interact();
                Dispose();
            }
        }

        public void Dispose()
        {
            EventBus.Instance.DisposeInteractable(this);
        }
    }
}