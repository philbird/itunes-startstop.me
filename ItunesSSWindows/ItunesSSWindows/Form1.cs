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

        // How could this be improved
        /*
         
         *Add a dictionary for the stat lookup
         *Segment it to be in classes
         *Change it so it works with the XML rather than the COM object
         *Add lots of extra stats! 
         
         */

        // Some small debugging
        // If it is in debug then don't send stats. 
        public bool DebugMode = false;

        #region New WS's

        public startstop.AccessPoint StartStopAccess = new startstop.AccessPoint();
        public startstop.MessageResponse MessageResponse = new startstop.MessageResponse();
        public startstop.ValidatedUserInfo ValidatedUserInfo = new startstop.ValidatedUserInfo();
        public startstop.UserStatLog UserStat = new startstop.UserStatLog();

        #endregion


        // Developer APIKEY
        const string APIKey = "1b50f643-fb89-4d5a-8fcf-20ca96deef22";

        public Guid cNumberOfTunesInLibraryStatID = new Guid("5C79B7C9-A018-4E4E-977E-55303167377F");
        public Guid cMostPlayesOnaSong = new Guid("57109D18-B4D3-4DE8-B3C0-620FA09CEE01");
        public Guid cMostPlayedSong = new Guid("1F07A775-3D3E-4DFD-92CE-AF0443982F8B");
        public Guid cUnplayedTunes = new Guid("2AB00A23-FF5B-4B3E-A757-CA50381D54C3");
        public Guid cTotalNumberOfPlays = new Guid("B90A3777-03B0-4D38-9541-54AB6B6CE155");
        public Guid cLastUpdate = new Guid("67788B2F-D0D0-4F6F-9D73-644A8D5186D5");
        public Guid cTimePlayed = new Guid("3275EEA5-2ED4-48D0-B7D1-966A104076F1");
        public Guid cTimeUnPlayed = new Guid("7EAC85E1-3C86-408F-ACEE-0E5BBB1D95BE");

        // This is the record for the most played song. 
        public startstop.AudioContent oMostPlayedSong = new startstop.AudioContent();


        // This is the overview GUID for this application stat. 
        public Guid cStatOverview = new Guid("4C05FE37-EDCF-4315-A249-DEA3E6B4ECF8");


        // Release APIKEY - This should be used when creating releases as it only allows non-developer webservice interactions. 
        const string APIKeyRelease = "";

        // This should be where the itunes XML is when it's found
        public string iTunesXML = "";

        // This is the iTunes appID
        public const Int64 LinkedAppID = 17;

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show(@"
Welcome to the iTunes uploader. This is a pre-release which means it's not completely ready yet. However, it does work. It needs some more onscreen instructions. 

You need to have iTunes running and you should have a StartStop.Me account, enter your login details before you click RUN. 

"); 
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
            startstop.AccessPoint oAccessPoint = new startstop.AccessPoint();
            // Validate the user

            ValidatedUserInfo = oAccessPoint.LoginUser(APIKey, txbUserName.Text, txbPassword.Text);
            // Check to see if the user is validated
            if (ValidatedUserInfo.Validated) // User details invalid
            {

                // We should really access the iTunes XML file, but for now this will. 

                //iTunes classes
                iTunesAppClass itunes = new iTunesAppClass();
                IITLibraryPlaylist mainLibrary = itunes.LibraryPlaylist;
                IITTrackCollection tracks = mainLibrary.Tracks;
                IITFileOrCDTrack currTrack;

                int _numberOfTracks = tracks.Count;



                startstop.UserStatLog oStat = new startstop.UserStatLog();


                // Total number of tracks (This is done here, because, well, we decrement the number of tracks)
                oStat = new startstop.UserStatLog();
                oStat.Count = _numberOfTracks;
                oStat.DetailedStatGuid = cNumberOfTunesInLibraryStatID;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }

                //  oReturnMessage = oDevAPI.ExactStatUpdateForUserWithDayHistory(APIKey, _UserID, cNumberOfTunesInLibraryStatID, tracks.Count, 0,"");
                AddLine("Updated your startstop.me stats with the number of tracks in your iTunes library");

                // Total number of plays
                int _TotalNumberOfPlays = 0;

                // The highest played track
                int _MostPlayedCount = 0;
                string _MostPlayedTrack = "";

                // Number of unplayed tracks
                int _UnplayedTracks = 0;

                // Total time played
                TimeSpan oTimeTimePlayed = new TimeSpan(0, 0, 0);
                TimeSpan oTimeUnplayed = new TimeSpan(0, 0, 0);


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

                            _tracktime = new TimeSpan(0, int.Parse(_timeSplit[0]), int.Parse(_timeSplit[1]));
                        }
                        // Hours Minutes and seconds
                        if (_timeSplit.Length == 3)
                        {
                            _tracktime = new TimeSpan(int.Parse(_timeSplit[0]), int.Parse(_timeSplit[1]), int.Parse(_timeSplit[2]));
                        }


                        if (currTrack.PlayedCount > _MostPlayedCount)
                        {
                            _MostPlayedCount = currTrack.PlayedCount;
                            _MostPlayedTrack = currTrack.Name;
                            oMostPlayedSong.SongTitle = currTrack.Name;
                            oMostPlayedSong.AlbumnTitle = currTrack.Album;
                            oMostPlayedSong.Genre = currTrack.Genre;
                            oMostPlayedSong.Notes = "";
                            oMostPlayedSong.ReportingUserID = ValidatedUserInfo.UserID;
                            // Catch if there is an invalid date. 
                            if (currTrack.Year != 0)
                            {
                                oMostPlayedSong.Year = new DateTime(currTrack.Year, 1, 1, 0, 0, 0);
                            }
                        }

                        if (currTrack.Unplayed)
                        {
                            _UnplayedTracks++;
                            oTimeUnplayed = oTimeUnplayed.Add(_tracktime);
                        }
                        else
                        {
                            // Repeat through these to give us the total time
                            for (int i = 0; i < currTrack.PlayedCount; i++)
                            {
                                oTimeTimePlayed = oTimeTimePlayed.Add(_tracktime);
                            }
                        }
                    } // end track kind check. 
                }

                #region update the stats with startstop
                AddLine("Added total number of plays");



                // we should swap all this rhubarb for a dictionary, it doesn't need to be this complex. That way we can iterate through the dictionary. 
                // Anyway, for now, this will work. 



                // Total number of plays
                oStat = new startstop.UserStatLog();
                oStat.Count = _TotalNumberOfPlays;
                oStat.DetailedStatGuid = cTotalNumberOfPlays;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }

                // Unplayed tracks
                oStat = new startstop.UserStatLog();
                oStat.Count = _UnplayedTracks;
                oStat.DetailedStatGuid = cUnplayedTunes;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }

                // Most played tracks
                oStat = new startstop.UserStatLog();
                oStat.Count = _MostPlayedCount;
                oStat.DetailedStatGuid = cMostPlayesOnaSong;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }

                // Amount of time played
                oStat = new startstop.UserStatLog();
                oStat.Count = 0;
                oStat.Note = oTimeTimePlayed.Days + "d " + oTimeTimePlayed.Hours + "h " + oTimeTimePlayed.Minutes + "m";
                oStat.DetailedStatGuid = cTimePlayed;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }


                // Amount of time played
                oStat = new startstop.UserStatLog();
                oStat.Count = 0;
                oStat.Note = oTimeUnplayed.Days + "d " + oTimeUnplayed.Hours + "h " + oTimeUnplayed.Minutes + "m";
                oStat.DetailedStatGuid = cTimeUnPlayed;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }

                // Amount of time played
                oStat = new startstop.UserStatLog();
                oStat.Count = 0;
                oStat.Note = _MostPlayedTrack;
                oStat.DetailedStatGuid = cMostPlayedSong;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStatMusic(APIKey, oStat, oMostPlayedSong);
                }

                oStat = new startstop.UserStatLog();
                oStat.Count = 0;
                oStat.Note = DateTime.Now.ToString();
                oStat.DetailedStatGuid = cLastUpdate;
                oStat.DetailedStatOverviewGUID = cStatOverview;
                oStat.UserGuid = ValidatedUserInfo.UserGUID;
                if (!DebugMode)
                {
                    oAccessPoint.AddUserStat(APIKey, oStat);
                }


                AddLine("All done folks! Visit www.startstop.me to see your stats");
                #endregion
            }
            else
            {
                AddLine("User cannot be logged in");
                MessageBox.Show("Sorry your login details aren't correct");
            }

        }



        private void btnRunStats_Click(object sender, EventArgs e)
        {
            // We should really ask for a login here, before we attempt to PArse the lib.

            ParseiTunesXML();
        }


    }
}
