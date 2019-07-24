using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 股市
    /// </summary>
    class Stock : IModule
    {

        public Stock()
        {
            time = 0;
            MirrorInit();
            Timeslice.AddSecondEvent(StockStep);
        }

        #region 崇高石市场
        public const int MaxExPrice = 150;
        public const int MinExPrice = 50;
        public const int MiddleExPrice = 100;
        private int curExPrice;
        //上一时间段内总的流入流出量
        private int lastStepIO;
        //下一时间段原本待定的涨幅
        private int trend;

        //开盘
        private void ExStockInit()
        {
            curExPrice = MiddleExPrice;
            lastStepIO = 0;
            trend = 1000;
        }

        //每两分钟发生的变化
        private void ExStockStep()
        {

        }

        /// <summary>
        /// 通知崇高石市场崇高石的变化量
        /// </summary>
        /// <param name="delta"></param>
        private void ExStockChange(int delta)
        {

        }
        #endregion

        #region 镜子市场
        int curMirrorPrice;
        bool done;
        string intro;
        
        private void MirrorInit()
        {
            done = false;
            intro = "";
            curMirrorPrice = 0;
            MirrorUpdate();
        }
        private void MirrorUpdate()
        {
            done = false;
            _S.NetGet("http://hq.sinajs.cn/list=sz000725", (stream) => {
                var sstream = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                string content = sstream.ReadToEnd();
                sstream.Close();
                sstream.Dispose();
                //处理数据
                content = content.Substring(21, content.Length - 23);
                var param = content.Split(new char[] { ',' }, StringSplitOptions.None);
                intro = _S.GetText("Response_to_QueryMirror",
                    GetIntFromStockValue(param[1]),
                    GetIntFromStockValue(param[2]),
                    GetIntFromStockValue(param[3]),
                    GetIntFromStockValue(param[4]),
                    GetIntFromStockValue(param[5]));
                curMirrorPrice = GetIntFromStockValue(param[3]);
                done = true;
            },
            (e) => {
                done = false;
            });

            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://hq.sinajs.cn/list=sz000725");
            //    request.Method = "GET";
            //    using (WebResponse wr = request.GetResponse())
            //    {
            //        var stream = wr.GetResponseStream();
            //        var sstream = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            //        string content = sstream.ReadToEnd();
            //        stream.Close();
            //        stream.Dispose();
            //        sstream.Close();
            //        sstream.Dispose();
            //        //处理数据
            //        content = content.Substring(21, content.Length - 23);
            //        var param = content.Split(new char[] { ',' }, StringSplitOptions.None);
            //        intro = _S.GetText("Response_to_QueryMirror",
            //            GetIntFromStockValue(param[1]),
            //            GetIntFromStockValue(param[2]),
            //            GetIntFromStockValue(param[3]),
            //            GetIntFromStockValue(param[4]),
            //            GetIntFromStockValue(param[5]));
            //        curMirrorPrice = GetIntFromStockValue(param[3]);
            //        done = true;
            //    }
            //}
            //catch(System.Exception e)
            //{
            //    IRQQApi.Api_OutPutLog(e.ToString());
            //    done = false;
            //}
        }

        public int GetIntFromStockValue(string param)
        {
            double value;
            if (double.TryParse(param, out value))
            {
                return (int)(value * 1000);
            }
            return 0;
        }
        #endregion

        int time = 0;
        private void StockStep(object sender, ElapsedEventArgs e)
        {
            time++;
            if (time >= 20)
            {
                MirrorUpdate();
                if (done)
                    _S.Broadcast("Response_to_QueryStock", intro);
                time -= 20;
            }
        }

        public bool WhenParamIn(SendParam param)
        {
            if (!done) return true;
            if (param.param[0] == _S.GetText("Command_to_QueryStock") && param.param.Length == 1)
            {
                //_S.Response(param.GroupQQ, param.QQ, "Response_to_QueryStock", curPrice);
                _S.Response(param.GroupQQ, param.QQ, intro);
                return false;
            }
            //int opCnt;
            //int realCost;
            //if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 1)
            //{
            //    opCnt = (int)Math.Floor(param.caster.ChaosCount / curPrice);
            //    realCost = opCnt * curPrice;
            //    if (_S.Cost(param.caster, realCost))
            //    {
            //        DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
            //        _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
            //    }
            //    return false;
            //}
            //if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            //{
            //    if (opCnt <= 0) return false;
            //    realCost = opCnt * curPrice;
            //    if (_S.Cost(param.caster, realCost))
            //    {
            //        DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
            //        _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
            //    }
            //    return false;
            //}
            //if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 1)
            //{
            //    opCnt = param.caster.ExCount;
            //    opCnt = -opCnt;
            //    realCost = Math.Abs(opCnt * curPrice);
            //    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
            //    DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
            //    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
            //    return false;
            //}
            //if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            //{
            //    if (opCnt <= 0) return false;
            //    if (opCnt > param.caster.ExCount) return false;
            //    opCnt = -opCnt;
            //    realCost = Math.Abs(opCnt * curPrice);
            //    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
            //    DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
            //    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
            //    return false;
            //}
            return true;
        }

    }
}
