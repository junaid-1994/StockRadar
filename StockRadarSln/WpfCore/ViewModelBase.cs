using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfCore
{
    /// <summary>
    /// Serves as a base class for view models, providing support for property change notifications.
    /// </summary>
    /// <remarks>This class implements the <see cref="INotifyPropertyChanged"/> interface, enabling derived
    /// classes  to notify bound UI elements of property changes. It includes a helper method, <see
    /// cref="RaisePropertyChanged"/>,  to simplify raising the <see cref="PropertyChanged"/> event.</remarks>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]  string propertyname = null)
        {
            if (!string.IsNullOrEmpty(propertyname) && PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
