using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CitiesTableApp.Models
{
    public class CitiesManager
    {
        #region Singleton

        static readonly Lazy<CitiesManager> lazy = new Lazy<CitiesManager>(() => new CitiesManager());
        public static CitiesManager SharedInstance { get => lazy.Value; }

        #endregion

        #region ClassVariables

        HttpClient httpClient;
        Dictionary<string, List<string>> cities;

        #endregion

        #region Constructors
        CitiesManager()
        {
            httpClient = new HttpClient();
        }
        #endregion

        #region Events

        public event EventHandler<CitiesEventArgs> CitiesFetched;
        public event EventHandler<EventArgs> CitiesFetchedFailed;

        #endregion

        #region Public Functionality

        public Dictionary<string, List<string>> GetDefaultCities()
        {
            var citiesJson = File.ReadAllText("incompletecities.json");
            //Parse JSON using Json.Net or Newtonsoft --Newtonsoft.Json 9.0.1
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
        }

        //Download JSON
        public void FetchCities()
        {

            Task.Factory.StartNew(FetchCitiesAsync);

            async Task FetchCitiesAsync()
            {
                try
                {
                    if (CitiesFetched == null)
                        return;

                    var citiesJson = await httpClient.GetStringAsync("https://dl.dropbox.com/s/0adq8yw6vd5r6bj/cities.json?=dl=0");
                    cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);

                    var e = new CitiesEventArgs(cities);
                    CitiesFetched(this, e);
                    //Notify controller, these are available now.
                    //(Events, Delegates)Using events
                    //(NSNotificationCenter)Using notifications
                    //(Only when in ViewController)Using Unwind Segue
                }
                catch (Exception ex)
                {
                    if (CitiesFetchedFailed == null)
                        return;

                    //Notify controller, Exception.
                    //(Events, Delegates)Using events
                    //(NSNotificationCenter)Using notifications
                    //(Only when in ViewController)Using Unwind Segue

                    CitiesFetchedFailed(this, new EventArgs());
                }
            }

            #endregion
        }

        public class CitiesEventArgs : EventArgs
        {

            public Dictionary<string, List<string>> Cities { get; private set; }

            public CitiesEventArgs(Dictionary<string, List<string>> cities)
            {
                Cities = cities;
            }
        }
    }
}
