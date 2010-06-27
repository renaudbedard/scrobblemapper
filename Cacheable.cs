using System;

namespace ScrobbleMapper
{
    /// <summary>
    /// Kinda like a LazyInit, but with writing capability
    /// </summary>
    class Cacheable<T>
    {
        bool stale = true;

        T cache;

        readonly Func<T> refresher;
        readonly Action<T> updater;

        /// <param name="refresher">A function that refreshes the field from the data storage</param>
        /// <param name="updater">A function that writes new local data to the data storage</param>
        public Cacheable(Func<T> refresher, Action<T> updater)
        {
            this.refresher = refresher;
            this.updater = updater;
        }

        /// <summary>
        /// Marks this instance as out-of-sync with the data storage
        /// </summary>
        public void Invalidate()
        {
            stale = true;
        }

        public T Value
        {
            get
            {
                if (stale)
                {
                    cache = refresher();
                    stale = false;
                }
                return cache;
            }
            set
            {
                updater(value);
                cache = value;
                stale = false;
            }
        }

        public static implicit operator T(Cacheable<T> cached)
        {
            return cached.Value;
        }
    }
}
