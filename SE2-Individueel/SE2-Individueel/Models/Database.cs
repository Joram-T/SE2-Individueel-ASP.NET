using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;
using System.Data;
using System.Globalization;

namespace SE2_Individueel.Models
    {
        public class Database
        {
            private OracleConnection con;
            private OracleCommand command;
            private OracleDataReader reader;

            public Database()
            {

                string constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fhictora01.fhict.local)(PORT=1521)))"
                              + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=fhictora)));"
                              + "User ID=DBI347664; PASSWORD=individueel;";

                con = new OracleConnection(constr);
            }

        #region Id's opvragen
        public int maxAfspeellijstID()
        {
            int maxid = 0;

            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT MAX(ID) FROM Afspeellijst", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                maxid = Convert.ToInt32(reader["MAX(ID)"]);
            }
            con.Close();
            return maxid;
        }
        public int maxLiedID()
        {
            int maxid = 0;

            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT MAX(ID) FROM Lied", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                maxid = Convert.ToInt32(reader["MAX(ID)"]);
            }
            con.Close();
            return maxid;
        }
        #endregion

        #region Zoeken
        public List<Lied> Liedzoeken(string gezocht)
        {
            List<Lied> liedjes = new List<Lied>();
            List<int> idlist = new List<int>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID FROM LIED WHERE UPPER(NAAM) LIKE UPPER(:gezocht)", con);
            command.Parameters.Add(new OracleParameter("gezocht", OracleDbType.Varchar2)).Value = gezocht + "%";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idlist.Add(Convert.ToInt32(reader["ID"]));
            }

            foreach (int i in idlist)
            {
                command = new OracleCommand("SELECT ID, SPEELDUUR_SECONDEN, NAAM FROM LIED WHERE ID =:lied", con);
                command.Parameters.Add(new OracleParameter("lied", OracleDbType.Int32)).Value = i;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    TimeSpan time = new TimeSpan(0, 0, Convert.ToInt32(reader["SPEELDUUR_SECONDEN"]));
                    liedjes.Add(new Lied(Convert.ToInt32(id), time, reader["NAAM"].ToString(), Albuminfo(Albumidbijlied(Convert.ToInt32(id)))));
                }
            }

            con.Close();
            return liedjes;
        }
        public List<Album> Albumzoeken(string gezocht)
        {
            List<Album> albums = new List<Album>();
            List<int> idlist = new List<int>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM WHERE UPPER(NAAM) LIKE UPPER(:gezocht)", con);
            command.Parameters.Add(new OracleParameter("gezocht", OracleDbType.Varchar2)).Value = gezocht + "%";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idlist.Add(Convert.ToInt32(reader["ID"]));
            }
            foreach(int i in idlist)
            {
                command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM WHERE ID =:albumid", con);
                command.Parameters.Add(new OracleParameter("albumtid", OracleDbType.Int32)).Value = i;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    DateTime time = Convert.ToDateTime(reader["DATUM"]);
                    albums.Add(new Album(Convert.ToInt32(id), reader["NAAM"].ToString(), time, Artiestinfo(Artiestidbijalbum(id))));
                }
            }
            con.Close();
            return albums;
        }
        public List<Artiest> Artiestzoeken(string gezocht)
        {
            List<Artiest> artiesten = new List<Artiest>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, BIOGRAFIE FROM ARTIEST WHERE UPPER(NAAM) LIKE UPPER(:gezocht)", con);
            command.Parameters.Add(new OracleParameter("gezocht", OracleDbType.Varchar2)).Value = gezocht + "%";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                artiesten.Add(new Artiest(Convert.ToInt32(reader["ID"]), reader["NAAM"].ToString(), reader["BIOGRAFIE"].ToString()));
            }
            con.Close();
            return artiesten;
        }
        #endregion

        #region Lijsten Opvragen
        public List<Lied> Liedjes()
        {
            List<Lied> liedjes = new List<Lied>();
            List<int> idlist = new List<int>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID FROM LIED", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idlist.Add(Convert.ToInt32(reader["ID"]));
            }

            foreach (int i in idlist)
            {
                command = new OracleCommand("SELECT ID, SPEELDUUR_SECONDEN, NAAM FROM LIED WHERE ID =:lied", con);
                command.Parameters.Add(new OracleParameter("lied", OracleDbType.Int32)).Value = i;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    TimeSpan time = new TimeSpan(0, 0, Convert.ToInt32(reader["SPEELDUUR_SECONDEN"]));
                    liedjes.Add(new Lied(Convert.ToInt32(id), time, reader["NAAM"].ToString(), Albuminfo(Albumidbijlied(Convert.ToInt32(id)))));
                }
            }

            con.Close();
            return liedjes;
        }
        public List<Album> Albums()
        {
            List<Album> albums = new List<Album>();
            List<int> idlist = new List<int>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idlist.Add(Convert.ToInt32(reader["ID"]));
            }
            foreach (int i in idlist)
            {
                command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM WHERE ID =:albumid", con);
                command.Parameters.Add(new OracleParameter("albumtid", OracleDbType.Int32)).Value = i;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    DateTime time = Convert.ToDateTime(reader["SPEELDUUR_SECONDEN"]);
                    albums.Add(new Album(Convert.ToInt32(id), reader["NAAM"].ToString(), time, Artiestinfo(Artiestidbijalbum(id))));
                }
            }
            con.Close();
            return albums;
        }
        public List<Artiest> Artiesten()
        {
            List<Artiest> artiesten = new List<Artiest>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, BIOGRAFIE FROM ARTIEST", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                artiesten.Add(new Artiest(Convert.ToInt32(reader["ID"]), reader["NAAM"].ToString(), reader["BIOGRAFIE"].ToString()));
            }
            con.Close();
            return artiesten;
        }
        public List<Afspeellijst> Afspeellijsten(string maker)
        {
            List<Afspeellijst> afspeellijsten;
            afspeellijsten = new List<Afspeellijst>();

            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT Naam, ID FROM Afspeellijst WHERE MAKER =:maker", con);
            command.Parameters.Add(new OracleParameter("maker", OracleDbType.Varchar2)).Value = maker;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                afspeellijsten.Add(new Afspeellijst(reader["NAAM"].ToString(), Convert.ToInt32(reader["ID"])));
            }

            foreach(Afspeellijst afspeellijst in afspeellijsten)
            {
                List<Lied> liedjes = new List<Lied>();
                List<int> idlist = new List<int>();
                try { con.Open(); } catch { };
                command = new OracleCommand("SELECT L.ID FROM LIED L, AFSPEELLIJST_LIED A WHERE L.ID = A.LIED_ID AND A.AFSPEELLIJST_ID =:afpspeellijstID", con);
                command.Parameters.Add(new OracleParameter("afpspeellijstID", OracleDbType.Int32)).Value = afspeellijst.ID;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idlist.Add(Convert.ToInt32(reader["ID"]));
                }

                foreach (int i in idlist)
                {
                    command = new OracleCommand("SELECT ID, SPEELDUUR_SECONDEN, NAAM FROM LIED WHERE ID =:lied", con);
                    command.Parameters.Add(new OracleParameter("lied", OracleDbType.Int32)).Value = i;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        TimeSpan time = new TimeSpan(0, 0, Convert.ToInt32(reader["SPEELDUUR_SECONDEN"]));
                        afspeellijst.Liedjes.Add(new Lied(Convert.ToInt32(id), time, reader["NAAM"].ToString(), Albuminfo(Albumidbijlied(Convert.ToInt32(id)))));
                    }
                }
            }
            
            con.Close();

            return afspeellijsten;
        }
        #endregion

        public Gebruiker Inloggen(string gebruikersnaam, string wachtwoord)
        {
            Gebruiker gebruiker = new Gebruiker("", "", "", "", "", DateTime.Now, "", "", "", "","");

            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT * FROM GEBRUIKER WHERE UPPER(GEBRUIKERSNAAM) =:gebruikersnaam AND WACHTWOORD =: wachtwoord", con);
            command.Parameters.Add(new OracleParameter("gebruikersnaam", OracleDbType.Varchar2)).Value = gebruikersnaam.ToUpper();
            command.Parameters.Add(new OracleParameter("wachtwoord", OracleDbType.Varchar2)).Value = wachtwoord;
            reader = command.ExecuteReader();
            while (reader.Read())

            {
                gebruiker = new Gebruiker(reader["GEBRUIKERSNAAM"].ToString(), reader["VOORNAAM"].ToString(), reader["TUSSENVOEGSEL"].ToString(),
                    reader["ACHTERNAAM"].ToString(), reader["EMAIL"].ToString(), Convert.ToDateTime(reader["GEBOORTEDATUM"]), reader["LAND"].ToString(),
                    reader["POSTCODE"].ToString(), reader["GESLACHT"].ToString(), reader["TELEFOONNUMMER"].ToString(), reader["SOORT"].ToString()); 
            }
            con.Close();
            return gebruiker;
        }

        public Artiest Artiestinfo(int artiestid)
        {
            Artiest artiest = new Artiest(0,"","");
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, BIOGRAFIE FROM ARTIEST WHERE ID =:artiestid", con);
            command.Parameters.Add(new OracleParameter("artiestid", OracleDbType.Int32)).Value = artiestid;
            reader = command.ExecuteReader();
            while (reader.Read())

            {
                artiest = new Artiest(Convert.ToInt32(reader["ID"]), reader["NAAM"].ToString(), reader["BIOGRAFIE"].ToString());
            }
            return artiest;
        }

        public int Artiestidbijlied(int liedid)
        {
            int artiestid = 0;
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ARTIEST_ID FROM LIED_ARTIEST WHERE LIED_ID =:liedid", con);
            command.Parameters.Add(new OracleParameter("liedid", OracleDbType.Int32)).Value = liedid;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                artiestid = Convert.ToInt32(reader["ARTIEST_ID"]);
            }
            return artiestid;
        }

        public Album Albuminfo(int albumid)
        {
            Album album = new Album(0, "", DateTime.Now, new Artiest(0, "", ""));
            List<int> idlist = new List<int>();
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM WHERE ID =:albumid", con);
            command.Parameters.Add(new OracleParameter("albumtid", OracleDbType.Int32)).Value = albumid;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idlist.Add(Convert.ToInt32(reader["ID"]));
            }
            foreach (int i in idlist)
            {
                command = new OracleCommand("SELECT ID, NAAM, DATUM FROM ALBUM WHERE ID =:albumid", con);
                command.Parameters.Add(new OracleParameter("albumtid", OracleDbType.Int32)).Value = i;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    DateTime time = Convert.ToDateTime(reader["DATUM"]);
                    album = new Album(Convert.ToInt32(id), reader["NAAM"].ToString(), time, Artiestinfo(Artiestidbijalbum(id)));
                }
            }
                return album;
        }

        public int Albumidbijlied(int liedid)
        {
            int albumid = 0;
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ALBUM_ID FROM ALBUM_LIED WHERE LIED_ID =:liedid", con);
            command.Parameters.Add(new OracleParameter("liedid", OracleDbType.Int32)).Value = liedid;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                albumid = Convert.ToInt32(reader["ALBUM_ID"]);
            }
            return albumid;
        }

        public int Artiestidbijalbum(int albumid)
        {
            int artiestid = 0;
            try { con.Open(); } catch { };
            command = new OracleCommand("SELECT ARTIEST_ID FROM ARTIEST_ALBUM WHERE ALBUM_ID =:albumid", con);
            command.Parameters.Add(new OracleParameter("albumid", OracleDbType.Int32)).Value = albumid;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                artiestid = Convert.ToInt32(reader["ARTIEST_ID"]);
            }
            return artiestid;
        }

        public void AfspeellijstMaken(string naam, string gebruiker)
        {
            int id = maxAfspeellijstID() + 1;

            try { con.Open(); } catch { };
            command = new OracleCommand("INSERT INTO Afspeellijst (ID, Maker, Naam) VALUES(:ID, :gebruiker, :naam)", con);
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Int32)).Value = id;
            command.Parameters.Add(new OracleParameter("gebruiker", OracleDbType.Varchar2)).Value = gebruiker;
            command.Parameters.Add(new OracleParameter("naam", OracleDbType.Varchar2)).Value = naam;
            command.ExecuteNonQuery();
            con.Close();
        }

    }
}