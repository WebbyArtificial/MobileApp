using MobilePrototype.View;

namespace MobilePrototype
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
        }
    }
}
