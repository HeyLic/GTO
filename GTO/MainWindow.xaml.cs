using GTO.Pages;
using System.Windows;

namespace GTO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameManager.MainFrame = MainFrame;
            FrameManager.MainFrame.Navigate(new PageAutorisation());
        }
    }
}
