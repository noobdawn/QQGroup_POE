using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NiLiuShui.IRQQ.CSharp
{
    class SomeAPI : IModule
    {
        const string CLOUD_MUSIC_XML = "<?xml version='1.0' encoding='UTF-8' standalone='yes'?><msg templateID=\"123\" url=\"http://music.163.com/song?id={0}\" serviceID=\"143\" action=\"web\" actionData=\"\" a_actionData=\"\" i_actionData=\"\" brief=\"网易云音乐 的分享\" flag=\"0\"><item layout=\"2\"><picture cover=\"\"/><title>分享单曲</title><summary>Pendulum</summary></item><source url=\"\" icon=\"\" name=\"网易云音乐\" appid=\"0\" action=\"\" actionData=\"\" a_actionData=\"\" i_actionData=\"\"/></msg>";

        public bool WhenParamIn(SendParam param)
        {
            if (param.param[0] == _S.GetText("Command_to_Song") && param.param.Length == 2)
            {
                //新的接口
                string url = "http://music.163.com/api/search/pc";
                string p = string.Format("s={0}&type=1&limit=1&offset=0", param.param[1]);
                _S.NetPost(url, p, (stream) =>
                {
                    var sstream = new StreamReader(stream, Encoding.UTF8);
                    string json = sstream.ReadToEnd();
                    sstream.Close();
                    sstream.Dispose();
                    LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(json);
                    if (!jsonData.ContainsKey("result") || !jsonData["result"].ContainsKey("songs")) return;
                    string sid = jsonData["result"]["songs"][0]["id"].ToString();
                    var xml = string.Format(CLOUD_MUSIC_XML, sid);
                    IRQQApi.Api_SendXML(Config.RobotQQ, 1, 2, param.GroupQQ, param.QQ, xml, 2);
                }, null, ";charset=gb2312");

                return false;
            }

            return true;
        }
    }
}
