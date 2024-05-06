using System;
namespace DAS_Coursework.utils
{
    public class PriorityQueue
    {
        private (string, double)[] queue;
        private int count;

        public PriorityQueue()
        {
            this.queue = new (string, double)[0];
            this.count = 0;
        }

        public void Enqueue(string element, double priority)
        {
            Array.Resize(ref this.queue, this.queue.Length + 1);
            this.queue[this.count++] = (element, priority);
            this.Sort();
        }

        public string Dequeue()
        {
            if (this.IsEmpty()) return null;
            var dequeued = this.queue[0].Item1;
            Array.Copy(this.queue, 1, this.queue, 0, --this.count);
            return dequeued;
        }

        private void Sort()
        {
            for (int i = 0; i < this.count - 1; i++)
            {
                for (int j = 0; j < this.count - i - 1; j++)
                {
                    if (this.queue[j].Item2 > this.queue[j + 1].Item2)
                    {
                        // Swap elements
                        var temp = this.queue[j];
                        this.queue[j] = this.queue[j + 1];
                        this.queue[j + 1] = temp;
                    }
                }
            }

        }

        public bool IsEmpty()
        {
            return this.count == 0;
        }
    }

}

