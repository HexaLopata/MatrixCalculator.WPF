using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatrixCalculator.WPF
{
    internal class MatrixView : ItemsControl
    {
        public static readonly DependencyProperty WidthInCellsProperty =
            DependencyProperty.Register("WidthInCells", typeof(int), typeof(MatrixView), new PropertyMetadata(0));

        public static readonly DependencyProperty HeightInCellsProperty =
            DependencyProperty.Register("HeightInCells", typeof(int), typeof(MatrixView), new PropertyMetadata(0));

        public int WidthInCells
        {
            get { return (int)GetValue(WidthInCellsProperty); }
            set { SetValue(WidthInCellsProperty, value); }
        }

        public int HeightInCells
        {
            get { return (int)GetValue(HeightInCellsProperty); }
            set { SetValue(HeightInCellsProperty, value); }
        }
    }
}
