using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QoLApp.Models;

namespace QoLApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversionPage : ContentPage
    {
        // determines which measurements to use
        List<string> ConversionTypes = new List<string> { "Weight", "Length", "Volume" };

        // this determines what measurements we can use for the weight dropdown
        List<string> WeightTypes = new List<string> { "lbs", "ounces", "g", "mg", "kg" };

        // this determines what measurements we can use for the length ratio
        List<string> LengthTypes = new List<string> { "inches", "feet", "miles", "meters", "cm", "km" };

        // this determines what measurements we can use for the volume ratio
        List<string> VolumeTypes = new List<string> { "fl oz", "cups", "gallons", "liters", "ml" };

        // Conversion goes from x to y (row to column), so 1 ounce = 0.0625 lbs
        // conversion ratio chart for lbs, ounces, g (g fully compatible with varying measures)
        double[,] weightRatio = new double[3, 3]
        {
            // lbs, ounces, g
            { 1, 16, 453.59237}, // lbs
            { 0.0625, 1, 28.3495231}, // ounces
            { 0.00220462, 0.03527396, 1}, // g
        };

        // conversion ratio chart for lengths (inches, feet, miles, meters)
        double[,] lengthRatio = new double[4, 4]
        {
            // inches, feet, miles, meters
            { 1, 0.08333333, 0.00001578, 0.0254}, // inches
            { 12, 1, 0.00018939, 0.3048}, // feet
            { 63360, 5280, 1, 1609.344}, // miles
            { 39.3700787, 3.2808399, 0.00062137, 1}, // meters
        };

        // conversion ratio chart for liquids, take care of this later
        double[,] volumeRatio = new double[4, 4]
        {
            // fl oz, cups, gallons, liters
            { 1, 0.125, 0.0078125, 0.02957353}, // fl oz
            { 8, 1, 0.0625, 0.23658824}, // cups
            { 128, 16, 1, 3.78541178}, // gallons
            { 33.8140227, 4.22675284, 0.26417205, 1}, // liters
        };

        public ConversionPage()
        {
            InitializeComponent();

            // load the pickers
            PckrType.ItemsSource = ConversionTypes;
            PckrUnits.ItemsSource = WeightTypes;
            PckrConvertedUnits.ItemsSource = WeightTypes;

            PckrType.SelectedIndex = 0;
            PckrUnits.SelectedIndex = 0;
            PckrConvertedUnits.SelectedIndex = 0;
        }

        private double ConvertUnits(double units, ConversionValues values)
        {
            // determine the ratio from the md arrays above (need two index values)
            double ratio;
            if (values.ConversionType == "Weight")
            {
                ratio = weightRatio[values.FirstIndex, values.SecondIndex];
            }
            else if (values.ConversionType == "Length")
            {
                ratio = lengthRatio[values.FirstIndex, values.SecondIndex];
            }
            else
            {
                ratio = volumeRatio[values.FirstIndex, values.SecondIndex];
            }
            // formula goes here
            units = (units * ratio) * values.Multiplier;
            return Math.Round(units, 6);
        }

        private void PckrUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            double units;
            if (EntUnits.Text != null && EntUnits.Text != "" && EntUnits.Text != ".")
            {
                // grab the provided units, should be fine as is
                units = double.Parse(EntUnits.Text);

                // send the conversion values into the class
                ConversionValues values = new ConversionValues(PckrUnits.SelectedItem.ToString(), PckrConvertedUnits.SelectedItem.ToString(), PckrType.SelectedItem.ToString());

                // convert the units using the units and values
                units = ConvertUnits(units, values);

                // show the converted units, leave as is
                EntConvertedUnits.Text = units.ToString();
            }
        }

        private void PckrConvertedUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            double units;
            if (EntUnits.Text != null && EntUnits.Text != "" && EntUnits.Text != ".")
            {
                // units
                units = double.Parse(EntUnits.Text);

                ConversionValues values = new ConversionValues(PckrUnits.SelectedItem.ToString(), PckrConvertedUnits.SelectedItem.ToString(), PckrType.SelectedItem.ToString());

                // convert the units
                units = ConvertUnits(units, values);

                // show the converted units
                EntConvertedUnits.Text = units.ToString();
            }
        }

        private void EntUnits_TextChanged(object sender, TextChangedEventArgs e)
        {
            double units;
            if (EntUnits.Text != null && EntUnits.Text != "" && EntUnits.Text != ".")
            {
                // units
                units = double.Parse(EntUnits.Text);

                ConversionValues values = new ConversionValues(PckrUnits.SelectedItem.ToString(), PckrConvertedUnits.SelectedItem.ToString(), PckrType.SelectedItem.ToString());

                // convert the units
                units = ConvertUnits(units, values);

                // show the converted units
                EntConvertedUnits.Text = units.ToString();
            }
        }

        // Reset the pickers, select new type conversions
        private void PckrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntUnits.Text = "";
            EntConvertedUnits.Text = "";
            if (PckrType.SelectedIndex == 0)
            {
                PckrUnits.ItemsSource = WeightTypes;
                PckrConvertedUnits.ItemsSource = WeightTypes;
                PckrUnits.SelectedIndex = 0;
                PckrConvertedUnits.SelectedIndex = 0;
            }
            else if (PckrType.SelectedIndex == 1)
            {
                PckrUnits.ItemsSource = LengthTypes;
                PckrConvertedUnits.ItemsSource = LengthTypes;
                PckrUnits.SelectedIndex = 0;
                PckrConvertedUnits.SelectedIndex = 0;
            }
            else
            {
                PckrUnits.ItemsSource = VolumeTypes;
                PckrConvertedUnits.ItemsSource = VolumeTypes;
                PckrUnits.SelectedIndex = 0;
                PckrConvertedUnits.SelectedIndex = 0;
            }
        }
    }
}