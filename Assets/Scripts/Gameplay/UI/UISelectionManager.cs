using Gameplay.Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.UI
{
    public class UISelectionManager : MonoBehaviour
    {
        // Событие вызываемое при выборе объектов с тегом "Construction"
        [HideInInspector] public UnityEvent<Selectable> onConstructionSelected = new UnityEvent<Selectable>();
        
        private SelectionSystem _selectionSystem;

        private void Awake()
        {
            _selectionSystem = FindObjectOfType<SelectionSystem>();
        }
        
        private void OnEnable()
        {
            _selectionSystem.objectSelected.AddListener(UIObjectSelector);
        }

        private void OnDisable()
        {
            _selectionSystem.objectSelected.RemoveAllListeners();
        }

        // Управляем интрфейсом для выбранного объекта
        private void UIObjectSelector(Selectable selectable)
        {
            if (selectable.CompareTag("Construction"))
            {
                onConstructionSelected.Invoke(selectable);
            }
        }
    }
}