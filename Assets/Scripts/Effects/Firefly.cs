using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    /// <summary>
    /// Class that represents a single firefly.
    /// </summary>
    public class Firefly : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Invoke("PlayAnimation", Random.Range(0,15f));
        }

        /// <summary>
        /// Starts the firefly animation.
        /// </summary>
        private void PlayAnimation()
        {
            _animator.Play("Firefly");
        }
    }
}
