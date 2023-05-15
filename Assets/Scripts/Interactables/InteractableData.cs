using Examples.VerticalScrollerExample;
using UnityEngine;

[System.Serializable]
public class InteractableData
{
   [SerializeField] private Interactable _interactablePrefab;
   [SerializeField] private float _startCooldown;
   [SerializeField] private float _endCooldown;
   [SerializeField] private float prewarmTime;

   
   /// <summary>
   /// Ссылка на монобех префаб, который мы будем создавать
   /// </summary>
   public Interactable InteractablePrefab => _interactablePrefab;
   /// <summary>
   /// КД появления объектов в начале уровня
   /// </summary>
   public float StartCooldown => _startCooldown;
   /// <summary>
   /// КД появления объектов в конце уровня
   /// </summary>
   public float EndCooldown => _endCooldown;
   /// <summary>
   /// Время до первого появления объекта
   /// Если prewarm = 10, то первый объект появится через 10 секунд
   /// А дальше по КД
   /// </summary>
   public float PrewarmTime => prewarmTime;

}
