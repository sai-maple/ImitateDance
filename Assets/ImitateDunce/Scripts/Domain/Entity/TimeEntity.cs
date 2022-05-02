namespace ImitateDunce.Domain.Entity
{
    public sealed class TimeEntity
    {
        public float Time { get; private set; }

        public void Start()
        {
            Time = 0;
        }

        public void Update(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}