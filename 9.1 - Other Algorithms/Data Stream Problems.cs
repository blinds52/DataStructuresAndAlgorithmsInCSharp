﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.OtherAlgorithms
{
    public class PriorityQueue<T>
    {
        IComparer<T> comparer;
        T[] heapArray;

        public int Count { get; private set; }

        public PriorityQueue(int capacity = 16, IComparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            this.heapArray = new T[capacity];
        }

        public void Enqueue(T value)
        {
            if (Count >= heapArray.Length)
            {
                Array.Resize(ref heapArray, Count * 2);
            }

            heapArray[Count] = value;
            SiftUp(Count++);
        }

        public T Dequeue()
        {
            T value = Peek();
            heapArray[0] = heapArray[--Count];

            if (Count > 1)
            {
                SiftDown(0);
            }

            return value;
        }

        public T Peek() // Or Top
        {
            if (Count > 0)
            {
                return heapArray[0];
            }

            throw new InvalidOperationException("");
        }

        void SiftUp(int curPosition)
        {
            T value = heapArray[curPosition];

            for (int index = curPosition / 2; curPosition > 0; curPosition = index, index /= 2)
            {
                if (comparer.Compare(value, heapArray[index]) > 0)
                {
                    heapArray[curPosition] = heapArray[index];
                }
                else
                {
                    break;
                }
            }

            heapArray[curPosition] = value;
        }

        void SiftDown(int curPosition)
        {
            T value = heapArray[curPosition];

            for (int index = curPosition * 2; index < Count; curPosition = index, index *= 2)
            {
                if (index + 1 < Count && comparer.Compare(heapArray[index + 1], heapArray[index]) > 0)
                {
                    index++;
                }

                if (comparer.Compare(value, heapArray[index]) >= 0)
                {
                    break;
                }

                heapArray[curPosition] = heapArray[index];
            }

            heapArray[curPosition] = value;
        }
    }

    // LC 346 Moving Average Using Queue
    //------------------------------------------------------------------------------------------------------------
    // https://discuss.leetcode.com/category/430/moving-average-from-data-stream
    public class MovingAverage
    {
        private int size;
        private int curSum;
        private Queue<int> queue;

        public MovingAverage(int size)
        {
            this.size = size;
            this.curSum = 0;
            this.queue = new Queue<int>();
        }

        public double Next(int val)
        {
            queue.Enqueue(val);

            if (queue.Count > size)
            {
                curSum -= queue.Dequeue();
            }

            curSum += val;

            return (double)curSum / queue.Count;
        }
    }

    // Using Array
    /*
    The idea is to use an array int[] arr = new int[size] to keep the previous elements.
    Use an int to keep the count of the inserting elements and a double to keep the sum.
    If a new element is waiting to insert:
    if count<arr.size
    put the new in the array
    sum = sum + new val;
    count ++
    $) cal the average
    else if count >= arr.length
    replace index = count % arr.length
    sum -= arr[replace index];
    arr[replace index] = new val
    sum += new val;
    avg = sum / arr.length;
    */

    public class MovingAverage2
    {
        private int[] arr;
        int count;
        double sum;

        public MovingAverage2(int size)
        {
            arr = new int[size];
            count = 0;
            sum = 0.0;
        }

        public double Next(int val)
        {
            double avg = 0.0;
            int size = arr.Length;

            if (size == 0)
                return avg;

            if (count < size)
            {
                arr[count] = val;
                sum += arr[count];
                count += 1;
                avg = sum / count;
            }
            else
            {
                int replace = count % size;
                sum -= arr[replace];
                arr[replace] = val;
                sum += val;
                count += 1;
                avg = sum / size;
            }
            return avg;
        }
    }

    // LC 295 Median from Data Stream
    //---------------------------------------------------------------------------------------------------------------------------------
    // https://leetcode.com/problems/find-median-from-data-stream
    // https://www.programcreek.com/2015/01/leetcode-find-median-from-data-stream-java/

    // Using Ordered Multi Set (Sorted Set and Dictionary)
    public class MedianFinder
    {
        private class OrderedMultiSet
        {
            private readonly SortedSet<int> set;
            private readonly Dictionary<int, int> numberToCount = new Dictionary<int, int>();
            private int size = 0;

            public OrderedMultiSet(Comparer<int> comparer)
            {
                this.set = new SortedSet<int>(comparer);
            }

            public int Count
            {
                get { return this.size; }
            }

            public int Top { get { return this.set.Max; } }

            public void Push(int value)
            {
                int count;
                if (!this.numberToCount.TryGetValue(value, out count))
                {
                    count = 0;
                    this.numberToCount.Add(value, count);
                }

                this.numberToCount[value] = count + 1;
                this.set.Add(value);
                this.size++;
            }

            public int Pop()
            {
                int value = this.Top;
                if (--this.numberToCount[value] == 0)
                {
                    this.numberToCount.Remove(value);
                    this.set.Remove(value);
                }
                --size;
                return value;
            }
        }

        // initialize your data structure here
        OrderedMultiSet lSet = new OrderedMultiSet(Comparer<int>.Default);
        OrderedMultiSet rSet = new OrderedMultiSet(Comparer<int>.Default); //.Create((a, b) => b.CompareTo(a)));

        public MedianFinder()
        {
        }

        public void AddNum(int num)
        {
            if (this.lSet.Count == 0 || this.lSet.Top >= num)
                this.lSet.Push(num);

            else if (this.rSet.Count == 0 || this.rSet.Top <= num)
                this.rSet.Push(num);

            else
                this.lSet.Push(num);

            OrderedMultiSet recieverSet = (lSet.Count < rSet.Count) ? lSet : rSet;
            OrderedMultiSet supplierSet = (lSet.Count >= rSet.Count) ? lSet : rSet;

            while (supplierSet.Count >= recieverSet.Count + 2)
            {
                recieverSet.Push(supplierSet.Pop());
            }
        }

        public double FindMedian()
        {
            if (this.lSet.Count == this.rSet.Count)
                return ((double)this.lSet.Top + (double)this.rSet.Top) / 2;
            else
                return this.lSet.Count > this.rSet.Count ? this.lSet.Top : this.rSet.Top;
        }
    }

    // With Custom Priority Queue class (heap)
    public class MedianFinder1
    {
        // minPQ.Count >= maxPQ.Count
        private PriorityQueue<double> minPQ = new PriorityQueue<double>();
        private PriorityQueue<double> maxPQ = new PriorityQueue<double>();

        // Adds a num into the data structure.
        public void AddNum(double num)
        {
            maxPQ.Enqueue(-num);
            minPQ.Enqueue(-maxPQ.Dequeue());

            if (minPQ.Count >= maxPQ.Count + 2)
            {
                maxPQ.Enqueue(-minPQ.Dequeue());
            }
        }

        // return the median of current data stream
        public double FindMedian()
        {
            if (minPQ.Count > maxPQ.Count)
            {
                return minPQ.Peek();
            }
            else
            {
                return (minPQ.Peek() - maxPQ.Peek()) / 2;
            }
        }
    }

    // List & Binary Search
    public class MedianFinder2
    {
        List<int> nums;

        public MedianFinder2()
        {
            nums = new List<int>();
        }

        public void AddNum(int num)
        {
            int index = nums.BinarySearch(num);

            if (index < 0)
            {
                nums.Insert(~index, num);
            }
            else
            {
                nums.Insert(index, num);
            }
        }

        public double FindMedian()
        {
            double median = nums[nums.Count / 2];

            if (nums.Count % 2 == 0)
            {
                median += nums[nums.Count / 2 - 1];
                median /= 2;
            }
            return median;
        }
    }

    // Using Sorted Set
    public class MedianFinder3
    {
        // For threadsafty we can use ImmutableSortedSet.
        SortedSet<Tuple<int, int>> maxSet;
        SortedSet<Tuple<int, int>> minSet;

        int index = 0;

        public MedianFinder3()
        {
            maxSet = new SortedSet<Tuple<int, int>>(Comparer<Tuple<int, int>>.Create((a, b) =>
                    a.Item1 == b.Item1 ? a.Item2 - b.Item2 : a.Item1 - b.Item1
            ));

            minSet = new SortedSet<Tuple<int, int>>(Comparer<Tuple<int, int>>.Create((a, b) =>
                    a.Item1 == b.Item1 ? a.Item2 - b.Item2 : a.Item1 - b.Item1
            ));
        }

        public void AddNum(int num)
        {
            minSet.Add(new Tuple<int, int>(num, index++));
            maxSet.Add(minSet.Min);

            minSet.Remove(minSet.Min);

            if (maxSet.Count > minSet.Count)
            {
                minSet.Add(maxSet.Max);
                maxSet.Remove(maxSet.Max);
            }
        }

        public double FindMedian()
        {
            if (maxSet.Count == minSet.Count)
            {
                return (maxSet.Max.Item1 + minSet.Min.Item1) / 2.0;
            }
            else
            {
                return minSet.Min.Item1;
            }
        }
    }


    // LC 352 Data Stream as Disjoint Interval
    //------------------------------------------------------------------------------------------------------------
    // https://leetcode.com/problems/data-stream-as-disjoint-intervals
    // https://www.programcreek.com/2014/08/leetcode-data-stream-as-disjoint-intervals-java/

    public class SummaryRanges
    {
        //TreeMap<Integer, Interval> tree;

        //public SummaryRanges()
        //{
        //    tree = new TreeMap<>();
        //}

        //public void addNum(int val)
        //{
        //    if (tree.containsKey(val))
        //        return;

        //    Integer l = tree.lowerKey(val);
        //    Integer h = tree.higherKey(val);

        //    if (l != null && h != null && tree.get(l).end + 1 == val && h == val + 1)
        //    {
        //        tree.get(l).end = tree.get(h).end;
        //        tree.remove(h);
        //    }
        //    else if (l != null && tree.get(l).end + 1 >= val)
        //    {
        //        tree.get(l).end = Math.max(tree.get(l).end, val);
        //    }
        //    else if (h != null && h == val + 1)
        //    {
        //        tree.put(val, new Interval(val, tree.get(h).end));
        //        tree.remove(h);
        //    }
        //    else
        //    {
        //        tree.put(val, new Interval(val, val));
        //    }
        //}

        //public List<Interval> getIntervals()
        //{
        //    return new ArrayList<>(tree.values());
        //}
    }

    // https://www.geeksforgeeks.org/kth-largest-element-in-a-stream/

    public class KthSmallest
    {
        public void TestKth()
        {
            Console.WriteLine("Enter the value of k : ");
            int k = Convert.ToInt32(Console.ReadLine());
            PriorityQueue<int> min = new PriorityQueue<int>();
            //PriorityQueue<integer> min = new PriorityQueue<>(k, new Comparator<integer>() {

            //int compare(int a, int b)
            //{
            //if(a < b) {
            //return -1;
            //}else if( a > b) {
            //return 1;
            //}
            //return 0;
            //}
            //});

            while (true)
            {
                Console.WriteLine("Enter the next Element : ");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == -1)
                {
                    break;
                }
                FindKth(min, n, k);
            }
        }

        private void FindKth(PriorityQueue<int> min, int n, int k)
        {
            int count = min.Count;

            if (count < k - 1)
            {
                min.Enqueue(n);
                Console.WriteLine("_");
            }
            else
            {

                if (count == k - 1)
                {
                    min.Enqueue(n);
                }
                else
                {

                    if (n > min.Peek())
                    {
                        min.Dequeue();
                        min.Enqueue(n);
                    }
                }
                Console.WriteLine(min.Peek());
            }
        }

        // https://www.geeksforgeeks.org/select-a-random-number-from-stream-with-o1-space/

        // https://www.geeksforgeeks.org/find-first-non-repeating-character-stream-characters/

        // https://www.geeksforgeeks.org/online-algorithm-for-checking-palindrome-in-a-stream/
    }

    // https://www.geeksforgeeks.org/online-algorithm-for-checking-palindrome-in-a-stream/

    // https://www.geeksforgeeks.org/rank-element-stream/

    // https://www.geeksforgeeks.org/find-top-k-or-most-frequent-numbers-in-a-stream/
}