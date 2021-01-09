using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberArray
{
    public class Calculations
    {
        public event EventHandler<GreaterThan15EventArgs> GreaterThan15;
        private int _maxValue = 1000;
        private int _size;
        private int _div;
        private int _lim;

        public Calculations(int size, int div, int lim)
        {
            _size = size;
            _div = div;
            _lim = lim;
        }

        public void Start()
        {
            int[] array = GenerateRandomArray(_size, _maxValue);
            List<int> selectedInts = SelectWithoutRemainder(array, _div, _lim);
            int[] sortedArray = SortDescending(array);
            if (selectedInts.Count > 15)
            {
                var args = new GreaterThan15EventArgs();
                args.X = SelectGreaterThanLimit(array, _lim);
                args.Y = _lim;
                OnGreaterThan15(args);
            }
            else if (selectedInts.Count < 15)
            {
                Console.Write("The following array elements were not selected: ");
                foreach (int i in sortedArray)
                    if (!selectedInts.Contains(i))
                        Console.Write(i + "   ");
                Console.WriteLine(string.Empty);
            }
        }

        protected virtual void OnGreaterThan15(GreaterThan15EventArgs e)
        {
            GreaterThan15.Invoke(this, e);
        }

        private int[] GenerateRandomArray(int size, int max)
        {
            var rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(max);
            }
            return array;
        }

        private List<int> SelectWithoutRemainder(int[] array, int div, int lim)
        {
            var result =
                            from item in array
                            where item % div == 0 && item < lim
                            select item;
            return result.ToList();
        }

        private int[] SortDescending(int[] array)
        {
            Array.Sort(array, (a, b) => b.CompareTo(a));
            return array;
        }

        private int SelectGreaterThanLimit(int[] array, int lim)
        {
            var result =
                            from item in array
                            where item > lim
                            select item;
            return result.ToList().Count;
        }

    }

    public class GreaterThan15EventArgs : EventArgs
    {
        public int X
        { get; set; }
        public int Y
        { get; set; }

    }

}
