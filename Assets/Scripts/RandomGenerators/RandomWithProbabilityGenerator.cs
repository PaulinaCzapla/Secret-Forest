using System;
using Random = UnityEngine.Random;

namespace RandomGenerators
{
    public static class RandomWithProbabilityGenerator
    {
        public static T GetRandom<T>(Tuple<T, float>[] values)
        {
            float total = 0;

            foreach (var elem in values) {
                total += elem.Item2;
            }

            float randomPoint = Random.value * total;

            for (int i= 0; i < values.Length; i++) {
                if (randomPoint < values[i].Item2) {
                    return values[i].Item1;
                }
                else {
                    randomPoint -= values[i].Item2;
                }
            }
            return values[values.Length - 1].Item1;
        }
    }
}