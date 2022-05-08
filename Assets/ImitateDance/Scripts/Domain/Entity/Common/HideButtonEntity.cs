namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class HideButtonEntity
    {
        public bool IsHide { get; private set; }

        public HideButtonEntity()
        {
            IsHide = false;
        }

        public void Set(bool value)
        {
            IsHide = value;
        }
    }
}