using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Controls
{
    public class SelectionSystem : MonoBehaviour
    {
        // Событие вызываемое при выборе объекта
        [HideInInspector] public UnityEvent<Selectable> objectSelected = new UnityEvent<Selectable>();
        
        // Событие вызываемое при отмене выбора объекта
        [HideInInspector] public UnityEvent objectDeselected;
        
        // Список всех объектов, которые могут быть выбраны
        private readonly List<Selectable> _selectables = new List<Selectable>();
        
        // Регистрируем объект как "Selectable", добавляя его в список
        public void Register(Selectable selectable)
        {
            _selectables.Add(selectable);
        }
        
        // Убираем объект из списка "Selectable" при его уничтожении
        public void Unregister(Selectable selectable)
        {
            _selectables.Remove(selectable);
        }

        // Снимаем выбор со всех объектов
        public void ClearSelection()
        {
            _selectables.ForEach(s => s.Deselect());
        }

        public void OnSelected(Selectable selectable)
        {
            objectSelected.Invoke(selectable);
        }
        
        public void OnDeselected()
        {
            objectDeselected.Invoke();
        }
    }
}