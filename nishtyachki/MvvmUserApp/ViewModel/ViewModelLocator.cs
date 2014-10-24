/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MvvmUserApp.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmUserApp.Model;
using MvvmUserApp.QueueReference;
using MvvmUserApp.Views;
using System.ServiceModel;
using System.Windows;

namespace MvvmUserApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            UserAppServiceCallback callback = null;

            SimpleIoc.Default.Register<IUserAppService>(() =>
            {
                callback = new UserAppServiceCallback(new ShowMessages());
                InstanceContext context = new InstanceContext(callback);

                var service = new UserAppServiceClient(context);

                ButtonData.InitCommandsForButtons(service);

                return service;
            });

            SimpleIoc.Default.Register<MainViewModel>(() =>
            {
                var appService = SimpleIoc.Default.GetInstance<IUserAppService>();
                var view = Application.Current.MainWindow;
                var viewModel = new MainViewModel(appService, new ViewMainWindow(view));
                callback.MainViewModel = viewModel;
                return viewModel;
            });

        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}