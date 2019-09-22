using CBTools_Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Extensions
{
    public enum ArrayFillOrder
    {
        LowStart,
        HighStart,
        LowCenter,
        HighCenter,
        equalValue,
        RandomValues
    }

    public static class ArrayExtensions
    {
        internal static readonly Random defaultRand = new Random();

        public static T Random<T>(this T[] array)
        {
            if (array.Length == 0)
                throw new Exception("Zero-length array used");
            else
                return array[defaultRand.Next(array.Length)];
        }

        /// <summary>
        /// Copys contents to a new array then calls ShuffleInplace on that and returns. Does not modify initial
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] array) {
            T[] copy = new T[array.Length];
            array.CopyTo(copy, 0);
            return copy.ShuffleInplace();
        }

        /// <summary>
        /// Iterative shuffle of an array. Modifies initial array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="random">An optional random generator. If left null will use a default static one.</param>
        /// <returns></returns>
        public static T[] ShuffleInplace<T>(this T[] array, Random? random = null)
        {
            int n = array.Length;
            while (n > 0) {
                int r = random == null ? defaultRand.Next(0, n) : random.Next(0, n);
                T temp = array[r];
                array[r] = array[n - 1];
                array[n - 1] = temp;
                n--;
            }
            return array;
            //if (n == 0)
            //    return array;
            //else
            //{
            //    array = Shuffle(n - 1, array);
            //    int r = rand.Next(0, n);
            //    T temp = array[r];
            //    array[r] = array[n - 1];
            //    array[n - 1] = temp;
            //    return array;
            //}
        }

        


        /// <summary>
        /// Initialise the values of an array. Uses ref
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="order">Order to fill in.
        /// LowStart (eg. 0,1,2,3,4,5,6)
        /// HighStart (eg. 6,5,4,3,2,1,0)
        /// LowCenter (eg. 3,2,1,0,1,2,3)
        /// HighCenter (eg. 0,1,2,3,2,1,0)
        /// EqualValue (eg. 3,3,3,3,3,3,3) 
        /// RandomValue (step = max value for this eg. 2,5,8,1,3,12,6)</param>
        /// <param name="lowestValue">start value. If step is negative, this will actually be the highest value</param>
        /// <param name="step">How much to increase or decrease each item by. For random order, this is max</param>
        public static void Init(this int[] arr, ArrayFillOrder order, int lowestValue = 0, int step = 1)
        {
            switch (order)
            {
                case ArrayFillOrder.HighStart:
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        arr[i] = lowestValue;
                        lowestValue += step;
                    }
                    break;

                case ArrayFillOrder.HighCenter:
                    for (int i = 0; i < (arr.Length.IsEven() ? arr.Length / 2 : arr.Length / 2 + 1); i++)
                    {
                        arr[i] = lowestValue;
                        arr[arr.Length - 1 - i] = lowestValue;
                        lowestValue += step;
                    }
                    break;

                case ArrayFillOrder.LowCenter:
                    for (int i = (arr.Length.IsEven() ? arr.Length / 2 : arr.Length / 2 + 1) - 1; i >= 0; i--)
                    {
                        arr[i] = lowestValue;
                        arr[arr.Length - 1 - i] = lowestValue;
                        lowestValue += step;
                    }
                    break;

                case ArrayFillOrder.LowStart:
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = lowestValue;
                        lowestValue += step;
                    }
                    break;

                case ArrayFillOrder.RandomValues:
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = defaultRand.Next(lowestValue, step);
                    }
                    break;


                case ArrayFillOrder.equalValue:
                default:
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        arr[i] = lowestValue;
                    }
                    break;
            }
        }
    }
}
