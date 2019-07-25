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
        public static Stock Instance;
        public Stock()
        {
            Instance = this;
            time = 0;
            ExStockInit();
            TaxInit();
            Timeslice.AddSecondEvent(StockStep);
        }

        #region 交易税
        private Dictionary<PersonData, double> taxDic;
        private void TaxInit()
        {
            taxDic = new Dictionary<PersonData, double>();
        }

        private void TaxStep()
        {
            var keys = taxDic.Keys.ToArray();
            foreach (var key in keys)
            {
                var value = taxDic[key];
                taxDic[key] = value > 0.02 ? value - 0.02 : 0;
            }
        }

        private double GetTax(PersonData person)
        {
            if (taxDic == null) return 0;
            if (!taxDic.ContainsKey(person)) return 0;
            return taxDic[person];
        }

        private void TaxAdd(PersonData person)
        {
            if (taxDic == null) return;
            if (!taxDic.ContainsKey(person)) { taxDic.Add(person, 0.1);return; }
            taxDic[person] += 0.1;
        }
        #endregion

        #region 崇高石市场
        private int octaves = 3;
        private double persistence = 0.6;
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
        public const int StockTrendCount = 30;
        public const int IOTrendCount = 30;
        //散户流入流出总量预计
        public const double IO_Total = 1000.0;
        private int curExPrice;
        public int CurExPrice { get { return curExPrice; } }
        //上一时间段内总的流入流出量
        private double lastStepIO;
        private double xxx;

        //开盘
        private void ExStockInit()
        {
            curExPrice = MiddleExPrice;
            lastStepIO = 0;
            xxx = DateTime.Now.Millisecond + DateTime.Now.Second;
            ExStockStep();
        }

        //每两分钟发生的变化
        private void ExStockStep()
        {
            StringBuilder sb = new StringBuilder();
            var io = (50 * lastStepIO / IO_Total);
            io = io > IOTrendCount ? IOTrendCount : io < -IOTrendCount ? -IOTrendCount : io;
            curExPrice = (int)(MiddleExPrice + StockTrendCount * FractalRandom(xxx) + io);
            xxx += 0.1;
            lastStepIO = Math.Sign(lastStepIO) * Math.Abs(lastStepIO) * 0.8;
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
            if (time >= 60)
            {
                ExStockStep();
                TaxStep();
                _S.Broadcast("Response_to_QueryStock", curExPrice);
                time -= 60;
            }
        }

        public bool WhenParamIn(SendParam param)
        {
            //if (!done) return true;
            var taxrate = GetTax(param.caster);
            if (param.param[0] == _S.GetText("Command_to_QueryStock") && param.param.Length == 1)
            {
                _S.Response(param.GroupQQ, param.QQ, "Response_to_QueryStockPerson", curExPrice, param.QQ , (int)(taxrate * 100));
                //_S.Response(param.GroupQQ, param.QQ, intro);
                return false;
            }
            int opCnt;
            int realCost;
            if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 1)
            {
                opCnt = (int)Math.Floor(param.caster.ChaosCount / (curExPrice * (1 + taxrate)));
                if (opCnt == 0) return false;
                realCost = (int)Math.Ceiling(opCnt * curExPrice * (1 + taxrate));
                if (_S.Cost(param.caster, realCost))
                {
                    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                    ExStockChange(opCnt);
                    TaxAdd(param.caster);
                    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                }
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                realCost = (int)Math.Ceiling(opCnt * curExPrice * (1 + taxrate));
                if (_S.Cost(param.caster, realCost))
                {
                    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                    ExStockChange(opCnt);
                    TaxAdd(param.caster);
                    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                }
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 1)
            {
                opCnt = param.caster.ExCount;
                if (opCnt == 0) return false;
                opCnt = -opCnt;
                realCost = Math.Abs((int)Math.Floor(opCnt * curExPrice * (1 - taxrate)));
                DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
                ExStockChange(opCnt);
                TaxAdd(param.caster);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                if (opCnt > param.caster.ExCount) return false;
                opCnt = -opCnt;
                realCost = Math.Abs((int)Math.Floor(opCnt * curExPrice * (1 - taxrate)));
                DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
                ExStockChange(opCnt);
                TaxAdd(param.caster);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                return false;
            }
            return true;
        }

    }
}
