using System.Collections;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Controls
{
    public class Selectable : MonoBehaviour
    {
        // Индикатор отображающий, что объект выбран
        [SerializeField] protected GameObject indicator;
        
        // Флаг показывабщий, выбран ли объект в данный момент
        [SerializeField] [ReadOnly] protected bool selected = false;
        
        // Флаг указывающий, можно ли выполнять действия с объектом
        [SerializeField] [ReadOnly] protected bool actionAllowed;
        
        private SelectionSystem _system;

        protected virtual void Start()
        {
            // Находим и регистрируем объект в системе выбора
            _system = FindObjectOfType<SelectionSystem>();
            _system.Register(this);

            Deselect(true);
        }
        
        protected virtual void OnMouseDown()
        {
            // Очищаем предыдущие выборы и выбираем этот объект
            _system.ClearSelection();
            Select();
        }

        protected virtual void OnDestroy()
        {
            // Убираем объект из системы выбора
            _system.Unregister(this);
        }

        // Выбор объекта
        public void Select()
        {
            if (selected) return;
            
            selected = true;

            StartCoroutine(AllowAction());

            // Включаем индикатор, если он установлен
            if (indicator != null)
                indicator.SetActive(true);

            // Уведомляем систему выбора, что объект выбран
            _system.OnSelected(this);

#if UNITY_EDITOR
            // Выделяет объект в редакторе Unity при выборе
            Selection.activeGameObject = gameObject;
#endif
        }

        // Отменяем выбор объекта
        public void Deselect(bool force = false)
        {
            if (selected || force)
            {
                selected = false;
                actionAllowed = false;

                // Отключаем индикатор, если он установлен
                if (indicator != null)
                    indicator.SetActive(false);

                // Уведомляем систему выбора, что объект больше не выбран
                _system.OnDeselected();
            }
        }

        // Делаем задержку перед разрешением действий
        private IEnumerator AllowAction()
        {
            yield return new WaitForSeconds(0.1f);

            actionAllowed = true;
        }
    }
}