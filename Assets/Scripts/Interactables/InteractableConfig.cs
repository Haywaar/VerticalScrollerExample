using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactables
{
    [CreateAssetMenu(fileName = "InteractableConfig", menuName = "ScriptableObjects/InteractableConfig", order = 1)]
    public class InteractableConfig : ScriptableObject
    {
        [SerializeField]
        private List<InteractablePrefabData> interactablePrefabsData;

        public Interactable Get(InteractableType type, int grade)
        {
            var interactable =
                interactablePrefabsData.FirstOrDefault(x =>
                    x.InteractableType == type && x.InteractableGrade == grade);
            
            if (interactable == null || interactable.Prefab == null)
            {
                Debug.LogErrorFormat("Cannot find interactable of type {0} and grade {1}", type, grade);
                return null;
            }

            return interactable.Prefab;
        }
    }
}