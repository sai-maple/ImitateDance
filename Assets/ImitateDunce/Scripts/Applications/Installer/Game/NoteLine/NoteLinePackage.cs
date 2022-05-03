using ImitateDunce.Domain.Entity.Game.NoteLine;
using ImitateDunce.Domain.UseCase.Game.NoteLine;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Game.NoteLine
{
    public sealed class NoteLinePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PositionEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LinePositionUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // todo presenter and view
        }
    }
}