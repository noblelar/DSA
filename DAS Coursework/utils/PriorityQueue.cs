using System;
using DAS_Coursework.models;

namespace DAS_Coursework.utils
{
    

    public class PriorityQueue
    {
        private (Verticex, double)[] queue;
        private int count;

        public PriorityQueue()
        {
            queue = new (Verticex, double)[10]; // Initial capacity
            count = 0;
        }

        public void Enqueue(Verticex vertex, double priority)
        {
            if (count == queue.Length)
                ResizeQueue(queue.Length * 2); // Double the capacity if full

            queue[count++] = (vertex, priority);
            Sort();
        }

        public Verticex Dequeue()
        {
            if (IsEmpty()) return null;
            Verticex vertex = queue[0].Item1;
            for (int i = 1; i < count; i++)
            {
                queue[i - 1] = queue[i];
            }
            count--;

            // Resize the queue if it's less than 25% full
            if (count < queue.Length / 4)
                ResizeQueue(queue.Length / 2);

            return vertex;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        private void Sort()
        {
            Array.Sort(queue, 0, count, new PriorityQueueComparer());
        }

        private void ResizeQueue(int newCapacity)
        {
            (Verticex, double)[] newQueue = new (Verticex, double)[newCapacity];
            Array.Copy(queue, newQueue, count);
            queue = newQueue;
        }

        private class PriorityQueueComparer : IComparer<(Verticex, double)>
        {
            public int Compare((Verticex, double) x, (Verticex, double) y)
            {
                return x.Item2.CompareTo(y.Item2);
            }
        }
    }

}

