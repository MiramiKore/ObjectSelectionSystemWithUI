using UnityEngine;
using UnityEngine.UI;
using Selectable = Gameplay.Controls.Selectable;

namespace Gameplay.UI
{
    public abstract class UIManager : MonoBehaviour
    {
        [SerializeField] protected GameObject uiWindow; // Главное окно UI для отображения информации

        [SerializeField] protected float distanceUIFromBuilding = 200; // Расстояние между интерфейсом и объектом

        [SerializeField] protected Button[] buttons; // Массив для кнопок в окне UI

        // Настройка UI в зависимости от выбранного объекта
        protected abstract void SetupUI(Selectable selectable1);
        
        // Настройка слушателей кнопок
        protected abstract void SetupButtonListeners(Selectable selectable);

        // Обновление UI в зависимости от состояния объекта
        protected abstract void UpdateUI(Selectable selectable);

        protected void OpenUI(Selectable selectable)
        {
            uiWindow.SetActive(true);
            MoveUI(selectable.transform.position);
            SetupButtonListeners(selectable);
            SetupUI(selectable);
        }

        protected void CloseUI()
        {
            uiWindow.SetActive(false);
            RemoveButtonListeners();
        }

        // Перемещаем интерфейс к выбранному объекту
        private void MoveUI(Vector3 objectPosition)
        {
            if (Camera.main == null) return;

            var screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
            uiWindow.transform.position = screenPosition + (Vector3.up * distanceUIFromBuilding);
        }

        // Убираем слушателей со всех кнопок
        private void RemoveButtonListeners()
        {
            foreach (var button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}