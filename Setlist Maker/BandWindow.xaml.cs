using SetlistMaker.Framework.Exceptions;
using SetlistMaker.Services.Domain.Models;
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
using System.Windows.Shapes;

namespace Setlist_Maker
{
    /// <summary>
    /// Interaction logic for BandWindow.xaml
    /// </summary>
    public partial class BandWindow : Window
    {
        private readonly IBandService _bandService;
        private List<Guid> mBandIds = new List<Guid>();

        public BandWindow(IBandService bandService)
        {
            InitializeComponent();

            _bandService = bandService;

            Setup();
        }

        private void Setup()
        {
            PopulateList();
        }

        private void PopulateList()
        {
            lstBands.Items.Clear();
            mBandIds.Clear();

            var bands = _bandService.GetBands();
            foreach (var band in bands)
            {
                lstBands.Items.Add(band.Name);
                mBandIds.Add(band.Id);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _bandService.AddBand(new Band()
                {
                    Name = txtName.Text
                });

                PopulateList();
            }
            catch(BusinessException ex)
            {
                MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void lstBand_SelectionChanged(object sender, EventArgs e)
        {
            if (lstBands.SelectedIndex >= 0)
            {
                btnEdit.IsEnabled = true;
                btnRemove.IsEnabled = true;
                txtName.Text = lstBands.Items[lstBands.SelectedIndex].ToString();
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnRemove.IsEnabled = false;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstBands.SelectedIndex >= 0)
            {
                try
                {
                    _bandService.EditBand(new Band()
                    {
                        Id = mBandIds[lstBands.SelectedIndex],
                        Name = txtName.Text
                    });

                    PopulateList();
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstBands.SelectedIndex >= 0)
            {
                try
                {
                    _bandService.RemoveBand(mBandIds[lstBands.SelectedIndex]);
                    txtName.Text = string.Empty;

                    PopulateList();
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
