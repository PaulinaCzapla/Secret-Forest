using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class Firefly : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Invoke("PlayAnimation", Random.Range(0,15f));
        }

        private void PlayAnimation()
        {
            _animator.Play("Firefly");
        }
    }
}
