using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QoLApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KhmerPage : ContentPage
    {
        bool flipped = true;
        public KhmerPage()
        {
            InitializeComponent();

            ImgTemplate.Source = "khmerConsonant.jpg";
        }

        private void BtnFlip_Clicked(object sender, EventArgs e)
        {
            if (flipped == true)
            {
                ImgTemplate.Source = "khmerVowels.jpg";
                flipped = false;
            }
            else
            {
                ImgTemplate.Source = "khmerConsonant.jpg";
                flipped = true;
            }
        }
    }
}