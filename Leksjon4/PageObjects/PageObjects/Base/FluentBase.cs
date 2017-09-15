namespace PageObjects.Base
{
    public class FluentBase<T> where T : FluentBase<T>
    {
        public T Gitt => (T)this;

        public T At => (T)this;

        public T Når => (T)this;

        public T Det => (T)this;

        public T Har => (T)this;

        public T Så => (T)this;

        public T Skal => (T)this;

        public T Vi => (T)this;
    }
}
