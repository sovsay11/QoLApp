using QoLApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace QoLApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}