using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Football_API.Models.Models_Fixture;
using Football_API.Models.Models_Odds;
using Football_API.Models;
using Newtonsoft.Json;

namespace FootballAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class OddsController : ControllerBase
    {
        API API = new API();
        System.Timers.Timer aTimer = new System.Timers.Timer();
        public static List<List_LeagueOddsFixture> Temp_List_LeagueOddsFixture = new List<List_LeagueOddsFixture> { };
        public static List<RootObjectOdds> ObjectOddsAllPaging1 = new List<RootObjectOdds> { };


        #region ================ Public Function ================

        [HttpGet]
        public ActionResult<IEnumerable<Football_API.Models.Models_Fixture.Fixture>> Test_Get_AllSoccerLeague_ThisDay()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            string JsonStr_Fixture = GetAllFixtureThisDay(date);
            List<RootObjectFixture> APIJsonObjectFixture = JsonConvert.DeserializeObject<List<RootObjectFixture>>(JsonStr_Fixture);
            return APIJsonObjectFixture[0].api.fixtures.ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<List_LeagueOddsFixture>> GetOdds_SoccerHighlight_ThisDay()
        {
            string[] Highlight_Country = { };
            List<List_LeagueOddsFixture> Ret_LeagureOddsFixture = new List<List_LeagueOddsFixture>();
            List<List_LeagueOddsFixture> Ret_LeagureOddsFixtureHot = new List<List_LeagueOddsFixture>();
            List<List_LeagueOddsFixture> Ret_LeagureOddsFixtureNotHot = new List<List_LeagueOddsFixture>();
            int CountAll = Temp_List_LeagueOddsFixture.Count;
            for (int i = 0; i < CountAll; i++)
            {
                if (Temp_List_LeagueOddsFixture[i].LeagueID == "524")
                {
                    Ret_LeagureOddsFixtureHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
                else if (Temp_List_LeagueOddsFixture[i].LeagueID == "891")
                {
                    Ret_LeagureOddsFixtureHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
                else if (Temp_List_LeagueOddsFixture[i].LeagueID == "775")
                {
                    Ret_LeagureOddsFixtureHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
                else if (Temp_List_LeagueOddsFixture[i].LeagueID == "754")
                {
                    Ret_LeagureOddsFixtureHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
                else if (Temp_List_LeagueOddsFixture[i].LeagueID == "525")
                {
                    Ret_LeagureOddsFixtureHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
                else
                {
                    Ret_LeagureOddsFixtureNotHot.Add(Temp_List_LeagueOddsFixture[i]);
                }
            }

            if (Ret_LeagureOddsFixtureHot.Count > 0)
            {
                foreach (var DataLeagueHot in Ret_LeagureOddsFixtureHot)
                {
                    Ret_LeagureOddsFixture.Add(DataLeagueHot);
                }
                foreach (var DataLeagueNotHot in Ret_LeagureOddsFixtureNotHot)
                {
                    Ret_LeagureOddsFixture.Add(DataLeagueNotHot);
                }
            }
            else
            {
                foreach (var DataLeagueNotHot in Ret_LeagureOddsFixtureNotHot)
                {
                    Ret_LeagureOddsFixture.Add(DataLeagueNotHot);
                }
            }

            // switch (DateTime.Now.DayOfWeek)
            // {
            //     case DayOfWeek.Monday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Germany", "Portugal", "Turkey", "Denmark", "Netherlands", "Scotland", "Ireland", "Northern-Ireland" };
            //         break;
            //     case DayOfWeek.Tuesday:
            //         Highlight_Country = new string[] { "England", "Germany", "Spain", "Italy" , "France"};
            //         break;
            //     case DayOfWeek.Wednesday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Turkey", "Scotland" };
            //         break;
            //     case DayOfWeek.Thursday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Turkey" };
            //         break;
            //     case DayOfWeek.Friday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Germany", "Spain", "France", "Netherlands", "Portugal", "Ireland", "Northern-Ireland", "Italy" };
            //         break;
            //     case DayOfWeek.Saturday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Germany", "Italy", "Spain", "France", "Netherlands", "Portugal" };
            //         break;
            //     case DayOfWeek.Sunday:
            //         Highlight_Country = new string[] { "World", "England", "Thailand", "Germany", "Italy", "Spain", "France", "Netherlands", "Portugal", "Denmark", "Switzerland", "Turkey", "Belgium", "Scotland" };
            //         break;
            // }
            // //Loop country
            // foreach (var country in Highlight_Country)
            // {
            //     //Loop all fixture for check with country
            //     foreach (var eachleagueOdd in Temp_List_LeagueOddsFixture)
            //     {
            //         if (eachleagueOdd.LeagueCountry.ToLower() == country.ToLower())
            //         {
            //             Ret_LeagureOddsFixture.Add(eachleagueOdd);
            //         }
            //     }
            // }
            return Ret_LeagureOddsFixture;
        }

        [HttpGet]
        public ActionResult<IEnumerable<List_LeagueOddsFixture>> GetAllOddsDay()
        {
            return Temp_List_LeagueOddsFixture;
        }

        [HttpGet("{leagueID}")]
        public ActionResult<List_LeagueOddsFixture> GetOdds_SoccerByLeague_ThisDay(string leagueID)
        {
            var Ret_LeagureOddsFixture = Temp_List_LeagueOddsFixture.FirstOrDefault(it => it.LeagueID == leagueID);
            return Ret_LeagureOddsFixture;
        }

        [HttpGet("{country}")]
        public ActionResult<IEnumerable<List_LeagueOddsFixture>> GetOdds_SoccerByCountry_ThisDay(string country)
        {
            var Ret_LeagureOddsFixture = Temp_List_LeagueOddsFixture.Where(it => it.LeagueCountry.ToLower() == country.ToLower()).ToList();
            return Ret_LeagureOddsFixture;
        }

        [HttpPost]
        public ActionResult<IEnumerable<List_LeagueOddsFixture>> GetOdds_SoccerManyLeague_ThisDay([FromBody] string[] AllLeague)
        {
            List<List_LeagueOddsFixture> Ret_LeagureOddsFixture = new List<List_LeagueOddsFixture>();

            foreach (string leagueID in AllLeague)
            {
                var LeagureOddsFixture = Temp_List_LeagueOddsFixture.FirstOrDefault(it => it.LeagueID == leagueID);
                if (LeagureOddsFixture != null)
                {
                    Ret_LeagureOddsFixture.Add(LeagureOddsFixture);
                }
            }
            return Ret_LeagureOddsFixture.OrderBy(c => c.LeagueCountry).ToList();
        }

        [HttpGet("{leagueID}")]
        public ActionResult<IEnumerable<RootObjectOdds>> GetALLFixtureByLeagueIDtest(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1";
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            int total = APIJsonObjectOdds2[0].api.paging.total;
            return APIJsonObjectOdds2;
        }

        [HttpGet("{leagueID}")]
        public ActionResult<IEnumerable<RootObjectOdds>> GetALLFixtureByLeagueIDtest02(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=1";
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            int total = APIJsonObjectOdds2[0].api.paging.total;
            for (int i = 2; i <= total; i++)
            {
                string URL2 = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=" + i.ToString();
                string getresult01 = API.callAPIService(URL2);
                List<RootObjectOdds> APIJsonObjectOdds02 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult01);
                APIJsonObjectOdds2.AddRange(APIJsonObjectOdds02);
            }
            return APIJsonObjectOdds2;
        }

        [HttpGet("{leagueID}")]
        public ActionResult<IEnumerable<RootObjectOdds>> GetALLFixtureByLeagueIDtestReturnString(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=1";
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            int total = APIJsonObjectOdds2[0].api.paging.total;
            for (int i = 2; i <= total; i++)
            {
                string URL2 = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=" + i.ToString();

                string getresult01 = API.callAPIService(URL2);
                List<RootObjectOdds> APIJsonObjectOdds02 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult01);

                APIJsonObjectOdds2[0].api.odds.AddRange(APIJsonObjectOdds02[0].api.odds);
            }

            string JsonObjectOddsString = "";
            JsonObjectOddsString = JsonConvert.SerializeObject(APIJsonObjectOdds2);
            List<RootObjectOdds> APIJsonObjectOdds03 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(JsonObjectOddsString);
            // string getresult02 = getresult;
            return APIJsonObjectOdds03;
        }


        [HttpGet("{FixtureID}")]
        public ActionResult<IEnumerable<RootObjectOdds>> GetALLFixtureByFixtureIDtest(string FixtureID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/fixture/" + FixtureID;
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds3 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            return APIJsonObjectOdds3;
        }

        [HttpGet]
        public string Getfixtureday()
        {
            string test = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            test = GetAllFixtureThisDay(date);
            return test;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RootObjectOdds>> GetOddsday()
        {
            // string test = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/date/" + date;
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            return APIJsonObjectOdds2;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RootObjectOdds>> GetOddsdaypaging()
        {
            // string test = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/date/" + date;
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            int total = APIJsonObjectOdds2[0].api.paging.total;
            for (int i = 2; i <= total; i++)
            {
                string URL2 = "https://api-football-v1.p.rapidapi.com/v2/odds/date/" + date + "/label/1?page=" + i.ToString();
                string getresult01 = API.callAPIService(URL2);
                List<RootObjectOdds> APIJsonObjectOdds02 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult01);
                APIJsonObjectOdds2.AddRange(APIJsonObjectOdds02);
            }
            // APIJsonObjectOdds2[1].api.odds.
            return APIJsonObjectOdds2;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<RootObjectFixture>> GetALLFixtureInDayTest()
        // // public string Getdatafix()
        // {
        //     string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
        //     string URL = "https://api-football-v1.p.rapidapi.com/v2/fixtures/date/" + date.ToString();
        //     string getresult2 = API.callAPIService(URL);
        //     List<RootObjectFixture> APIJsonObjectFixture2 = JsonConvert.DeserializeObject<List<RootObjectFixture>>(getresult2);
        //     return APIJsonObjectFixture2;
        //     // return getresult2;
        // }

        [HttpGet]
        public void StartGetData_OddsFixture()
        {
            GetData_LeagueOdds(); //รอบแรกหลังจากเริ่ม run function
            int second = 3600; //1 hr
            aTimer.Interval = second * 1000;
            aTimer.Elapsed += Interval_GetData_LeagueOdds;
            aTimer.Start();
        }
        #endregion

        #region ================ Private Function ================
        private string GetFixtureByLeagueIDwithDate(string leagueID, string date)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/fixtures/league/" + leagueID + "/" + date;
            return API.callAPIService(URL);
        }

        private string GetALLFixtureByLeagueID(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/fixtures/league/" + leagueID;
            return API.callAPIService(URL);
        }

        private string GetOddByLeagueID(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1";
            return API.callAPIService(URL);
        }

        private string GetOddByFixtureID(string FixtureID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/fixture/" + FixtureID + "/label/1";
            return API.callAPIService(URL);
        }

        private string GetAllFixtureThisDay(string date)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/fixtures/date/" + date;
            return API.callAPIService(URL);
        }

        private string GetObjectOddLeagueIDAllPaging(string leagueID)
        {
            string URL = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=1";
            string getresult = API.callAPIService(URL);
            List<RootObjectOdds> APIJsonObjectOdds2 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult);
            int total = APIJsonObjectOdds2[0].api.paging.total;
            for (int i = 2; i <= total; i++)
            {
                string URL2 = "https://api-football-v1.p.rapidapi.com/v2/odds/league/" + leagueID + "/label/1?page=" + i.ToString();

                string getresult01 = API.callAPIService(URL2);
                List<RootObjectOdds> APIJsonObjectOdds02 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(getresult01);

                APIJsonObjectOdds2[0].api.odds.AddRange(APIJsonObjectOdds02[0].api.odds);
            }

            string JsonObjectOddsString = "";
            JsonObjectOddsString = JsonConvert.SerializeObject(APIJsonObjectOdds2);
            // List<RootObjectOdds> APIJsonObjectOdds03 = JsonConvert.DeserializeObject<List<RootObjectOdds>>(JsonObjectOddsString);
            // string getresult02 = getresult;
            return JsonObjectOddsString;
        }

        private void Interval_GetData_LeagueOdds(Object source, System.Timers.ElapsedEventArgs e)
        {
            GetData_LeagueOdds();
        }

        private void GetData_LeagueOdds()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            double perHome = 0, perDraw = 0, perAway = 0;
            string OddsMaker = "", JsonStr_Fixture = "", JsonStr_Odds = "";
            List<List_LeagueOddsFixture> Ret_LeagureOddsFixture = new List<List_LeagueOddsFixture>();

            //Get Json object Fixture
            JsonStr_Fixture = GetAllFixtureThisDay(date);
            List<RootObjectFixture> APIJsonObjectFixture = JsonConvert.DeserializeObject<List<RootObjectFixture>>(JsonStr_Fixture);

            if (APIJsonObjectFixture[0].api.fixtures.Count > 0)
            {
                //Group by leagueID in all Fixture
                var groupLeagueID = from lstfixture in APIJsonObjectFixture[0].api.fixtures
                                    group lstfixture by lstfixture.league_id into newGroup
                                    orderby newGroup.Key
                                    select newGroup.Key.ToString();

                foreach (string ID in groupLeagueID)
                {
                    List<LeagueOddsFixture> ListLeagueFixOdds = new List<LeagueOddsFixture>();

                    //Get Json object Odds
                    // List<RootObjectOdds> APIJsonObjectOdds = new List<RootObjectOdds>();
                    // APIJsonObjectOdds.AddRange(GetObjectOddLeagueIDAllPaging(ID));
                    JsonStr_Odds = GetObjectOddLeagueIDAllPaging(ID);
                    List<RootObjectOdds> APIJsonObjectOdds = JsonConvert.DeserializeObject<List<RootObjectOdds>>(JsonStr_Odds);

                    if (APIJsonObjectOdds[0].api.odds.Count > 0)
                    {
                        foreach (var eachLstOdds in APIJsonObjectOdds[0].api.odds)
                        {
                            //หาตัวที่มี FixtureID เดียวกัน
                            var FixtureMatch = APIJsonObjectFixture[0].api.fixtures.FirstOrDefault(it => it.fixture_id == eachLstOdds.fixture.fixture_id);
                            if (FixtureMatch != null)
                            {
                                perHome = 0;
                                perDraw = 0;
                                perAway = 0;
                                OddsMaker = "";
                                Value oddHome = new Value();
                                Value oddDraw = new Value();
                                Value oddAway = new Value();

                                foreach (var eachOdds in eachLstOdds.bookmakers)
                                {
                                    oddHome = eachOdds.bets[0].values.FirstOrDefault(it2 => it2.value == "Home");
                                    oddDraw = eachOdds.bets[0].values.FirstOrDefault(it2 => it2.value == "Draw");
                                    oddAway = eachOdds.bets[0].values.FirstOrDefault(it2 => it2.value == "Away");
                                    if (oddHome != null && oddDraw != null && oddAway != null)
                                    {
                                        OddsMaker = eachOdds.bookmaker_name;
                                        perHome = (Convert.ToDouble(oddAway.odd) * 100) / (Convert.ToDouble(oddHome.odd) + Convert.ToDouble(oddDraw.odd) + Convert.ToDouble(oddAway.odd));
                                        perDraw = (Convert.ToDouble(oddDraw.odd) * 100) / (Convert.ToDouble(oddHome.odd) + Convert.ToDouble(oddDraw.odd) + Convert.ToDouble(oddAway.odd));
                                        perAway = (Convert.ToDouble(oddHome.odd) * 100) / (Convert.ToDouble(oddHome.odd) + Convert.ToDouble(oddDraw.odd) + Convert.ToDouble(oddAway.odd));
                                        break;
                                    }
                                }

                                var item = new LeagueOddsFixture
                                {
                                    LeagueID = FixtureMatch.league_id.ToString(),
                                    LeagueName = FixtureMatch.league.name,
                                    LeagueCountry = FixtureMatch.league.country,
                                    LeagueLogo = FixtureMatch.league.logo,
                                    LeagueFlag = FixtureMatch.league.flag,
                                    EventDate = FixtureMatch.event_date,
                                    MatchStatus = FixtureMatch.status,
                                    HometeamName = FixtureMatch.homeTeam.team_name,
                                    HometeamLogo = FixtureMatch.homeTeam.logo,
                                    HometeamScore = FixtureMatch.goalsHomeTeam,
                                    AwayteamName = FixtureMatch.awayTeam.team_name,
                                    AwayteamLogo = FixtureMatch.awayTeam.logo,
                                    AwayteamScore = FixtureMatch.goalsAwayTeam,
                                    OddsBookmaker = OddsMaker,
                                    OddsLabal = eachLstOdds.bookmakers[0].bets[0].label_name,
                                    OddsHome = oddHome.odd,
                                    OddsDraw = oddDraw.odd,
                                    OddsAway = oddAway.odd,
                                    PerHome = string.Format("{0:00.############}", perHome),
                                    PerDraw = string.Format("{0:00.############}", perDraw),
                                    PerAway = string.Format("{0:00.############}", perAway)
                                };
                                if (FixtureMatch.status.ToLower().IndexOf("postponed") < 0)
                                {
                                    ListLeagueFixOdds.Add(item);
                                }
                            }
                        }
                        if (ListLeagueFixOdds.Count > 0)
                        {
                            //Add into Return List
                            var item2 = new List_LeagueOddsFixture
                            {
                                LeagueID = ListLeagueFixOdds[0].LeagueID,
                                LeagueName = ListLeagueFixOdds[0].LeagueName,
                                LeagueFlag = ListLeagueFixOdds[0].LeagueFlag,
                                LeagueCountry = ListLeagueFixOdds[0].LeagueCountry,
                                LeagueOddsFixture = ListLeagueFixOdds.OrderBy(c => c.EventDate).ToList()
                            };
                            Ret_LeagureOddsFixture.Add(item2);
                        }
                    }
                }
            }
            // string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Ret_LeagureOddsFixture);
            // FileStream fs_fixture = new FileStream(date + ".txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            // StreamWriter textWriter_fixture = new StreamWriter(fs_fixture);
            // textWriter_fixture.WriteLine(jsonString);
            // textWriter_fixture.Close();
            // fs_fixture.Close();

            Temp_List_LeagueOddsFixture = Ret_LeagureOddsFixture;
        }
        #endregion
    }
}
