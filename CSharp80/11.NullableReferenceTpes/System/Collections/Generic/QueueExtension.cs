using System;
using System.Collections.Generic;
using System.Text;

namespace _11.NullableReferenceTpes.System.Collections.Generic
{
    public static class QueueExtension
    {
        public static Queue<T> Shift<T>(this Queue<T> queue, int elementsToShift)
        {
            for (; elementsToShift > 0; elementsToShift -= 1)
            {
                if (queue.TryDequeue(out T result))
                {
                    queue.Enqueue(item: result);
                }
            }

            return queue;
        }

        public static Queue<T> ShiftOne<T>(this Queue<T> queue)
            => Shift(queue: queue, elementsToShift: 1);
    }
}
