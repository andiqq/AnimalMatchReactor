namespace AnimalMatchReactor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiReactorApp<MainPage>();
            return builder.Build();
        }
    }
}