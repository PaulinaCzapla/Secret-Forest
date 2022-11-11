using UnityEngine.Events;

namespace CombatSystem
{
    public static class StaticCombatEvents
    {
        private static UnityEvent<Enemy> onCombatStarted = new UnityEvent<Enemy>();
        private static UnityEvent onCombatEnded = new UnityEvent();
        private static UnityEvent onShowCombatUI = new UnityEvent();
        private static UnityEvent<bool> onSetActiveCombatUI = new UnityEvent<bool>();

        public static void SubscribeToCombatStarted (UnityAction<Enemy> subscriber) =>
            onCombatStarted.AddListener(subscriber);
        public static void UnsubscribeFromCombatStarted (UnityAction<Enemy> subscriber) =>
            onCombatStarted.RemoveListener(subscriber);
        public static void InvokeCombatStarted (Enemy enemy) => onCombatStarted?.Invoke(enemy);
        
        public static void SubscribeToCombatEnded (UnityAction subscriber) =>
            onCombatEnded.AddListener(subscriber);
        public static void UnsubscribeFromCombatEnded  (UnityAction subscriber) =>
            onCombatEnded.RemoveListener(subscriber);
        public static void InvokeCombatEnded () => onCombatEnded?.Invoke();
        
        public static void SubscribeToShowCombatUI (UnityAction subscriber) =>
            onShowCombatUI.AddListener(subscriber);
        public static void UnsubscribeFromShowCombatUI  (UnityAction subscriber) =>
            onShowCombatUI.RemoveListener(subscriber);
        public static void InvokeShowCombatUI() => onShowCombatUI?.Invoke();

        public static void SubscribeToSetActiveCombatUI (UnityAction<bool> subscriber) =>
            onSetActiveCombatUI.AddListener(subscriber);
        public static void UnsubscribeFromSetActiveCombatUI (UnityAction<bool> subscriber) =>
            onSetActiveCombatUI.RemoveListener(subscriber);
        public static void InvokeSetActiveCombatUI (bool active) => onSetActiveCombatUI?.Invoke(active);

    }
}