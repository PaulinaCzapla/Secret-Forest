using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace RandomGenerators
{
    public static class RandomWithProbabilityGenerator
    {
        public static T GetRandom<T>(Tuple<T, float>[] values)
        {
            float total = 0;

            foreach (var elem in values)
            {
                total += elem.Item2;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < values.Length; i++)
            {
                if (randomPoint < values[i].Item2)
                {
                    return values[i].Item1;
                }
                else
                {
                    randomPoint -= values[i].Item2;
                }
            }

            return values[values.Length - 1].Item1;
        }

        public static bool GetRandom(float trueChance, float falseChance)
        {
            float total = trueChance + falseChance;

            float randomPoint = Random.value * total;

            if (randomPoint < trueChance)
                return true;
            return false;
        }
        
        public static T GetRandom<T>(List<Tuple<T, float>> values)
        {
            float total = 0;

            foreach (var elem in values)
            {
                total += elem.Item2;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < values.Count; i++)
            {
                if (randomPoint < values[i].Item2)
                {
                    return values[i].Item1;
                }
                else
                {
                    randomPoint -= values[i].Item2;
                }
            }

            return values[values.Count - 1].Item1;
        }

        public static List<T> GetRandom<T>(List<Tuple<T, float>> values, int amount)
        {
            float total = 0;
            List<T> result = new List<T>(amount);

            foreach (var elem in values)
            {
                total += elem.Item2;
            }

            bool wasAdded = false;
            
            do
            {
                float randomPoint = Random.value * total;
                
                for (int i = 0; i < values.Count; i++)
                {
                    if (randomPoint < values[i].Item2)
                    {
                        if (result.Contains(values[i].Item1))
                            amount++;
                        else
                        {
                            result.Add(values[i].Item1);
                            wasAdded = true;
                            break;
                        }

                        amount--;
                        if (amount == 0)
                            return result;
                    }
                    else
                    {
                        randomPoint -= values[i].Item2;
                    }
                }

                if (!wasAdded)
                {
                    result.Add(values[values.Count - 1].Item1);
                    wasAdded = false;
                }

                amount--;
            } while (amount > 0);

            return result;
        }
    }
}