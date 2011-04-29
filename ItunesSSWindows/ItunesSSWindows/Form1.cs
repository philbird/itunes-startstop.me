using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTunesLib; 

namespace ItunesSSWindows
{
    public partial class Form1 : Form
    {
        // Setup a connection to the statdrop webservices
        public statdropws.DeveloperAPI oDevAPI = new statdropws.DeveloperAPI();
        
        // Developer APIKEY
        const string APIKey = "1b50f643-fb89-4d5a-8fcf-20ca96deef22";

        const Int64 NumberOfTunesInLibraryStatID = 5; 

        // Release APIKEY - This should be used when creating releases as it only allows non-developer webservice interactions. 
        const string APIKeyRelease = ""; 
        
        // This should be where the itunes XML is when it's found
        public string iTunesXML = ""; 

        // This is the iTunes appID
        public const Int64 LinkedAppID = 17; 
       
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
            // We should really access the iTunes XML file, but for now this will. 

             //iTunes classes
             iTunesAppClass itunes = new iTunesAppClass();
             IITLibraryPlaylist mainLibrary = itunes.LibraryPlaylist;
             IITTrackCollection tracks = mainLibrary.Tracks;
             IITFileOrCDTrack currTrack;

             int _numberOfTracks = tracks.Count; 
   

            // First one should be number of songs in the library. 

            // We need a logged in user to add the stats to
            Int64 _UserID =  oDevAPI.AuthenticateUser(APIKey, txbUserName.Text, txbPassword.Text);
            if (_UserID != -1)
            {
                // The user can be authenticated so now we can add stats
                
                oDevAPI.ExactStatUpdateForUser(APIKey, _UserID, NumberOfTunesInLibraryStatID, tracks.Count, 0);
                AddLine("Updated your startstop.me stats with the number of tracks in your iTunes library"); 


                // Here we can go through all the files and update some more stats
                while (_numberOfTracks != 1)
                {
                    // Iterate throught the tracks. 
                    _numberOfTracks--;
                    currTrack = tracks[_numberOfTracks] as IITFileOrCDTrack;
                    
                }
                

            }
            else
            {
                AddLine("User cannot be logged in"); 
            }

        }



        private void btnRunStats_Click(object sender, EventArgs e)
        {
            if (CheckAPIKey())
            {
                // Using the apple COM for now.
                //LocateiTunesXML();

                ParseiTunesXML();
            }
            
               
        }

        /// <summary>
        /// This should be hidden from the user, it is used to setup stats. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeveloper_Click(object sender, EventArgs e)
        {
            // The startstop.me webservice isn't checking for dupes, so don't rerun this for now, but you can add new stats. 
           
            // oDevAPI.CreateDetailedStat(APIKey, "Songs In Library", LinkedAppID, "Number of songs in your iTunes Library",1,1); 
 
        }
    }
}
