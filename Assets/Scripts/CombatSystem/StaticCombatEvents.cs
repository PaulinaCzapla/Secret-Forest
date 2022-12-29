using UnityEngine.Events;

namespace CombatSystem
{
    /// <summary>
    /// A static class that contains events used in the combat system.
    /// </summary>
    public static class StaticCombatEvents
    {
        /// <summary>
        /// An event that is invoked when the combat starts.
        /// </summary>
        private static UnityEvent<Enemy> onCombatStarted = new UnityEvent<Enemy>();
        /// <summary>
        /// An event that is invoked when the combat ends.
        /// </summary>
        private static UnityEvent onCombatEnded = new UnityEvent();
        /// <summary>
        /// An event that enables/disables combat UI.
        /// </summary>
        private static UnityEvent<bool> onToggleCombatUI = new UnityEvent<bool>();
        /// <summary>
        /// An event that enables/disablet combat buttons UI.
        /// </summary>
        private static UnityEvent<bool> onToggleCombatButtonsUI = new UnityEvent<bool>();
        /// <summary>
        /// An event that is invoked to update enemy's current and maximum health value.
        /// </summary>
        private static UnityEvent<float,float> onUpdateEnemyHealthUI = new UnityEvent<float,float>();
        /// <summary>
        /// An event that is invoked when the player attacks with a sword.
        /// </summary>
        private static UnityEvent onPlayerSwordAttack = new UnityEvent();
        /// <summary>
        /// An event that is invoked when the player attacks with a bow.
        /// </summary>
        private static UnityEvent onPlayerBowAttack = new UnityEvent();

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
        
        public static void SubscribeToToggleCombatUI(UnityAction<bool> subscriber) =>
            onToggleCombatUI.AddListener(subscriber);
        public static void UnsubscribeFromToggleCombatUI(UnityAction<bool> subscriber) =>
            onToggleCombatUI.RemoveListener(subscriber);
        public static void InvokeToggleCombatUI(bool isOn) => onToggleCombatUI.Invoke(isOn);
        
        public static void SubscribeToUpdateEnemyHealthUI (UnityAction<float, float> subscriber) =>
            onUpdateEnemyHealthUI .AddListener(subscriber);
        public static void UnsubscribeFromUpdateEnemyHealthUI (UnityAction<float, float> subscriber) =>
            onUpdateEnemyHealthUI .RemoveListener(subscriber);
        public static void InvokeUpdateEnemyHealthUI (float currentHealth, float maxHealth) => onUpdateEnemyHealthUI .Invoke(currentHealth, maxHealth);
        
        public static void SubscribeToPlayerSwordAttack (UnityAction subscriber) =>
            onPlayerSwordAttack.AddListener(subscriber);
        public static void UnsubscribeFromPlayerSwordAttack (UnityAction subscriber) =>
            onPlayerSwordAttack.RemoveListener(subscriber);
        public static void InvokePlayerSwordAttack() => onPlayerSwordAttack?.Invoke();
        
        public static void SubscribeToPlayerBowAttack(UnityAction subscriber) =>
            onPlayerBowAttack.AddListener(subscriber);
        public static void UnsubscribeFromPlayerBowAttack(UnityAction subscriber) =>
            onPlayerBowAttack.RemoveListener(subscriber);
        public static void InvokePlayerBowAttack() => onPlayerBowAttack?.Invoke();
        
        public static void SubscribeToToggleCombatButtonsUI(UnityAction<bool> subscriber) =>
            onToggleCombatButtonsUI.AddListener(subscriber);
        public static void UnsubscribeFromToggleCombatButtonsUI(UnityAction<bool> subscriber) =>
            onToggleCombatButtonsUI.RemoveListener(subscriber);
        public static void InvokeToggleCombatButtonsUI(bool isOn) => onToggleCombatButtonsUI.Invoke(isOn);
    }
}