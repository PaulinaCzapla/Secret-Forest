using System;
using System.Collections.Generic;
using DG.Tweening;
using LevelGenerating;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace Effects
{
    public class Fireflies : MonoBehaviour
    {
        [SerializeField] private Collider2D area;
        [SerializeField] private GameObject firefly;
        [Range(0, 50)] [SerializeField] private int amount;
        [SerializeField] private float speed = 0.4f;
        [SerializeField] private float movementOffset = 0.3f;

        private Vector3 newPos;
        private float distance;
        private List<Sequence> _sequences = new List<Sequence>();
        private List<GameObject> _fireflies = new List<GameObject>();


        private void Awake()
        {
            for (int i = 0; i < amount; i++)
            {
                var newFirefly = Instantiate(firefly, transform);
                  newFirefly.transform.position =  new Vector3(
                    Random.Range(area.bounds.min.x, area.bounds.max.x),
                    Random.Range(area.bounds.min.y, area.bounds.max.y), 0);
                  
                _fireflies.Add(newFirefly);
                newPos = new Vector3(
                    Random.Range(area.bounds.min.x, area.bounds.max.x),
                    Random.Range(area.bounds.min.y, area.bounds.max.y), 0);

                // MoveFirefly(newFirefly);
            }
        }

        private void OnEnable()
        {
            
            EnableFireflies();
            LevelGenerator.OnLevelGenerated += ResetFireflies;
        }

        private void ResetFireflies()
        {
            DisableFireflies();
            EnableFireflies();
        }

        private void DisableFireflies()
        {
            Debug.Log("Disable");
            if (_sequences != null)
                foreach (var sequence in _sequences)
                {
                    sequence.Kill();
                }

            foreach (var vfirefly in _fireflies)
            {
                vfirefly.SetActive(false);
            }
        }


        private void EnableFireflies()
        {
            foreach (var vfirefly in _fireflies)
            {
                vfirefly.transform.position = new Vector3(
                    Random.Range(area.bounds.min.x, area.bounds.max.x),
                    Random.Range(area.bounds.min.y, area.bounds.max.y), 0);
                newPos = new Vector3(
                    Random.Range(area.bounds.min.x, area.bounds.max.x),
                    Random.Range(area.bounds.min.y, area.bounds.max.y), 0);
                vfirefly.SetActive(true);
                MoveFirefly(vfirefly);
            }
        }

        private void Update()
        {
            if (_fireflies != null && (_fireflies[0].transform.position.x < area.bounds.min.x ||
                                       _fireflies[0].transform.position.x > area.bounds.max.x ||
                                       _fireflies[0].transform.position.y < area.bounds.min.y ||
                                       _fireflies[0].transform.position.y > area.bounds.max.y))
            {
                ResetFireflies();
            }
        }

        private void OnDisable()
        {
            LevelGenerator.OnLevelGenerated -= ResetFireflies;

            DisableFireflies();
        }

        private void MoveFirefly(GameObject newFirefly)
        {
            Sequence sequence = DOTween.Sequence();
            _sequences.Add(sequence);

            sequence.Append(newFirefly.transform.DOMove(newPos, Mathf.Abs(distance) / speed).SetEase(Ease.Flash))
                .AppendCallback(() => newPos =
                    new Vector3(
                        Mathf.Clamp(Random.Range(area.bounds.min.x, area.bounds.max.x),
                            newFirefly.transform.position.x - movementOffset,
                            newFirefly.transform.position.x + movementOffset),
                        Mathf.Clamp(Random.Range(area.bounds.min.y, area.bounds.max.y),
                            newFirefly.transform.position.y - movementOffset,
                            newFirefly.transform.position.y + movementOffset), 0)).AppendCallback(() =>
                    distance = Vector2.Distance(newFirefly.transform.position, newPos))
                .OnComplete(() => MoveFirefly(newFirefly));
        }
    }
}