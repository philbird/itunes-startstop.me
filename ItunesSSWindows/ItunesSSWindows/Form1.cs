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

        #region deprecated services
        // Setup a connection to the statdrop webservices
        public statdropws.DeveloperAPI oDevAPI = new statdropws.DeveloperAPI();
        
        // Create a return message object to be used
        public statdropws.ReturnMessage oReturnMessage = new statdropws.ReturnMessage();
        #endregion 

        #region New WS's

        public startstop.AccessPoint StartStopAccess = new startstop.AccessPoint();
        public startstop.MessageResponse MessageResponse = new startstop.MessageResponse();
        public startstop.ValidatedUserInfo ValidatedUserInfo = new startstop.ValidatedUserInfo();
        public startstop.UserStat UserStat = new startstop.UserStat(); 

        #endregion 


        // Developer APIKEY
        const string APIKey = "1b50f643-fb89-4d5a-8fcf-20ca96deef22";

        const Guid cNumberOfTunesInLibraryStatID = new Guid("5C79B7C9-A018-4E4E-977E-55303167377F"); 
        const Guid cMostPlayesOnaSong = new Guid("57109D18-B4D3-4DE8-B3C0-620FA09CEE01"); 
        const Guid cMostPlayedSong = new Guid("1F07A775-3D3E-4DFD-92CE-AF0443982F8B"); 
        const Guid cUnplayedTunes = new Guid("2AB00A23-FF5B-4B3E-A757-CA50381D54C3"); 
        const Guid cTotalNumberOfPlays = new Guid("B90A3777-03B0-4D38-9541-54AB6B6CE155"); 
        const Guid cLastUpdate = new Guid("67788B2F-D0D0-4F6F-9D73-644A8D5186D5"); 
        const Guid cTimePlayed = new Guid("3275EEA5-2ED4-48D0-B7D1-966A104076F1");
        const Guid cTimeUnPlayed = new Guid("7EAC85E1-3C86-408F-ACEE-0E5BBB1D95BE"); 


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
           oReturnMessage = oDevAPI.ValidateAPIKey(APIKey);
           return oReturnMessage.Success;  
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
            oReturnMessage = oDevAPI.AuthenticateUser(APIKey, txbUserName.Text, txbPassword.Text);
            Int64 _UserID = -1; 
            
            if (oReturnMessage.Success)
            {
                // Since we're reusing the return message, we need to store the userID into it's local variable. 
                _UserID = oReturnMessage.ReturnedID; 
                // The user can be authenticated so now we can add stats
                
                oReturnMessage = oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cNumberOfTunesInLibraryStatID, tracks.Count, 0,"");
                AddLine("Updated your startstop.me stats with the number of tracks in your iTunes library"); 

                // Total number of plays
                int _TotalNumberOfPlays = 0; 

                // The highest played track
                int _MostPlayedCount = 0;
                string _MostPlayedTrack = ""; 

                // Number of unplayed tracks
                int _UnplayedTracks = 0; 

                // Total time played
                TimeSpan oTimeTimePlayed = new TimeSpan(0,0,0);
                TimeSpan oTimeUnplayed = new TimeSpan(0,0,0); 
                

                // Here we can go through all the files and update some more stats
                while (_numberOfTracks != 1)
                {
                    // Iterate throught the tracks. 
                    _numberOfTracks--;
                    currTrack = tracks[_numberOfTracks] as IITFileOrCDTrack;

                    // Check this is a song
                    if (currTrack.KindAsString.Contains("audio"))
                    {

                        _TotalNumberOfPlays += currTrack.PlayedCount;

                        string[] _timeSplit = currTrack.Time.Split(':');
                        TimeSpan _tracktime = new TimeSpan();
                        // Minutes and Seconds
                        if (_timeSplit.Length == 2)
                        {
                            
                            _tracktime = new TimeSpan(0, int.Parse(_timeSplit[1]), int.Parse(_timeSplit[0]));
                        }
                        // Hours Minutes and seconds
                        if (_timeSplit.Length == 3)
                        {
                            _tracktime = new TimeSpan(int.Parse(_timeSplit[2]), int.Parse(_timeSplit[1]), int.Parse(_timeSplit[0]));
                        }


                        if (currTrack.PlayedCount > _MostPlayedCount)
                        {
                            _MostPlayedCount = currTrack.PlayedCount;
                            _MostPlayedTrack = currTrack.Name;
                        }





                        if (currTrack.Unplayed)
                        {
                            _UnplayedTracks++;
                           oTimeUnplayed =  oTimeUnplayed.Add(_tracktime);
                        }
                        else
                        {
                            oTimeTimePlayed = oTimeTimePlayed.Add(_tracktime);
                        }
                    } // end track kind check. 
                }

                #region update the stats with startstop
                AddLine("Added total number of plays");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cTotalNumberOfPlays, _TotalNumberOfPlays, 0, "");
                AddLine("Total number of unplayed");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cUnplayedTunes, _UnplayedTracks, 0, "");
                AddLine("Most plays on a song");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cMostPlayesOnaSong, _MostPlayedCount, 0, "");
                AddLine("Most played song");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cMostPlayedSong, 0, 0, _MostPlayedTrack);
                AddLine("Last Updated");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cLastUpdate, 0, 0, DateTime.Now.ToString());
                AddLine("Amount of Time Played");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cTimePlayed, 0, 0, oTimeTimePlayed.Days + "d " + oTimeTimePlayed.Hours + "h " + oTimeTimePlayed.Minutes + "m");
                AddLine("Amount of Time Unplayed");
                oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cTimeUnPlayed, 0, 0, oTimeUnplayed.Days + "d " + oTimeUnplayed.Hours + "h " + oTimeUnplayed.Minutes + "m");
                AddLine("All done folks! Visit www.startstop.me to see your stats"); 
                #endregion 
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
           
            /*
            oDevAPI.CreateDetailedStat(APIKey, "Songs In Library", LinkedAppID, "Number of songs in your iTunes Library",1,1); 

            oDevAPI.CreateDetailedStat(APIKey, "Most plays on a song", LinkedAppID, "The song with the most plays", 1, 1);
            oDevAPI.CreateDetailedStat(APIKey, "Most played song", LinkedAppID, "The song with the most plays", 1, 1);
            oDevAPI.CreateDetailedStat(APIKey, "Number of unplayed tracks", LinkedAppID, "Number of songs which have never been played", 1, 1);
            oDevAPI.CreateDetailedStat(APIKey, "The number of plays", LinkedAppID, "The number of times tracks have been played in total", 1, 1); 
            */
            //oDevAPI.CreateDetailedStat(APIKey, "First time", LinkedAppID, "The date you first uploaded iTunesStats", 1, 1);
            //oDevAPI.CreateDetailedStat(APIKey, "Last Update", LinkedAppID, "The last time you ran the updater", 1, 1); 
            //oDevAPI.CreateDetailedStat(APIKey, "Amount of time played", LinkedAppID, "The total amount of time of all the plays", 1, 1);
            //oDevAPI.CreateDetailedStat(APIKey, "Amount of time unplayed", LinkedAppID, "The amount of time of unplayed tracks", 1, 1);
        }
    }
}
