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

namespace PRB.PE01.Reservatiesysteem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Naam;
        int Nummer;
        decimal TotaalBedrag;
        decimal WachtLijst;
        int Geweigerd;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulLstBeginUurEnEindUur();
            lblBevestigdeReservaties.Content = $"Bevestigde reservaties: 0" ;
            lblGeweigerderReservaties.Content = $"Geweigerde reservaties: 0";
            lblWachtLijst.Content = $"Wachtlijst: 0";
            lblInkomsten.Content = $"Inkomsten: 0";

            Nummer = 0;
            TotaalBedrag = 0;
            WachtLijst = 0;
            Geweigerd = 0;
        }
        void VulLstBeginUurEnEindUur()
        {
            cmbBeginUur.Items.Add ("8");
            cmbBeginUur.Items.Add("9");
            cmbBeginUur.Items.Add("10");
            cmbBeginUur.Items.Add("11");
            cmbBeginUur.Items.Add("12");
            cmbBeginUur.Items.Add("13");
            cmbBeginUur.Items.Add("14");
            cmbBeginUur.Items.Add("15");
            cmbBeginUur.Items.Add("16");
            cmbBeginUur.Items.Add("17");

            cmbEindUur.Items.Add("8");
            cmbEindUur.Items.Add("9");
            cmbEindUur.Items.Add("10");
            cmbEindUur.Items.Add("11");
            cmbEindUur.Items.Add("12");
            cmbEindUur.Items.Add("13");
            cmbEindUur.Items.Add("14");
            cmbEindUur.Items.Add("15");
            cmbEindUur.Items.Add("16");
            cmbEindUur.Items.Add("17");

        }

        private void btnReserveer_Click(object sender, RoutedEventArgs e)
        {
            //Declaratie van variabelen
            decimal bedrag;

            //Inlezen van input
            Naam = txtNaam.Text;
            decimal korting = decimal.Parse(txtKorting.Text);
            decimal einduur = decimal.Parse(cmbEindUur.Text);
            decimal beginuur = decimal.Parse(cmbBeginUur.Text);

            //Uitvoeren van berekeningen/bijwerkingen via methoden
            bedrag = (einduur - beginuur) * 7 - korting;

            string reservatieTekst = $"Reservatie van {Naam} van {cmbBeginUur.SelectedItem} tot {cmbEindUur.SelectedItem}" +
                $" met €{korting} korting voor €{bedrag}";

            lblWachtLijst.Content = $"Wachtlijst: {++WachtLijst}";

            //Feedback geven
            lstFeedBack.Items.Add(reservatieTekst);
        }

       private decimal SeperatePrice(string selectedString)
        {
            return decimal.Parse(selectedString.Split(" ").Last().Substring(1));
        }

        private void btnAanvaard_Click(object sender, RoutedEventArgs e)
        {
            lblBevestigdeReservaties.Content = $"Bevestigde reservaties: {++Nummer}";
            TotaalBedrag = TotaalBedrag + SeperatePrice(lstFeedBack.SelectedItem.ToString());
            lblInkomsten.Content = $"Inkomsten:" + TotaalBedrag;
            lstFeedBack.Items.RemoveAt(lstFeedBack.SelectedIndex);
            VerminderdeWachtlijst();
        }

        private void btnWeiger_Click(object sender, RoutedEventArgs e)
        {
            VerminderdeWachtlijst();
            lblGeweigerderReservaties.Content = $"Geweigerde reservaties: {++Geweigerd}";
            lstFeedBack.Items.RemoveAt(lstFeedBack.SelectedIndex);
        }

        private void VerminderdeWachtlijst()
        {
            lblWachtLijst.Content = $"Wachtlijst: {--WachtLijst}";
        }
    }
}
