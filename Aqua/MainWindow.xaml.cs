using Aqua.Views;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Aqua
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private string _accent = "Blue";
        private string _apptheme = "BaseLight";

        DonateDialog donateDialog = new DonateDialog();
        AboutDialog aboutDialog = new AboutDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            donateDialog.btnClose.Click += btnClose_Donate;
            await this.ShowMetroDialogAsync(donateDialog);
        }

        private async void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            aboutDialog.btnClose.Click += btnClose_About;
            await this.ShowMetroDialogAsync(aboutDialog);
        }

        private void btnClose_About(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(aboutDialog);
        }

        private void btnClose_Donate(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(donateDialog);
        }

        // 打印
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                double pageWidth = dialog.PrintableAreaWidth - 80;
                double pageHeight = dialog.PrintableAreaHeight - 100;
                double actWidth = Print_Area.DesiredSize.Width;
                double actHeight = Print_Area.DesiredSize.Height;
                double scaleWidth = pageWidth / actWidth;
                double scaleHeight = pageHeight / actHeight;
                double scale = Math.Min(scaleWidth, scaleHeight);
                double width = actWidth * scale;
                double height = actHeight * scale;

                VisualBrush visualBrush = new VisualBrush(Print_Area);
                Rectangle printRect = new Rectangle { Fill = visualBrush, Stretch = Stretch.Uniform, Width = width, Height = height };
                printRect.Measure(new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight));
                printRect.Arrange(new Rect(new Point(0, 30), new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight)));
                printRect.UpdateLayout();
                try
                {
                    dialog.PrintVisual(printRect, "TyroTools-Aqua：打印");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // 响应明暗模式切换
        private void btnTheme_IsCheckedChanged(object sender, EventArgs e)
        {
            if (this.btnTheme.IsChecked == false)
                this._apptheme = "BaseLight";
            else
                this._apptheme = "BaseDark";
            ChangeTheme(_accent, this._apptheme);
        }

        // 改变主题
        private void ChangeTheme(string accent, string apptheme)
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                                    ThemeManager.GetAccent(accent),
                                                    ThemeManager.GetAppTheme(apptheme));
        }
    }
}
