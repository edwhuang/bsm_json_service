using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;
using Jayrock.Json.Conversion;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using BsmDatabaseObjects;



namespace CMS
{
    public class kod_song
    {

        public string no;
        public string songname;
        public string singer;
        public string songclass;
        public string songnamelength;
        public string songtype;
        public string lovesong;
        public string newsong;
        public string guidesing;
        public string singertype;
        public string songnamesymbol;
        public string singernamesymbol;
     //   public string filenameold;
     //   public string clubberstatus;
        public decimal count;
      //  public string filenamefree;
       // public string filename;
     //   public string paymenttype;
        public string asset_id;
      //  public string asset_id_free;
      //  public string status_flg;
        public string release_date;
        public decimal songname_order;
        public decimal singer_order;
     //   public decimal src_order;
     //   public string sony_asset_id;
      //  public string sony_image;
      //  public string sony_flg;
     //   public string lyricscopyright;
      //  public string musiccopyright;
      //  public string videocopyright;
     //   public decimal base_count;
    //    public decimal real_count;
        public string specialcla;
    }


    public class KOD_Service : JsonRpcHandler
    {
        OracleConnection conn;


        public KOD_Service()
        {
            conn = new OracleConnection();

            System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                conn.ConnectionString = connString.ConnectionString;
            }
        }

        [JsonRpcMethod("get_kod_info")]
        public List<kod_song> get_kod_info(int? limit)
        {
            List<kod_song> _result = new List<kod_song>();
            kod_song _kod;
            conn.Open();
            try
            {
                string _sql = "select  no,songname, singer, songclass, songnamelength, songtype, lovesong, newsong, guidesing, singertype, songnamesymbol, singernamesymbol, filenameold, " +
"clubberstatus, count, filenamefree, filename, paymenttype, asset_id, asset_id_free, status_flg, release_date, songname_order, singer_order, src_order, sony_asset_id, sony_image, sony_flg, " +
"lyricscopyright, musiccopyright, videocopyright, base_count, real_count, specialclass, songname_order, singer_order from mid_kod_songdata ";

                if (!(limit == null))
                {
                    _sql = _sql + "where rownum <= " + limit.ToString();
                }

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                OracleDataReader _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    _kod = new kod_song();
                    _kod.no = this.get_string(_reader, 0);
                    _kod.songname = this.get_string(_reader, 1);
                    _kod.singer = this.get_string(_reader, 2);
                    _kod.songclass = this.get_string(_reader, 3);
                    _kod.songnamelength = this.get_string(_reader, 4);
                    _kod.songtype = this.get_string(_reader, 5);
                    _kod.lovesong = this.get_string(_reader, 6);
                    _kod.newsong = this.get_string(_reader, 7);
                    _kod.guidesing = this.get_string(_reader, 8);
                    _kod.singertype = this.get_string(_reader, 9);
                    _kod.songnamesymbol = this.get_string(_reader, 10);
                    _kod.singernamesymbol = this.get_string(_reader, 11);
                  //  _kod.filenameold = this.get_string(_reader, 12);
                  //  _kod.clubberstatus = this.get_string(_reader, 13);
                    if (!_reader.IsDBNull(14))
                    {
                        _kod.count = _reader.GetDecimal(14);
                    }
                    else
                    {
                        _kod.count = 0;
                    }
                  //  _kod.filenamefree = this.get_string(_reader, 15);
                  //  _kod.filename = this.get_string(_reader, 16);
                 //   _kod.paymenttype = this.get_string(_reader, 17);
                    _kod.asset_id = this.get_string(_reader, 18);
                //    _kod.asset_id_free = this.get_string(_reader, 19);
                 //   _kod.status_flg = this.get_string(_reader, 20);
                    //if (!_reader.IsDBNull(21))
                    //{
                    //    _kod.release_date = _reader.GetDateTime(21).ToString();
                    //}

                 //   _kod.songname_order = this.get_number(_reader, 22);
                 //   _kod.singer_order = this.get_number(_reader, 23);
                 //   _kod.src_order = this.get_number(_reader, 24);
                  //  _kod.sony_asset_id = this.get_string(_reader, 25);
                  //  _kod.sony_image = this.get_string(_reader, 26);
                  //  _kod.sony_flg = this.get_string(_reader, 27);
                  //  _kod.lyricscopyright = this.get_string(_reader, 28);
                  //  _kod.musiccopyright = this.get_string(_reader, 29);
                  //  _kod.videocopyright = this.get_string(_reader, 30);
                    if (!_reader.IsDBNull(31))
                    {
                   //     _kod.base_count = _reader.GetDecimal(31);

                    }
                    else
                    {
//                        _kod.base_count = 0;
                    }

                    if (!_reader.IsDBNull(32))
                    {
  //                      _kod.real_count = _reader.GetDecimal(32);

                    }
                    else
                    {
    //                    _kod.real_count = 0;
                    };
                    _kod.specialcla = this.get_string(_reader, 33);
                    _kod.songname_order = this.get_number(_reader, 34);
                    _kod.singer_order = this.get_number(_reader, 35);

                    _result.Add(_kod);

                }


            }
            finally
            {
                conn.Close();
            }



            return _result;
        }

        [JsonRpcMethod("get_kod_string")]
        public string get_kod_string(int? limit)
        {
            List<kod_song> _json_object = this.get_kod_info(limit);
            string sJSON = JsonConvert.ExportToString(_json_object);

            return sJSON;
        }


        public string get_string(OracleDataReader _reader, int index)
        {
            string _result;
            if (!_reader.IsDBNull(index))
            {
                _result = _reader.GetString(index);
            }
            else
            {
                _result = "";
            }
            return _result;
        }

        public decimal get_number(OracleDataReader _reader, int index)
        {
            decimal _result;
            if (!_reader.IsDBNull(index))
            {
                _result = _reader.GetDecimal(index);
            }
            else
            {
                _result = 0;
            }
            return _result;
        }

    }


}