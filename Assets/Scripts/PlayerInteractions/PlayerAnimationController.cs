using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace PlayerInteractions
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer renderer;
        private Sequence _sequence;
        private Vector3 _initialPos;
        
        private void Awake()
        {
            text.gameObject.SetActive(false);
        }

        public void GoToSleep(Vector3 targetPos, float moveTime)
        {
            _initialPos = transform.position;
            
            _sequence = DOTween.Sequence().Append(transform.DOMove(targetPos,  moveTime))
                .Join(renderer.DOFade(0,  moveTime-0.1f));
        }

        public void StopSleeping()
        {
            transform.position = _initialPos;
            renderer.DOFade(1, 0.1f);
        }
        public void Dodged()
        {
            Sequence s = DOTween.Sequence().AppendCallback(() => text.text = "dodged!")
                .Append(transform.DOMoveX(transform.position.x - 0.2f, 0.15f))
                .AppendCallback(() => text.gameObject.SetActive(true))
                .AppendInterval(0.1f)
                .Append(transform.DOMoveX(transform.position.x, 0.15f))
                .AppendCallback(() => text.gameObject.SetActive(false));
        }

        public void AttackSword(bool isCritical)
        {
            if (isCritical)
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x + 1f, 0.15f))
                    .AppendCallback(() => animator.Play("Attack"+ Random.Range(0,4)))
                    .AppendCallback(() => text.text = "<color=#FB1412>critical!</color>")
                    .AppendCallback(() => text.gameObject.SetActive(true))
                    .AppendInterval(1f)
                    .AppendCallback(() => text.gameObject.SetActive(false))
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }
            else
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x + 1f, 0.15f))
                    .AppendCallback(() => animator.Play("Attack"+ Random.Range(0,4)))
                    .AppendInterval(1f)
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }
        }

        public void AttackBow(bool isCritical)
        {
            
            if (isCritical)
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x  + 0.25f, 0.15f))
                    .AppendCallback(() => animator.Play("Shoot"))
                    .AppendCallback(() => text.text = "<color=#FB1412>critical!</color>")
                    .AppendCallback(() => text.gameObject.SetActive(true))
                    .AppendInterval(1f)
                    .AppendCallback(() => text.gameObject.SetActive(false))
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }
            else
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x  + 0.25f, 0.15f))
                    .AppendCallback(() => animator.Play("Shoot"))
                    .AppendInterval(1f)
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }

        }

        public void GetHit(float dmg)
        {
            animator.Play("Hit");
            _sequence = DOTween.Sequence().AppendCallback(() => text.text = "<color=#FB1412>-" + dmg + "</color>")
                .AppendCallback(() => text.gameObject.SetActive(true))
                .AppendInterval(0.4f)
                .AppendCallback(() => text.gameObject.SetActive(false));
        }

        public void Die()
        {
            animator.Play("Death");
        }
    }
}