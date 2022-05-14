using System;
using System.Collections.Generic;

namespace asom.lib.core.util
{
    public class RandomSelector<T, U> where T : List<U>, new()
    {
        private int limit = 10; // default selector limit
        private T sourceLst;

        public RandomSelector(T source)
        {
            sourceLst = source;
        }

        public RandomSelector()
        {
        }

        public int Limit
        {
            get { return limit; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                limit = value;
            }
        }

        public T Randomize(int limit)
        {
            Limit = limit;
            return Randomize();
        }

        public T Randomize(int limit, T sourceList)
        {
            this.sourceLst = sourceList;
            Limit = limit;
            return Randomize();
        }

        public T SourceList
        {
            get { return sourceLst; }
            set { sourceLst = value; }
        }

        public T Randomize()
        {
            T res = sourceLst;
            T result = new T();
            int z = 0;
            int count = res.Count;
            if (sourceLst.Count < 1)
            {
                return sourceLst;
            }

            if (Limit > sourceLst.Count)
            {
                Limit = sourceLst.Count;
            }

            for (int i = 0; i < Limit; i++)
            {
                System.Threading.Thread.Sleep(50);
                Random rnd = new Random();
                for (int j = 0; j < count; j++)
                {
                    rnd = new Random();
                    z = new Random().Next(0, new Random().Next(1, count));
                }

                int index = rnd.Next(z, count);
                result.Add(res[index]);
                res.RemoveAt(index);
                count = res.Count;
            }

            return result;
        }
    }
}
