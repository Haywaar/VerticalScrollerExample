using UnityEngine;

namespace Interactables
{
    [System.Serializable]
    public class InteractablePrefabData
    {
        [SerializeField]
        private InteractableType _interactableType;

        [SerializeField]
        private int _interactableGrade;
        
        [SerializeField]
        private Interactable _prefab;
        
        public InteractableType InteractableType => _interactableType;

        public int InteractableGrade => _interactableGrade;

        public Interactable Prefab => _prefab;
    }
}