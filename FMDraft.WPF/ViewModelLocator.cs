using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FMDraft.WPF
{
    public static class ViewModelLocator
    {
        public static bool GetAutoVireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoVireViewModelProperty);
        }

        public static void SetAutoVireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoVireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoVireViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoVireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoVireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var viewType = d.GetType();
            var viewTypeName = viewType.FullName;
            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
