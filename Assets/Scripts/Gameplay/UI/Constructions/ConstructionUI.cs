using Gameplay.Controls;
using UnityEngine;
using Selectable = Gameplay.Controls.Selectable;

namespace Gameplay.UI.Constructions
{
    public class ConstructionUI : UIManager
    {
        private void Awake()
        {
            Initialize(FindObjectOfType<UISelectionManager>(), 
                FindObjectOfType<SelectionSystem>());
        }

        // Установка зависимостей между UI и системами выбора объектов
        private void Initialize(UISelectionManager uiSelectionManager, SelectionSystem selectionSystem)
        {
            // Подписываемся на событие выбора объекта с тегом "Construction"
            uiSelectionManager.onConstructionSelected.AddListener(OpenUI);
            
            // Подписываемся на событие отмены выбора, чтобы закрыть UI при отмене выбора
            selectionSystem.objectDeselected.AddListener(CloseUI);
        }

        protected override void SetupUI(Selectable selectable1)
        {
            // Логика настройки UI
        }

        protected override void SetupButtonListeners(Selectable selectable)
        {
            buttons[0].onClick.AddListener(UpgradeConstruction);
            buttons[1].onClick.AddListener(RebuildConstruction);
        }

        protected override void UpdateUI(Selectable selectable)
        {
            // Логика обновления UI
        }

        // Улучшения постройки
        private void UpgradeConstruction()
        {
            Debug.Log("Постройка улучшена");
        }

        // Перестройки постройки
        private void RebuildConstruction()
        {
            Debug.Log("Постройка перестроена");
        }
    }
}