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
            ExStockInit();
            Timeslice.AddSecondEvent(StockStep);
        }

        #region 崇高石市场
        private int octaves = 2;
        private double persistence = 0.5;

        /// <summary>
        /// 分形用的玩意儿
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        private double Random1D(int px)
        {
            int n = px;
            n = (n << 13) ^ n;
            
            return (1f - (n * (n * n * 15731 + 789221) + 1376312589 & 0x7fffffff) / 1073741824.0);
        }
        private double Smooth1D(int x)
        {
            //return Random1D(x);

            double res = Random1D(x) * 0.6666;
            res += (Random1D(x - 1) + Random1D(x + 1)) * 0.3334;
            return res;
        }
        private double cosine_interpolate(double a, double b, double x)
        {
            double ft = x * Math.PI;
            double f = (1 - Math.Cos(ft)) * 0.5f;
            return a * (1 - f) + b * f;
        }
        private double InterpolatedNoise(double x)
        {
            int ix = (int)x;
            double fx = x - ix;
            return cosine_interpolate(Smooth1D(ix), Smooth1D(ix + 1), fx);
        }
        private double FractalRandom(double x)
        {
            var total = 0.0;
            for (int i = 0; i < octaves; i++)
            {
                var frequency = Math.Pow(2, i);
                var amplitude = Math.Pow(persistence, i);
                total += InterpolatedNoise(x * frequency) * amplitude;
            }
            return total;
        }
        
        public const int MiddleExPrice = 100;
        //由股市和散户分别可能造成的波动幅度
        public const int StockTrendCount = 50;
        public const int IOTrendCount = 50;
        private int curExPrice;
        //上一时间段内总的流入流出量
        private int lastStepIO;
        private double xxx;

        //开盘
        private void ExStockInit()
        {
            curExPrice = MiddleExPrice;
            lastStepIO = 0;
            xxx = DateTime.Now.Millisecond + DateTime.Now.Second;
        }

        //每两分钟发生的变化
        private void ExStockStep()
        {
            StringBuilder sb = new StringBuilder();
            curExPrice = (int)(MiddleExPrice + StockTrendCount * FractalRandom(xxx));
            xxx += 0.1;
            lastStepIO = 0;
        }

        /// <summary>
        /// 通知崇高石市场崇高石的变化量
        /// </summary>
        /// <param name="delta"></param>
        private void ExStockChange(int delta)
        {
            lastStepIO += delta;
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
            if (time >= 5)
            {
                ExStockStep();
                _S.Broadcast(curExPrice.ToString());
                time -= 5;
            }
        }

        public bool WhenParamIn(SendParam param)
        {
            if (!done) return true;
            if (param.param[0] == _S.GetText("Command_to_QueryStock") && param.param.Length == 1)
            {
                _S.Response(param.GroupQQ, param.QQ, "Response_to_QueryStock", curExPrice);
                //_S.Response(param.GroupQQ, param.QQ, intro);
                return false;
            }
            int opCnt;
            int realCost;
            if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 1)
            {
                opCnt = (int)Math.Floor(param.caster.ChaosCount / curExPrice);
                realCost = opCnt * curExPrice;
                if (_S.Cost(param.caster, realCost))
                {
                    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                }
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                realCost = opCnt * curExPrice;
                if (_S.Cost(param.caster, realCost))
                {
                    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                }
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 1)
            {
                opCnt = param.caster.ExCount;
                opCnt = -opCnt;
                realCost = Math.Abs(opCnt * curExPrice);
                DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                if (opCnt > param.caster.ExCount) return false;
                opCnt = -opCnt;
                realCost = Math.Abs(opCnt * curExPrice);
                DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                return false;
            }
            return true;
        }

    }
}
