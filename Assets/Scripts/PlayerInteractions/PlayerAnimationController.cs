using DG.Tweening;
using UnityEngine;

namespace PlayerInteractions
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer renderer;
        private Sequence _sequence;
        public void Dodged()
        {
            Sequence s = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x - 0.15f, 0.15f))
                .Append(transform.DOMoveX(transform.position.x + 0.1f, 0.15f));
        }

        public void AttackSword()
        {
            _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x + 1f, 0.15f))
                .AppendCallback(() => animator.Play("Attack"+ Random.Range(0,4)))
                .AppendInterval(1f)
                .Append(transform.DOMoveX(transform.position.x, 0.15f));
            
        }

        public void AttackBow()
        {
            _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x + 1f, 0.15f))
                .AppendCallback(() =>  animator.Play("Shoot"))
                .AppendInterval(0.7f)
                .Append(transform.DOMoveX(transform.position.x, 0.15f));
        
        }

        public void GetHit(float dmg)
        {
            animator.Play("Hit");
        }

        public void Die()
        {
            animator.Play("Death");
        }
    }
}