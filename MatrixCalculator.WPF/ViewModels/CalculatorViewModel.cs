using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MatrixCalculator.WPF.Commands;
using MatrixOperations.BL;

namespace MatrixCalculator.WPF.ViewModels
{
    internal class CalculatorViewModel : ViewModelBase
    {
        private Matrix<BindingContainer<int>> _matrix1;
        private Matrix<BindingContainer<int>> _matrix2;
        private Matrix<BindingContainer<int>> _resultMatrix;
        private RelayCommand _calculateCommand;
        private RelayCommand _setMatrix1SizeCommand;
        private RelayCommand _setMatrix2SizeCommand;

        public RelayCommand CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(par =>
                {
                    try
                    {
                        var matrix1 = new IntMatrix(_matrix1.Width, _matrix1.Height, _matrix1.Select(e => e.Value));
                        var matrix2 = new IntMatrix(_matrix2.Width, _matrix2.Height, _matrix2.Select(e => e.Value));
                        IntMatrix resultMatrix = null;
                        switch (SelectedOperation.Value)
                        {
                            case 0:
                                resultMatrix = matrix1 * matrix2;
                                break;
                            case 1:
                                resultMatrix = matrix1 + matrix2;
                                break;
                            case 2:
                                resultMatrix = matrix1 - matrix2;
                                break;
                        }
                        ResultMatrix = new Matrix<BindingContainer<int>>(
                            resultMatrix.Width,
                            resultMatrix.Height,
                            resultMatrix.Select(e => new BindingContainer<int>() { Value = e }));
                        OnPropertyChanged(nameof(ResultMatrix.Width));
                        OnPropertyChanged(nameof(ResultMatrix.Height));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }));
            }
        }

        public RelayCommand SetMatrix1SizeCommand
        {
            get
            {
                return _setMatrix1SizeCommand ?? (_setMatrix1SizeCommand = new RelayCommand(par =>
                {
                    SetMatrixSize(ref _matrix1, WidthOfFirst.Value, HeightOfFirst.Value);
                    OnPropertyChanged(nameof(Matrix1));
                    OnPropertyChanged(nameof(Matrix1.Width));
                    OnPropertyChanged(nameof(Matrix1.Height));
                }));
            }
        }

        public RelayCommand SetMatrix2SizeCommand
        {
            get
            {
                return _setMatrix2SizeCommand ?? (_setMatrix2SizeCommand = new RelayCommand(par =>
                {
                    SetMatrixSize(ref _matrix2, WidthOfSecond.Value, HeightOfSecond.Value);
                    OnPropertyChanged(nameof(Matrix2));
                    OnPropertyChanged(nameof(Matrix2.Width));
                    OnPropertyChanged(nameof(Matrix2.Height));
                }));
            }
        }

        public Matrix<BindingContainer<int>> Matrix1
        {
            get { return _matrix1; }
            set { SetProperty(ref _matrix1, value); }
        }

        public Matrix<BindingContainer<int>> Matrix2
        {
            get { return _matrix2; }
            set { SetProperty(ref _matrix2, value); }
        }

        public Matrix<BindingContainer<int>> ResultMatrix
        {
            get { return _resultMatrix; }
            set { SetProperty(ref _resultMatrix, value); }
        }

        public List<string> Operations
        {
            get => new List<string>() { "Умножение", "Сложнение", "Вычитание" };
        }

        public BindingContainer<int> SelectedOperation { get; set; } = new BindingContainer<int>();
        public BindingContainer<int> WidthOfFirst { get; set; } = new BindingContainer<int>(3);
        public BindingContainer<int> WidthOfSecond { get; set; } = new BindingContainer<int>(3);
        public BindingContainer<int> HeightOfFirst { get; set; } = new BindingContainer<int>(3);
        public BindingContainer<int> HeightOfSecond { get; set; } = new BindingContainer<int>(3);

        public CalculatorViewModel()
        {
            _matrix1 = new Matrix<BindingContainer<int>>(3, 3, new BindingContainer<int>[9].Select(e => new BindingContainer<int>()));
            _matrix2 = new Matrix<BindingContainer<int>>(3, 3, new BindingContainer<int>[9].Select(e => new BindingContainer<int>()));
            _resultMatrix = new Matrix<BindingContainer<int>>(0, 0);
        }

        private void SetMatrixSize(ref Matrix<BindingContainer<int>> matrix, int width, int height)
        {
            matrix = new Matrix<BindingContainer<int>>(width, height, new BindingContainer<int>[width * height].Select(e => new BindingContainer<int>()));
        }
    }
}