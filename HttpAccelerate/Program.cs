using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HttpAccelerate
{
    class Program
    {
        static void Main(string[] args)
        {
            var frm = new FrmStartup();
            frm.ShowDialog();
            while (true)
            {
                var fileName = "url.txt";
                string[] urls = null;
                try
                {
                    urls = System.IO.File.ReadAllLines(fileName);

                    Parallel.ForEach(urls, (url) =>
                    {
                        if (!string.IsNullOrEmpty(url))
                        {
                            try
                            {
                                var request = (HttpWebRequest)WebRequest.Create(url);
                                request.Credentials = CredentialCache.DefaultCredentials;
                                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                                var response = request.GetResponse();
                                new StreamReader(response.GetResponseStream()).ReadToEnd();
                                response.Close();
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }
                    });
                    //Console.WriteLine("[{0}]--OK.", DateTime.Now);
                    Thread.Sleep(100);
                }
                catch (Exception ex2)
                {
                    //Console.WriteLine(ex2.Message);
                }
            }
        }
    }
}
