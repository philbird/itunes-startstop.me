using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ItunesSSWindows
{
    public partial class Form1 : Form
    {

        public statdropws.DeveloperAPI oDevAPI = new statdropws.DeveloperAPI();
        const string APIKey = "1b50f643-fb89-4d5a-8fcf-20ca96deef22";
        public string iTunesXML = ""; 


        public Form1()
        {
            InitializeComponent();
        }

        public bool CheckAPIKey()
        {
            return oDevAPI.ValidateAPIKey(APIKey);                
        }

        public void AddLine(string Message)
        {
            txbStatus.Text += Message; 
            txbStatus.Text += System.Environment.NewLine;
        }

        /// <summary>
        /// Locate the ItunesXML
        /// </summary>
        public void LocateiTunesXML()
        {
            // Here we need to populate the string value with the location. 
        }

        /// <summary>
        /// Parse the iTunes XML and then upload the stats to the user account
        /// 
        /// </summary>
        public void ParseiTunesXML()
        {
            // Do some basic counts

            // First one should be number of songs in the library. 

            // We need a logged in user to add the stats to


        }



        private void btnRunStats_Click(object sender, EventArgs e)
        {
            AddLine(CheckAPIKey().ToString());
            LocateiTunesXML();
            ParseiTunesXML(); 
        }
    }
}
