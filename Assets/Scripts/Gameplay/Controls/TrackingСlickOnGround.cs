using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Controls
{
    public class TrackingСlickOnGround : MonoBehaviour
    {
        private SelectionSystem _selectionSystem;

        private void Awake()
        {
            _selectionSystem = FindObjectOfType<SelectionSystem>();
        }

        // При нажатии на землю закрываем интерфейс
        private void OnMouseDown()
        {
            // Проверяем, не находится ли курсор над интрфейсом
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            _selectionSystem.ClearSelection();
        }
    }
}