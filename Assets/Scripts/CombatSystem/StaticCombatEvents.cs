using UnityEngine.Events;

namespace CombatSystem
{
    public static class StaticCombatEvents
    {
        private static UnityEvent onCombatStarted = new UnityEvent();
        private static UnityEvent onCombatEnded = new UnityEvent();

        public static void SubscribeToCombatStarted (UnityAction subscriber) =>
            onCombatStarted.AddListener(subscriber);
        public static void UnsubscribeFromCombatStarted (UnityAction subscriber) =>
            onCombatStarted.RemoveListener(subscriber);
        public static void InvokeCombatStarted () => onCombatStarted?.Invoke();
        
        public static void SubscribeToCombatEnded (UnityAction subscriber) =>
            onCombatEnded.AddListener(subscriber);
        public static void UnsubscribeFromCombatEnded  (UnityAction subscriber) =>
            onCombatEnded.RemoveListener(subscriber);
        public static void InvokeCombatEnded () => onCombatStarted?.Invoke();
    }
}