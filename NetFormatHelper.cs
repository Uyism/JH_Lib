using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jh_Lib
{
    /* ex) StructRequest
            "uid": "1",
            "request_url": "SyncMovement",
            "parameter": 
            {
                "opponentPosX": "0",
                "opponentPosY": "0"
            }
     */
    public struct StructRequest
    {
        public string uid;
        public string request_url;
        public Dictionary<string, string> parameter;
    }

    public enum URL { InitUser, SyncMovement, AttackBomb, SetBombSocket, GetOpponentData }

    public class NetFormatHelper
    {
        public static string ByteToString(byte[] data)
        { 
            String msg = Encoding.Default.GetString(data);
            return msg;
        }

        public static byte[] StringToByte(string send_msg)
        {
            byte[] send_data = new byte[1024 * 2];
            send_data = Encoding.Default.GetBytes(send_msg);
            return send_data;
        }

        static public  string StructRequestToString(StructRequest request)
        {

            JObject obj = new JObject();
            obj.Add("uid", request.uid);
            obj.Add("request_url", request.request_url);
            Dictionary<string, string> dic = request.parameter;

            if (dic != null)
                obj["parameter"] = JObject.FromObject(dic).ToString();

            return obj.ToString();
        }

        static public StructRequest StringToStructRequest(string msg)
        {
            JObject obj = JObject.Parse(msg);
            StructRequest reqeust = new StructRequest();
            reqeust.uid = obj.Value<string>("uid");
            reqeust.request_url = obj.Value<string>("request_url");

            if (obj.ContainsKey("parameter"))
            {
                string parameter = obj.Value<string>("parameter");
                Dictionary<string, string> praram = JsonConvert.DeserializeObject<Dictionary<string, string>>(parameter);
                reqeust.parameter = praram;
            }
            return reqeust;
        }
    }
}
