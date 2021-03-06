﻿using System.Collections.Generic;
using System.Linq;

namespace FreezerProUtility.Fp_BLL
{
    public class SampleSocrce
    {
        public static List<Fp_Model.SampleSourceTypes> GetAll(Fp_Common.UnameAndPwd up)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username", up.UserName);
            dic.Add("password", up.PassWord);
            dic.Add("method", Fp_Common.FpMethod.sample_source_types.ToString());
            Fp_DAL.CallApi call = new Fp_DAL.CallApi(dic);
            List<Fp_Model.SampleSourceTypes> List = call.getdata<Fp_Model.SampleSourceTypes>("SampleSourceTypes");
            return List;
        }

        public Fp_Model.SampleSourceTypes GetSampleSourceTypeByTypeName(Fp_Common.UnameAndPwd up, string name)
        {
            List<Fp_Model.SampleSourceTypes> list = GetAll(up);
            Fp_Model.SampleSourceTypes resObj = new Fp_Model.SampleSourceTypes();
            if (list != null && list.Count > 0)
            {
                resObj = list.Where<Fp_Model.SampleSourceTypes>(a => a.name == name).FirstOrDefault();
            }
            return resObj;
        }

        public static string ImportSampleSourceDataToFp(Fp_Common.UnameAndPwd up, string sampleSourceTypeName, Dictionary<string, string> jsonDic)
        {
            string result = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username", up.UserName);
            dic.Add("password", up.PassWord);
            dic.Add("method", Fp_Common.FpMethod.import_sources.ToString());
            dic.Add("sample_source_type", sampleSourceTypeName);
            if (jsonDic != null && jsonDic.Count > 0)
            {
                dic.Add("json", Fp_Common.FpJsonHelper.DictionaryToJsonString(jsonDic));
            }
            Fp_DAL.CallApi call = new Fp_DAL.CallApi(dic);
            result = call.PostData();
            return result;
        }

        public static Dictionary<string, string> GetAllIdAndNamesDic(Fp_Common.UnameAndPwd up)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<FreezerProUtility.Fp_Model.SampleSourceTypes> list = GetAll(up);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    dic.Add(item.id, item.name);
                }
            }
            return dic;
        }

        private static string CheckImportRes(string jsonResStr)
        {
            //检测是否导入成功
            if (string.IsNullOrEmpty(jsonResStr))
            {
                return "url或方法错误";
            }
            else if (jsonResStr == "1")
            {
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}