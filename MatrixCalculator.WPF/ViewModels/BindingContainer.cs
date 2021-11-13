using System.ComponentModel;
using System.Windows;

namespace MatrixCalculator.WPF.ViewModels
{
    public class BindingContainer<T> : DependencyObject
    {
        public BindingContainer() { }
        public BindingContainer(T value)
        {
            Value = value;
        }

        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(T), typeof(BindingContainer<T>), new PropertyMetadata(0));
    }
}
