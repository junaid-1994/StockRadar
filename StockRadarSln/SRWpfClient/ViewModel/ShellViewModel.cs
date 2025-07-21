using Common.Constants;
using SR.Service.Contract.DataContract;
using SR.WpfClient.ServiceProxies;
using System.Collections.ObjectModel;
using WpfCore;

namespace SRWpfClient.ViewModel
{
    /// <summary>
    /// Represents the main view model for the application's shell or primary user interface.
    /// </summary>
    /// <remarks>This class serves as the central view model for managing the state and behavior of the
    /// application's main window or shell. It is typically used in MVVM (Model-View-ViewModel) architectures to bind
    /// data and commands to the shell's UI components.</remarks>
    public class ShellViewModel : ViewModelBase
    {
        private StockNotificationServiceProxy _stockNotificationServiceProxy;

        private ObservableCollection<StockData> _stockCollection;

        public ObservableCollection<StockData> StockCollection
        {
            get { return _stockCollection; }
            set 
            {
                if (_stockCollection != value)
                {
                    _stockCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ShellViewModel()
        {
            _stockNotificationServiceProxy = SR.WpfClient.Factories.ProxyFactory.GetStockNotificationServiceProxy(SRConstants.Scheme_NETTCP);
            InitialiseStockCollection();
        }

        private void InitialiseStockCollection()
        {
            var response = _stockNotificationServiceProxy.Channel.GetStockDetails();
            if (response != null && response.Count > 0)
            {
                StockCollection = new ObservableCollection<StockData>(response);
            }
            else
            {
                StockCollection = new ObservableCollection<StockData>();
            }
        }
    }
}
