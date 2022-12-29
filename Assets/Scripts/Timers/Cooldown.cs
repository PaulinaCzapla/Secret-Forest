using UnityEngine;

namespace Timers
{
    /// <summary>
    /// A class that implements cooldown functionality.
    /// </summary>
    public class Cooldown
    {
        public bool CooldownEnded => IsCooldownEnded();

        private readonly float _cooldownTime;
        private float _nextFireTime;

        public Cooldown(float cooldownTime, bool startOnInit = true)
        {
            _cooldownTime = cooldownTime;

            if (startOnInit)
                StartCooldown();
        }
        /// <summary>
        /// Starts the cooldown by setting the next time.
        /// </summary>
        public void StartCooldown()
        {
            _nextFireTime = Time.time + _cooldownTime;
        }
        
        /// <returns> Returns true when current time is bigger than next time, otherwise returns false. </returns>
        private bool IsCooldownEnded()
        {
            return Time.time > _nextFireTime;
        }
    }
}