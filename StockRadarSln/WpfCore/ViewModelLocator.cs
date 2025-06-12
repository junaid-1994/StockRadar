using SR.Common.Extensions;
using System.Windows;

namespace WpfCore
{
    /// <summary>
    /// Provides a mechanism to automatically associate a view with its corresponding view model using a naming
    /// convention. This class is typically used in MVVM applications to simplify the process of setting the <see
    /// cref="FrameworkElement.DataContext"/> for views.
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// Identifies the AutoWireViewModel attached property, which determines whether the ViewModelLocator
        /// automatically wires a ViewModel to a view.
        /// </summary>
        /// <remarks>This attached property can be set on a view to enable automatic ViewModel resolution
        /// and assignment by the ViewModelLocator. When set to <see langword="true"/>, the ViewModelLocator attempts to
        /// locate and assign a ViewModel to the view based on naming conventions or other configured logic.</remarks>
        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, OnAutoWireViewModelPropertyChanged));

        public static void SetAutoWireViewModel(FrameworkElement element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        public static object GetAutoWireViewModel(FrameworkElement elemet)
        {
            return elemet.GetValue(AutoWireViewModelProperty);
        }

        private static void OnAutoWireViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is FrameworkElement viewInstance)
            {
                var viewType = viewInstance.GetType();
                var viewName = viewType.FullName;
                var viewAssembly = viewType.Assembly;

                if(viewAssembly.FullName.IsNullOrEmpty() || viewName.IsNullOrEmpty())
                {
                    return;
                }

                var viewModelName = viewName?.Replace(Constants.ViewNamePostfix, Constants.ViewModelNamePostfix);
                var viewModelInstance = Activator.CreateInstance(viewAssembly.FullName, viewModelName);
                viewInstance.DataContext = viewModelInstance;
            }
        }
    }
}
