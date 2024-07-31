
using MobilePrototype.Pages;

namespace MobilePrototype
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        }
    }
}
