using System;
using System.Linq;
using System.Collections.Generic;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int find(int[] list, List<int> indexes, int order)
        {
            if (indexes.Count == 1)
                return list[indexes[0]];
            if (order == 1)
            {
                int max = list[indexes[0]];
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (max < list[indexes[i]])
                        max = list[indexes[i]];
                }
                return max;
            }
            else
            {
                int min = list[indexes[0]];
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (min > list[indexes[i]])
                        min = list[indexes[i]];
                }
                return min;
            }
        }

        public static List<int> updateIndexes(int[] list, List<int> indexes, int val)
        {
            List<int> updatedIndexes = new List<int>();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == val)
                    updatedIndexes.Add(i);
            }

            return updatedIndexes;
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int[] cal = new int[protein.Length];
            int[] meals = new int[dietPlans.Length];
            for (int i = 0; i < protein.Length; i++)
                cal[i] = fat[i] * 9 + (carbs[i] + protein[i]) * 5;

            for (int i = 0; i < dietPlans.Length; i++)
            {
                List<int> indexes = new List<int>();
                int max, min;

                for (int k = 0; k < protein.Length; k++)
                    indexes.Add(k);

                for (int j = 0; j < dietPlans[i].Length; j++)
                {
                    switch (dietPlans[i][j])
                    {
                        case 'C':
                            max     = find(carbs, indexes, 1);
                            indexes = updateIndexes(carbs, indexes, max);
                            break;

                        case 'c':
                            min     = find(carbs, indexes, 0);
                            indexes = updateIndexes(carbs, indexes, min);
                            break;

                        case 'P':
                            max     = find(protein, indexes, 1);
                            indexes = updateIndexes(protein, indexes, max);
                            break;

                        case 'p':
                            min     = find(protein, indexes, 0);
                            indexes = updateIndexes(protein, indexes, min);
                            break;

                        case 'F':
                            max     = find(fat, indexes, 1);
                            indexes = updateIndexes(fat, indexes, max);
                            break;

                        case 'f':
                            min     = find(fat, indexes, 0);
                            indexes = updateIndexes(fat, indexes, min);
                            break;

                        case 'T':
                            max     = find(cal, indexes, 1);
                            indexes = updateIndexes(cal, indexes, max);
                            break;

                        case 't':
                            min     = find(cal, indexes, 0);
                            indexes = updateIndexes(cal, indexes, min);
                            break;
                    }
                }

                meals[i] = indexes[0];
            }

            return meals;
        }
    }
}
