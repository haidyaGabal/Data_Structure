using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAdvanced
{
    public class ItemAdvanced<T> : IEnumerable<T>,ICollection<T>,IList<T>
    {
        T []items;
        public int count { get; set; }
        public int currentIndex { get; set; }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        const int capacity = 4;
        IEqualityComparer<T> Comparer;



        public ItemAdvanced() 
        {
            items = new T[capacity];
            currentIndex = 0;
            count = 0;
            Comparer = EqualityComparer<T>.Default;


        }
        public ItemAdvanced(IEqualityComparer<T> comparer)
        {
            items = new T[capacity];
            currentIndex = 0;
            count = 0;
            Comparer = comparer;



        }

        public ItemAdvanced(int Capacity)
        {
           items = new T[Capacity];
            currentIndex = 0;
            count = 0;
            Comparer = EqualityComparer<T>.Default;

        }
        public ItemAdvanced(IEnumerable<T> values)
        {
            int nCount = IEnumerableCount(values);
            items = new T[nCount];
            foreach (T item in values)
            {
                Add(item);
            }

            Comparer = EqualityComparer<T>.Default;


        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > items.Length)
                    throw new ArgumentOutOfRangeException("index");
                return items[index];

            }

            set
            {
                if (index < 0 || index > items.Length)
                    throw new ArgumentOutOfRangeException("index");
                items[index] = value;


            }
        }
        public List<T> this[string Indexes]
        {
            get
            {
                if (string.IsNullOrEmpty(Indexes))
                    throw new ArgumentOutOfRangeException("index");
                string[] newString = Indexes.Split(',');

                List<T> list = new List<T>();

                foreach (string index in newString)
                {
                    if (Convert.ToInt32(index) < 0 || Convert.ToInt32(index) > items.Length) 
                        continue;
                    if (items[Convert.ToInt32(index)] == null)
                        continue;
                    list.Add(items[Convert.ToInt32(index)]);

                }
                return list;


            }

           
        }
        public void Add(T item)
        {
            if (currentIndex >= items.Length)
                reSize(items.Length * 2); 
            items[currentIndex] = item;
            currentIndex++;
            count++;
        }
        public void AddRange(IEnumerable<T> itemRange)
        {
            int nCount = IEnumerableCount(itemRange);
            if ((count+ nCount) >items.Length)
                reSize((count + nCount));

            foreach (T item in itemRange)
            {
                items[currentIndex] = item;
                currentIndex++;
                count++;
            }
        
          
        }
        public void Insert(int index,T item) 
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException("index");
            if(count == items.Length)
                reSize(items.Length * 2);
            ShiftRight(index,1);
            items[index] = item;


        }

        public void InsertRange(int index,IEnumerable<T> itemRange)
        {
            int nCount = IEnumerableCount(itemRange);
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException("index");
            if ((count + nCount) > items.Length)
                reSize((count + nCount ));
            ShiftRight(index, nCount);
            int i = 0;
            foreach (T item in itemRange)
            {
                items[index +i] = item;
                ++i;    
            }

        }
        public T[] ToArray() 
        {
            T[] newItems = new T[count];
            Array.Copy(items, newItems, count);
            return newItems;



        }
        public void RemoveAt(int index)
        {
            if(index < 0 || index > items.Length)
                throw new ArgumentOutOfRangeException("index");

            ShiftLeft(index);
            currentIndex--;
            count--;

        }
        public bool Remove(T item)
        {
 
           int nIndex= IndexOf(item);
            ShiftLeft(nIndex);
            currentIndex--;
            count--;
            return true;

        }
        public void RemoveRange(int index,int countt)
        {
            if (index < 0 || countt < 0 || index > countt || (index + countt) > items.Length) 
                throw new ArgumentOutOfRangeException("index");

            for (int i = (index + countt); i < items.Length; i++) 
            {
                items[index] = items[i];
                index++;
            }
            for (int i = items.Length - 1; i > items.Length - countt-1; i--) 
            {
                items[i] = default(T);
                count--;
                currentIndex--; 

            }
            if (count < ((items.Length) / 2))
                reSize(items.Length / 2);
        }


        void reSize(int newSize)
        {
            T[] newItems = new T[newSize];
            Array.Copy(items,newItems,count);
            items = newItems;

        }
        void ShiftLeft(int index)
        {
            for(int i = index; i < items.Length-1; i++)
            {
                if (items[i] == null)
                    break;
                items[i] = items[i+1];  

            }
            if (items[items.Length - 1] != null)
                items[items.Length - 1] = default(T);
        
          

        }
        void ShiftRight(int index,int numOfShift)
        {
            // if put +1 here int i = items.Length -1; i > numOfShift+1; i-- this is the same result becuse switch it in null for loop in under
            for (int i = items.Length -1; i > numOfShift; i--) 
            {
             items[i] = items[i- numOfShift];
            }
            for (int i = index; i < index+numOfShift; i++)
            {
                items[i] = default(T);
                count++;
                currentIndex++;
            }
    



        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(Comparer.Equals(items[i], item))
                    return i;

            }
            return -1;
        }

        public T[] Reverse()
        {
            T[] ReverseArray = new T[count];
           for (int i = 0;i < count;i++)
            {
                ReverseArray[count - 1 - i] = items[i];

            }
          items = ReverseArray;
           return items;
        }
        public void Sort()
        {
            Array.Sort(items);
        }
        public void RemoveAll()
        {
            for(int i=0; i < count; i++)
            {
                items[i] = default(T);
            }
        }
        public void Clear()
        {
            
            items = new T[capacity];
            count = 0;
            currentIndex = 0;
          

        }

        public IEnumerator<T> GetEnumerator()
        {
           foreach(T item in items)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
        int IEnumerableCount(IEnumerable<T> Count)
        {
            int count = 0;
            foreach(T item in Count)
                count++;

            return count;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) > -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(items, 0, array, arrayIndex, count);
        }

    
    }
}
