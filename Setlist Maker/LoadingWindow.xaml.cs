using Microsoft.EntityFrameworkCore;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly EntityContext _entityContext;

        public LoadingWindow(MainWindow mainWindow, EntityContext entityContext)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            _entityContext = entityContext;
            Setup();
        }

        public void Setup()
        {
            var pendingMigrations = _entityContext.Database?.GetPendingMigrations()?.ToList() ?? new List<string>();

            if (pendingMigrations.Any())
            {
                _entityContext.Database.Migrate();
            }

            _mainWindow.Show();
            this.Close();
        }
    }
}
