using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace COMPARE
{
    class UtilSoccer
    {
        public static ticket convertStringToTicket(string input)
        {
            string[] t = input.Split(',');
            ticket o = new ticket();
            o.home = t[0];
            o.away = t[1];
            o.bettype = t[2];
            o.hdp = double.Parse(t[3]);
            o.odd = double.Parse(t[4]);
            o.choose = t[5];
            o.keoid = t[6];
            o.score = t[7];
            return o;
        }

        public static List<objCompare> convertCompareToList(string input, int sort = 0)
        {
            List<objCompare> lst = new List<objCompare>();
            foreach (string str in input.Split('\n'))
            {
                try
                {
                    lst.Add(convertStringToCompare(str));
                }
                catch
                { }
            }
            if (sort == 0)
                lst = lst.OrderBy(o => o.hdp).ToList();
            else
                lst = lst.OrderByDescending(o => o.hdp).ToList();
            return lst;
        }

        public static objCompare convertStringToCompare(string input)
        {
            string[] t = input.Split(',');
            objCompare o = new objCompare();
            o.home = t[0];
            o.away = t[1];
            o.bettype = t[2];
            o.hdp = double.Parse(t[3]);
            o.odd = t[4];
            o.keo = t[5];
            o.betid = t[6];
            o.profit = double.Parse(t[7]);
            o.league = double.Parse(t[8]);
            o.score = t[9];
            o.time = t[10];
            return o;
        }
        public static float totalScore(string input)
        {
            try
            {
                return float.Parse(input.Split('-')[0]) + float.Parse(input.Split('-')[1]);
            }
            catch
            {
                return 0;
            }
        }
        public static string convertSoccerTime(string matchTime)
        {
            DateTime dtMatch = DateTime.Parse(matchTime);
            string strTimeMatch = dtMatch.ToString("HH:mm");
            DateTime dtCurrent = DateTime.Now.AddHours(1);
            string strTimeCurrent = dtCurrent.ToString("HH:mm");

            int intTimeMatch = Int16.Parse(strTimeMatch.Split(':')[0]) * 60 + Int16.Parse(strTimeMatch.Split(':')[1]);
            int intTimeCurrent = Int16.Parse(strTimeCurrent.Split(':')[0]) * 60 + Int16.Parse(strTimeCurrent.Split(':')[1]);

            if (intTimeMatch < intTimeCurrent)
            {
                return dtCurrent.AddDays(1).ToShortDateString() + " " + strTimeMatch;
            }
            else
            {
                return dtCurrent.ToShortDateString() + " " + strTimeMatch;
            }
        }
        public static string formatkeo(string str)
        {
            if (str == "") return "";
            int pos = str.IndexOf("-");
            int npos = str.Split('-').Length;
            double tam;
            string result = "";
            if (npos == 1 || (npos == 2 && pos == 0))
            {
                tam = Convert.ToDouble(str);
                result = tam.ToString();
            }
            else if (npos == 3)
            {
                tam = (Convert.ToDouble(str.Split('-')[1]) + Convert.ToDouble(str.Split('-')[2])) / (-2);
                result = tam.ToString();
            }
            else
            {
                tam = (Convert.ToDouble(str.Split('-')[0]) + Convert.ToDouble(str.Split('-')[1])) / 2;
                result = tam.ToString();
            }
            return result;
        }
        public static double profit_odd(string c, string d)
        {
            try
            {
                double a = double.Parse(c);
                double b = double.Parse(d);
                if (a > 0)
                {
                    a = a - 1;
                }
                else
                {
                    a = a + 1;
                }
                if (b > 0)
                {
                    b = b - 1;
                }
                else
                {
                    b = b + 1;
                }
                double result = System.Math.Round(a + b, 2);
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public static double profit_odd_new(string c, string d)
        {
            try
            {
                double a = double.Parse(c);
                double b = double.Parse(d);
                if (a > 0)
                {
                    a = a - 1;
                }
                else
                {
                    a = a + 1;
                }
                if (b > 0)
                {
                    b = b - 1;
                }
                else
                {
                    b = b + 1;
                }
                double result = System.Math.Round(a + b, 2);
                if (result > 0)
                    result = 0;
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public static string ChuanTenLeauge_Sbo(string name_leauge)
        {
            string result = "";
            string sbot = "COPA BRIDGESTONE LIBERTADORES,GERMANY 3RD LEAGUE,KOREA K-LEAGUE CLASSIC,CHILE SCOTIABANK PRIMERA DIVISION,";
            sbot += "CZECH REPUBLIC FIRST LEAGUE,NORWAY OBOS LIGAEN,PORTUGAL LIGA NOS,IRELAND SSE AIRTRICITY PREMIER DIVISION,";
            sbot += "IRELAND SSE AIRTRICITY 1ST DIV,SWISS RAIFFEISEN SUPER LEAGUE,FRANCE DOMINO\\S LIGUE 2,AUSTRALIA HYUNDAI A LEAGUE PLAYOFF,";
            sbot += "COLOMBIA LIGA AGUILA";
            sbot += "";


            string ibett = "COPA LIBERTADORES,GERMANY 3RD LIGA,KOREA K LEAGUE CLASSIC,CHILE PRIMERA DIVISION,";
            ibett += "CZECH REPUBLIC 1ST DIVISION,NORWAY 1ST DIVISION,PORTUGAL SUPER LIGA,IRELAND PREMIER DIVISION,";
            ibett += "IRELAND 1ST DIVISION,SWISS SUPER LEAGUE,FRANCE LIGUE 2,AUSTRALIA A-LEAGUE (PLAY OFF),";
            ibett += "COLOMBIA PRIMERA A";
            ibett += "";



            name_leauge = name_leauge.ToUpper();
            for (int i = 0; i < sbot.Split(',').Length; i++)
            {
                name_leauge = name_leauge.Replace(sbot.Split(',')[i], ibett.Split(',')[i]);
            }

            try
            {
                result = name_leauge.Trim();
            }
            catch
            { }
            return result;
        }
        public static string ChuanTenLeauge_Ibet(string name_leauge)
        {
            string result = "";
            string ibett = "SPAIN PRIMERA LALIGA,BRAZIL CAMPEONATO PAULISTA,HOLLAND EERSTE DIVISIE,JAPAN J-LEAGUE DIVISION 1,";
            ibett += "AUSTRALIA VICTORIA PREMIER LEAGUE,DENMARK SUPER LEAGUE (PLAY OFF),ENGLISH LEAGUE CHAMPIONSHIP,";
            ibett += "BRAZIL CAMPEONATO CARIOCA,SWEDEN 1ST DIVISION NORTH,SWEDEN 1ST DIVISION SOUTH,POLAND 1ST DIVISION,";
            ibett += "JAPAN J-LEAGUE DIVISION 2,SPAIN SEGUNDA DIVISION,GERMANY-BUNDESLIGA I,SWITZERLAND SUPER LEAGUE,";
            ibett += "SWITZERLAND CHALLENGE LEAGUE,(PLAY OFF),";
            ibett += "";
            ibett += "";
            ibett += "";
            ibett += "*";


            string sbot = "SPAIN LA LIGA,BRAZIL PAULISTA,HOLLAND JUPILER LEAGUE,JAPAN J1 LEAGUE,";
            sbot += "AUSTRALIA VICTORIAN PREMIER LEAGUE,DENMARK SUPER LEAGUE PLAYOFF,ENGLISH CHAMPIONSHIP,";
            sbot += "BRAZIL CARIOCA,SWEDEN 1ST DIV NORTH,SWEDEN 1ST DIV SOUTH,POLAND 1ST LIGA,";
            sbot += "JAPAN J2 LEAGUE,SPAIN LA LIGA 2, GERMANY BUNDESLIGA,SWISS SUPER LEAGUE,";
            sbot += "SWISS CHALLENGE LEAGUE,PLAYOFF,";
            sbot += "";
            sbot += "";
            sbot += "";
            sbot += "";
            for (int i = 0; i < ibett.Split(',').Length; i++)
            {
                name_leauge = name_leauge.Replace(ibett.Split(',')[i], sbot.Split(',')[i]);
            }

            try
            {
                result = name_leauge.Trim();
            }
            catch
            { }
            return result;
        }
        public static string ChuanTenTeam_Sbo(string name_team)
        {
            string result = "";
            string sbot = "FC Twente Enschede,Rennes,Vitesse Arnhem,LA Galaxy,Jaguares de Chiapas,Emelec Guayaquil,";
            sbot += "Sporting Charleroi,Sint-Truidense,Reus Deportiu,Tiburones Rojos Veracruz,Rayados Monterrey,Necaxa Aguascalientes,";
            sbot += "Racing Genk,KAS Eupen,Sporting Lokeren,Campos Athletic Association (n),Linense Lins,";
            sbot += "Penarol Montevideo,Atletico Paranaense,Patronato Parana,Cambuur Leeuwarden,Jong FC Utrecht (n),";
            sbot += "Preussen Munster,Yokohama F.Marinos,AD Alcorcon,Gazelec Ajaccio,Clermont Foot,";
            sbot += "VVV-Venlo,W.B.A,S.P.A.L. 2013,";//Patronato Parana
            sbot += "Club Tijuana,Pumas U.N.A.M.,Campos Athletic Association,Botafogo Ribeirao Preto,";
            sbot += "Belgrano de Cordoba,Atletico Huracan,Kardemir Karabukspor,Suwon Samsung Bluewings,";
            sbot += "Rheindorf Altach,Sanfrecce Hiroshima,Rot-Weiss Erfurt,Union de Santa Fe,";
            sbot += "Sarmiento Junin,Herfolge Koge,Odense BK,Glasgow Rangers,O.Higgins,";//Estoril Praia
            sbot += "Estoril Praia,Hellas Verona,Ascoli Picchio,Aldosivi Mar del Plata,";
            sbot += "Olimpo Bahia Blanca,Jong FC Utrecht,FBC Melgar Arequipa,Atletico Nacional Medellin,";
            sbot += "Zulia Maracaibo,GD Chaves,Cote d Ivoire,Republic of Ireland,Ullensaker Kisa,";
            sbot += "Sedan Ardennes,Lierse SK,Tromsdalen UIL,Elverum Fotball,";
            sbot += "Arameiska,Skeid Fotball,Finnsnes IL,";
            sbot += "";
            sbot += "";



            sbot += "NAC,PSV,SSV,OSC,SCR,SKN,PAS,FSV,MSV,SBV,AFC,GFC,WSG,RCD,(n),(N),KRC,RSC,OGC,SP,SD,SC,SV,CS,CF,CA,CD,AE,AC,AS,AJ,FK,FB,FF,FC,RJ,RC,NK, BK, IK";



            string ibett = "Twente,Rennais,Vitesse,Galaxy,Chiapas,Emelec,";
            ibett += "Charleroi,Truidense,Reus,Tiburones Veracruz,Monterrey,Necaxa,";
            ibett += "Genk,Eupen,Lokeren,Campos,Linense,";
            ibett += "Penarol,Paranaense,Patronato,Cambuur,Jong Utrecht,";
            ibett += "Preusen Munster,Yokohama Marinos,Alcorcon,Ajaccio,Clermont,";
            ibett += "VVV Venlo,West Bromwich,Spal 2013,";
            ibett += "Tijuana,Pumas,Campos AA,Botafogo,";
            ibett += "Belgrano,Huracan,Kardemir Karabuk,Suwon Bluewings,";
            ibett += "Altach,Hiroshima Sanfrecce,Rot Weiss Erfurt,Union Santa,";
            ibett += "Sarmiento,HB Koge,Odense,Rangers,O Higgins,";
            ibett += "Estoril,Verona,Ascoli,Aldosivi,";
            ibett += "Olimpo,Jong Utrecht,Melgar,Atletico Nacional,";
            ibett += "Zulia,Chaves,Ivory Coast,Ireland,Ull Kisa,";
            ibett += "Sedan,Lierse,Tromsdalen,Elverum,";
            ibett += "Arameisk,Skeid,Finnsnes,";
            ibett += "";
            ibett += "";



            ibett += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            //MessageBox.Show(ibett.Split(',').Length.ToString() + " / " + sbot.Split(',').Length.ToString());
            for (int i = 0; i < sbot.Split(',').Length; i++)
            {
                name_team = name_team.Replace(sbot.Split(',')[i], ibett.Split(',')[i]);
            }

            try
            {
                result = name_team.Trim();
            }
            catch
            { }

            return result;
        }
        public static string ChuanTenTeam_Ibet(string name_team)
        {
            string result = "";
            string ibett = "L.A Galaxy,Royal Charleroi SC,Sint Truidense,Washington D.C. United,Deportivo Toluca,";
            ibett += "Stade Rennais,SM Caen,Campos AA (N),Deportivo Alaves,Atl Paranaense,Bayer 04 Leverkusen,";
            ibett += "FAC Team Fur Wien,San Luis de Quillota,Stade Lavallois,Wexford Youths,Olympique de Marseille,";
            ibett += "Limerick 37,Red Star Saint-Ouen,TSG 1899 Hoffenheim,Atletico de Madrid,";
            ibett += "West Bromwich Albion,Lyngby BK,Inverness CT,Girondins de Bordeaux,";
            ibett += "Vitoria de Setubal,Levante UD,Carpi FC 1909,AS Nancy Lorraine,";
            ibett += "Baumit Jablonec,Stade Brestois,Union De Santa Fe,OB Odense,";
            ibett += "UD Almeria,S.D. Huesca,Heidenheim 1846,Olympique Lyonnais,Macae Esporte,";
            ibett += "Atletico de Rafaela,Club Olimpo,AaB Aalborg,Sochaux Montbeliard,FBC Melgar,Barcelona SC,";
            ibett += "Pumas UNAM,Orebro SK,Gremio Porto Alegrense,Gefle IF,Osters IF,St. Mirren,Bahia EC BA,";
            ibett += "PAOK Thessaloniki,Dinamo Moscow,";
            ibett += "";
            ibett += "";



            ibett += "NAC,PSV,SSV,OSC,SCR,SKN,PAS,FSV,MSV,SBV,AFC,GFC,WSG,RCD,(n),(N),KRC,RSC,OGC,SP,SD,SC,SV,CS,CF,CA,CD,AE,AC,AS,AJ,FK,FB,FF,FC,RJ,RC,NK, BK, IK";



            string sbot = "Galaxy,Charleroi,Truidense,D.C. United,Toluca,";
            sbot += "Rennais,Caen,Campos,Alaves,Paranaense,Bayer Leverkusen,";
            sbot += "Floridsdorfer,San Luis Quillota,Laval,Wexford,Marseille,";
            sbot += "Limerick,Red Star Saint Ouen,TSG Hoffenheim,Atletico Madrid,";
            sbot += "West Bromwich,Lyngby,Inverness,Bordeaux,";
            sbot += "Vitoria Setubal,Levante,Carpi,Nancy,";
            sbot += "Jablonec,Brest,Union Santa,Odense,";
            sbot += "Almeria,SD Huesca,Heidenheim,Lyon,Macae,";
            sbot += "Atletico Rafaela,Olimpo,Aalborg,Sochaux,Melgar,Barcelona Guayaquil,";
            sbot += "Pumas,Orebro,Gremio Porto Alegre,Gefle,Osters,Saint Mirren,Bahia,";
            sbot += "PAOK,Dynamo Moscow,";
            sbot += "";
            sbot += "";


            sbot += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            //MessageBox.Show("SSV,OSC,SCR,SKN,PAS,FSV,MSV,SBV,AFC,GFC,WSG,RCD,(n),(N),KRC,RSC,OGC,SP,SD,SC,SV,CS,CF,CA,CD,AE,AC,AS,AJ,FK,FB,FF,FC,RJ,RC,NK, BK, IK".Split(',').Length.ToString());
            //MessageBox.Show(",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,".Split(',').Length.ToString());
            //MessageBox.Show(ibett.Split(',').Length.ToString() + " / " + sbot.Split(',').Length.ToString());
            for (int i = 0; i < ibett.Split(',').Length; i++)
            {
                name_team = name_team.Replace(ibett.Split(',')[i], sbot.Split(',')[i]);
            }

            try
            {
                result = name_team.Trim();
            }
            catch
            { }

            return result;
        }
        private static Hashtable HashIbet = new Hashtable();
        private static Hashtable HashSbo = new Hashtable();
        public static string CompareByHash(List<objMatch> Ibet, List<objMatch> Sbo, double profitmin)
        {
            string result = "";
            HashIbet.Clear();
            HashSbo.Clear();
            foreach (objMatch ObjIbet in Ibet)
            {
                string key = string.Format("{0},{1},{2},{3}", ObjIbet.HomeName, ObjIbet.AwayName, ObjIbet.hdp, ObjIbet.BetType);
                if (!HashIbet.Contains(key))
                    HashIbet.Add(key, ObjIbet);
            }
            foreach (objMatch ObjSbo in Sbo)
            {
                string key = string.Format("{0},{1},{2},{3}", ObjSbo.HomeName, ObjSbo.AwayName, ObjSbo.hdp, ObjSbo.BetType);
                if (!HashSbo.Contains(key))
                    HashSbo.Add(key, ObjSbo);
            }

            ICollection keySbos = HashSbo.Keys;
            foreach (string keySbo in keySbos)
            {
                if (HashIbet.ContainsKey(keySbo))
                {
                    string OddSbo1 = ((objMatch)HashSbo[keySbo]).Odd1;
                    string OddSbo2 = ((objMatch)HashSbo[keySbo]).Odd2;
                    string OddIbet1 = ((objMatch)HashIbet[keySbo]).Odd1;
                    string OddIbet2 = ((objMatch)HashIbet[keySbo]).Odd2;
                    double LeagueOdd = UtilSoccer.profit_odd(OddSbo1, OddSbo2);
                    string IdKeoSbo = ((objMatch)HashSbo[keySbo]).IdKeo;
                    string IdKeoIbet = ((objMatch)HashIbet[keySbo]).IdKeo;
                    string HomeName = ((objMatch)HashIbet[keySbo]).HomeName;
                    string AwayName = ((objMatch)HashIbet[keySbo]).AwayName;
                    string Keo = ((objMatch)HashIbet[keySbo]).hdp;
                    string BetType = ((objMatch)HashSbo[keySbo]).BetType;
                    string Score = ((objMatch)HashIbet[keySbo]).Score;
                    string Time = ((objMatch)HashIbet[keySbo]).TimeNonLive;
                    //Time = Time.Split(' ')[Time.Split(' ').Length - 1];
                    if (profit_odd(OddSbo1, OddIbet2) >= profitmin)
                    {
                        result += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                    }
                    else if (profit_odd(OddSbo2, OddIbet1) >= profitmin)
                    {
                        result += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                    }
                }
            }
            return result;
        }
        private static Hashtable HashIbetLive = new Hashtable();
        private static Hashtable HashSboLive = new Hashtable();
        //private static Hashtable HashKeoDuoi = new Hashtable();
        public static string under_compare = "";
        public static string CompareByHashLive(List<objMatch> Ibet, List<objMatch> Sbo, double profitmin)
        {
            string result = "";
            under_compare = "";
            HashIbetLive.Clear();
            HashSboLive.Clear();
            //HashKeoDuoi.Clear();
            foreach (objMatch ObjIbet in Ibet)
            {
                string key = string.Format("{0},{1},{2},{3}", ObjIbet.HomeName, ObjIbet.AwayName, ObjIbet.hdp, ObjIbet.BetType);
                if (!HashIbetLive.Contains(key))
                    HashIbetLive.Add(key, ObjIbet);

                //if (ObjIbet.hdp.Trim() != "")
                //{
                //    try
                //    {
                //        if (float.Parse(ObjIbet.hdp) < 0 && !HashKeoDuoi.Contains(ObjIbet.HomeName))
                //            HashKeoDuoi.Add(ObjIbet.HomeName, ObjIbet.HomeName);
                //        else if (float.Parse(ObjIbet.hdp) >= 0 && !HashKeoDuoi.Contains(ObjIbet.AwayName))
                //            HashKeoDuoi.Add(ObjIbet.AwayName, ObjIbet.AwayName);
                //    }
                //    catch (Exception)
                //    {
                                                
                //    }
                //}
            }
            foreach (objMatch ObjSbo in Sbo)
            {
                string key = string.Format("{0},{1},{2},{3}", ObjSbo.HomeName, ObjSbo.AwayName, ObjSbo.hdp, ObjSbo.BetType);
                if (!HashSboLive.Contains(key))
                    HashSboLive.Add(key, ObjSbo);
            }

            ICollection keySbos = HashSboLive.Keys;
            foreach (string keySbo in keySbos)
            {
                try
                {
                    if (HashIbetLive.ContainsKey(keySbo))
                    {
                        string OddSbo1 = ((objMatch)HashSboLive[keySbo]).Odd1;
                        string OddSbo2 = ((objMatch)HashSboLive[keySbo]).Odd2;
                        string OddIbet1 = ((objMatch)HashIbetLive[keySbo]).Odd1;
                        string OddIbet2 = ((objMatch)HashIbetLive[keySbo]).Odd2;
                        double LeagueOdd = UtilSoccer.profit_odd(OddSbo1, OddSbo2);
                        string IdKeoSbo = ((objMatch)HashSboLive[keySbo]).IdKeo;
                        string IdKeoIbet = ((objMatch)HashIbetLive[keySbo]).IdKeo;
                        string HomeName = ((objMatch)HashIbetLive[keySbo]).HomeName;
                        string AwayName = ((objMatch)HashIbetLive[keySbo]).AwayName;
                        string Keo = ((objMatch)HashIbetLive[keySbo]).hdp;
                        string BetType = ((objMatch)HashSboLive[keySbo]).BetType;
                        string Score = ((objMatch)HashIbetLive[keySbo]).Score;
                        string Time = ((objMatch)HashIbetLive[keySbo]).TimeLive;
                        //Time = Time.Split(' ')[Time.Split(' ').Length - 1];
                        if (profit_odd_new(OddSbo1, OddIbet2) >= profitmin)
                        {
                            result += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                            try
                            {
                                if (int.Parse(Time.Split(' ')[1]) >= 25 && int.Parse(Time.Split(' ')[1]) <= 43)
                                {
                                    if (Time.Split(' ')[0].ToUpper().Contains("1H"))
                                    {
                                        if (BetType == "7" && float.Parse(Keo) >= 0)
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                        else if (BetType == "9")
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                    }
                                    else if (Time.Split(' ')[0].ToUpper().Contains("2H"))
                                    {
                                        if (BetType == "1" && float.Parse(Keo) >= 0)
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                        else if (BetType == "3")
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo1 + "/" + OddIbet2 + ",h/a," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo1, OddIbet2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                    }
                                }
                            }
                            catch (Exception)
                            {
;
                            }
                        }
                        else if (profit_odd_new(OddSbo2, OddIbet1) >= profitmin)
                        {
                            result += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                            try
                            {
                                if (int.Parse(Time.Split(' ')[1]) >= 25 && int.Parse(Time.Split(' ')[1]) <= 43)
                                {
                                    if (Time.Split(' ')[0].ToUpper().Contains("1H"))
                                    {
                                        if (BetType == "7" && float.Parse(Keo) >= 0)
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                        else if (BetType == "9")
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                    }
                                    else if (Time.Split(' ')[0].ToUpper().Contains("2H"))
                                    {
                                        if (BetType == "1" && float.Parse(Keo) >= 0)
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                        else if (BetType == "3")
                                            under_compare += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + OddSbo2 + "/" + OddIbet1 + ",a/h," + IdKeoSbo + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd_new(OddSbo2, OddIbet1) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                
                            }
                        }
                    }
                }
                catch (Exception)
                {
                                        
                }
            }
            return result;
        }
        private static Hashtable hsIbetUnder = new Hashtable();
        public static string CompareUnder(List<objMatch> lstIbet)
        {
            string result = "";
            foreach (objMatch match in lstIbet)
            {
                try
                {
                    string key = string.Format("{0},{1},{2},{3}", match.HomeName, match.AwayName, match.hdp, match.BetType);
                    if ((match.BetType == "3" || match.BetType == "9") && int.Parse(match.TimeLive.Split(' ')[1]) <= 5)
                    {                        
                        if (!hsIbetUnder.Contains(key))
                        {
                            hsIbetUnder.Add(key, match);
                        }
                    }
                    else if ((match.BetType == "3" || match.BetType == "9") && int.Parse(match.TimeLive.Split(' ')[1]) >= 30)
                    {
                        if (hsIbetUnder.Contains(key))
                        {
                            if (((objMatch)hsIbetUnder[key]).Score == match.Score)
                            {
                                string Odd1 = match.Odd1;
                                string Odd2 = match.Odd2;
                                double LeagueOdd = UtilSoccer.profit_odd(Odd1, Odd2);
                                string IdKeoIbet = match.IdKeo;
                                string HomeName = match.HomeName;
                                string AwayName = match.AwayName;
                                string Keo = match.hdp;
                                string BetType = match.BetType;
                                string Score = match.Score;
                                string Time = match.TimeLive;
                                result += HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + Odd1 + "/" + Odd2 + ",h/a," + IdKeoIbet + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd(Odd1, Odd2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";
                            }
                            hsIbetUnder.Remove(key);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            return result;
        }
        private static Hashtable hsIbetMinus = new Hashtable();
        public static void CompareMinus(List<objMatch> lstIbet)
        {
            string result = "";
            foreach (objMatch match in lstIbet)
            {
                try
                {
                    string key = string.Format("{0},{1},{2},{3}", match.HomeName, match.AwayName, match.hdp, match.BetType);
                    if ((float.Parse(match.Odd1) < 0 || float.Parse(match.Odd2) < 0) && match.hdp != "")
                    {
                        if (!hsIbetMinus.Contains(key))
                        {
                            hsIbetMinus.Add(key, match);
                            string Odd1 = match.Odd1;
                            string Odd2 = match.Odd2;
                            double LeagueOdd = UtilSoccer.profit_odd(Odd1, Odd2);
                            string IdKeoIbet = match.IdKeo;
                            string HomeName = match.HomeName;
                            string AwayName = match.AwayName;
                            string Keo = match.hdp;
                            string BetType = match.BetType;
                            string Score = match.Score;
                            string Time = match.TimeLive;
                            string home_away = "";
                            if (float.Parse(match.Odd1) < 0)
                                home_away = "a/h";
                            else
                                home_away = "h/a";
                            result = HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + Odd1 + "/" + Odd2 + "," + home_away + "," + IdKeoIbet + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd(Odd1, Odd2) + "," + LeagueOdd + "," + Score + "," + Time + "\n";                         
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
