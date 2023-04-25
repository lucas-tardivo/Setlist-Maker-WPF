using SetlistMaker.Services.Domain.Models;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using SetlistMaker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Setlist_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBandService _bandService;
        private readonly BandWindow _bandWindow;

        public MainWindow(IBandService bandService, 
                          BandWindow bandWindow)
        {
            InitializeComponent();

            _bandService = bandService;
            _bandWindow = bandWindow;

            Setup();
        }

        public void Setup()
        {
            cmbBand.Items.Clear();

            var bands = _bandService.GetBands();
            foreach(var band in bands) 
            {
                cmbBand.Items.Add(band.Name);
            }
        }

        private void mnuBandWindow_Click(object sender, EventArgs e)
        {
            _bandWindow.Show();
        }
    }
}
