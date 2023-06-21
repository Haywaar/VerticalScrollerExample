using Newtonsoft.Json;
using UnityEngine;

namespace Interactables
{
   [System.Serializable]
   public class InteractableSpawnData
   {
      [JsonProperty]
      [SerializeField] private InteractableType interactableType;
      
      [JsonProperty]
      [SerializeField] private int interactableGrade;
      
      [JsonProperty]
      [SerializeField] private float startCooldown;
      
      [JsonProperty]
      [SerializeField] private float endCooldown;
      
      [JsonProperty]
      [SerializeField] private float prewarmTime;

      public InteractableType InteractableType => interactableType;
      public int InteractableGrade => interactableGrade;
      /// <summary>
      /// КД появления объектов в начале уровня
      /// </summary>
      public float StartCooldown => startCooldown;
      /// <summary>
      /// КД появления объектов в конце уровня
      /// </summary>
      public float EndCooldown => endCooldown;
      /// <summary>
      /// Время до первого появления объекта
      /// Если prewarm = 10, то первый объект появится через 10 секунд
      /// А дальше по КД
      /// </summary>
      public float PrewarmTime => prewarmTime;
   }
}
