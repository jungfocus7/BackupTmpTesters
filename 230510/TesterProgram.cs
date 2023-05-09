using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ThStringExts__Tester
{
    public static class TesterProgram
    {
        public static void Main(string[] args)
        {

            bool b1 = ThStringExts.CheckInLike(
                ".임헌진. 최고다 대학교 박종명 교수님의 명강을 듣고 싶습니다. 임헌진..", "%임헌진%");

            bool b2 = ThStringExts.CheckInLike(
                "서현진 울 때 입술 파르르 떠는 디테일 진짜 미쳤다.. 연기의 신이다..", "%서현진%");

            bool b3 = ThStringExts.CheckInLike(
                "Dr romantic 3 with Yoon Se jeong , Kang Dong Joo , Cha eun jae and Seo Woo Jin..and I want the other cast also to remain the same😊",
                "%Kang Dong Joo , xxx Cha eun jae and Seo Woo%");

            bool b4 = ThStringExts.CheckInLike(
                "막컷  표!정 김사부 왤케 귀여워 😊<딱 이 표정인데",
                "%표정%");



            //var x1 = prCheckContains("012XXX120", "XXX", 1);
            //var x2 = prCheckContains("가나다AAA##", "가나다", 1);
            //var x3 = prCheckContains("qltkdrn김사부AAA##", "김사부", 1);
            //var x4 = prCheckContains("마이크로소프트 윈도우즈 12", "윈도우즈", 1);
            //var x5 = prCheckContains("012XXX120", "XXXX", 1);
        }




        private static bool prCheckContains(string ts, string es, int m)
        {
            int li = ts.Length;
            int lj = es.Length;

            if ((li > lj) && (lj > 2) && (m >= 0) && (m < li))
            {
                li -= m;

                int j = 0;
                for (int i = m; i < li; ++i)
                {
                    char x = ts[i];
                    char y = es[j];
                    if (x == y)
                    {
                        if (++j == lj)
                            break;
                    }
                    else if (j > 0)
                    {
                        j = 0;
                        break;
                    }
                }

                return j > 0;
            }

            return false;
        }
    }



    public static class ThStringExts
    {
        private const char m_eps = '%';

        private static bool prCheckContains(string ts, string es, int m)
        {
            int li = ts.Length;
            int lj = es.Length;

            if ((li > lj) && (lj > 2) && (m >= 0) && (m < li))
            {
                li -= m;

                int j = 0;
                for (int i = m; i < li; ++i)
                {
                    char x = ts[i];
                    char y = es[j];
                    if (x == y)
                    {
                        if (++j == lj)
                            break;
                    }
                    else if (j > 0)
                    {
                        j = 0;
                    }
                }

                return j > 0;
            }

            return false;
        }        

        public static bool CheckInLike(string ts, string es)
        {
            if (string.IsNullOrWhiteSpace(ts)) return false;
            if (string.IsNullOrWhiteSpace(es)) return false;
            if (ts.Length < es.Length) return false;

            bool rb = false;
            if (es.Length > 2)
            {                
                char sc = es[0];
                char ec = es[es.Length - 1];
                if ((sc == m_eps) && (ec == m_eps))
                {
                    string rs = es.Trim(m_eps);
                    rb = prCheckContains(ts, rs, 1);
                }
            }

            return rb;
        }
    }

}
