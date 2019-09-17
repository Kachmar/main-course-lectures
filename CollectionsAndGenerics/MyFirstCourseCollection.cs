using System.Collections;

namespace CollectionsAndGenerics
{
    class MyFirstCourseCollection : IEnumerator, IEnumerable
    {
        private Student[] container;
        private int position = -1;

        public MyFirstCourseCollection()
        {
            this.container = new Student[0];
        }

        public void Add(Student student)
        {
            if (!student.GroupName.EndsWith("1"))
            {
                return;
            }

            Student[] newContainer = new Student[this.container.Length + 1];
            for (int i = 0; i < this.container.Length; i++)
            {
                newContainer[i] = this.container[i];
            }
            newContainer[this.container.Length] = student;
            this.container = newContainer;
        }

        public void Dispose()
        {
            this.container = null;
        }

        public bool MoveNext()
        {
            position++;
            return position < this.container.Length;
        }

        public void Reset()
        {
            this.position = 0;
        }

        public Student Current
        {
            get
            {
                return this.container[this.position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

    }
    //class MyFirstCourseCollection<T> : IEnumerator<T>, IEnumerable<T>
    //{
    //    private T[] container;
    //    private int position = -1;

    //    public MyFirstCourseCollection()
    //    {
    //        this.container = new T[0];
    //    }

    //    public void Add(T element)
    //    {

    //        T[] newContainer = new T[this.container.Length + 1];
    //        for (int i = 0; i < this.container.Length; i++)
    //        {
    //            newContainer[i] = this.container[i];
    //        }
    //        newContainer[this.container.Length] = element;
    //        this.container = newContainer;
    //    }

    //    public void Dispose()
    //    {
    //        this.container = null;
    //    }

    //    public bool MoveNext()
    //    {
    //        position++;
    //        return position < this.container.Length;
    //    }

    //    public void Reset()
    //    {
    //        this.position = 0;
    //    }

    //    T IEnumerator<T>.Current
    //    {
    //        get
    //        {
    //            return this.container[this.position];
    //        }
    //    }

    //    object IEnumerator.Current
    //    {
    //        get
    //        {
    //            return this.container[this.position];
    //        }
    //    }

    //    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //    {
    //        return this;
    //    }

    //    public IEnumerator GetEnumerator()
    //    {
    //        return this;
    //    }
    //}
}