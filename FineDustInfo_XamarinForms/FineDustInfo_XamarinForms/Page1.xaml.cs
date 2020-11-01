using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Xml;
using System.IO;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineDustInfo_Grid_Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Label_so2Value_Clicked();
            Label_coValue_Clicked();
            Label_o3Value_Clicked();
            Label_no2Value_Clicked();
            Label_pm10Value_Clicked();
            Label_pm25Value_Clicked();
        }
        private string Page1_value;
        public string Passvalue
        {
            get { return this.Page1_value; }
            set { this.Page1_value = value; }
        }
        private void Button_수치확인_Clicked(object sender, EventArgs e)
        {
            string search = Passvalue;
            DisplayAlert("측정소명 확인", "측성소명: " + search, "OK");

            string xmlfile = string.Empty;
            string url = "http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getCtprvnRltmMesureDnsty?serviceKey=XhTa978BeUEMjroqJWgb%2FH9pBWX5QMxmE6MUSw15i7hs6epmOucqjl%2BXnn6ruZRIKsZ%2FTFluLxMd42F3vIvb1A%3D%3D&numOfRows=40&pageNo=1&sidoName=%EC%84%9C%EC%9A%B8&ver=1.1";
            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                    xmlfile = reader.ReadToEnd();
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlfile);
            XmlNodeList xnList1 = xml.GetElementsByTagName("item");
            int count = 0;

            foreach (XmlNode xn in xnList1)
            {
                string stationName = xn["stationName"].InnerText; //측정소명
                string khaiGrade = xn["khaiGrade"].InnerText; //통합대기환경지수

                string so2Value = xn["so2Value"].InnerText; //아황산가스농도
                string coValue = xn["coValue"].InnerText; //일산화탄소농도
                string o3Value = xn["o3Value"].InnerText; //오존농도
                string no2Value = xn["no2Value"].InnerText; //이산화탄소농도
                string pm10Value = xn["pm10Value"].InnerText; //미세먼지농도
                string pm25Value = xn["pm25Value"].InnerText; //초미세먼지농도

                string so2Grade = xn["so2Grade"].InnerText;
                string coGrade = xn["coGrade"].InnerText;
                string o3Grade = xn["o3Grade"].InnerText;
                string no2Grade = xn["no2Grade"].InnerText;
                string pm10Grade = xn["pm10Grade"].InnerText;
                string pm25Grade = xn["pm25Grade"].InnerText; //...등급

                string Grade(string grade) //등급에 따라 출력할 문자열 지정
                {
                    if (grade == "1")
                        return "좋음";
                    else if (grade == "2")
                        return "보통";
                    else if (grade == "3")
                        return "나쁨";
                    else
                        return "매우 나쁨";
                }

                void printlabel()
                {
                    Label_so2Value.Text = "아황산가스농도(ppm): " + so2Value + "  " + Grade(so2Grade);
                    Label_coValue.Text = "일산화탄소농도(ppm): " + coValue + "  " + Grade(coGrade);
                    Label_o3Value.Text = "오존농도(ppm): " + o3Value + "  " + Grade(o3Grade);
                    Label_no2Value.Text = "이산화질소농도(ppm): " + no2Value + "  " + Grade(no2Grade);
                    Label_pm10Value.Text = "미세먼지농도(mg/m^3): " + pm10Value + "  " + Grade(pm10Grade);
                    Label_pm25Value.Text = "초미세먼지농도(mg/m^3): " + pm25Value + "  " + Grade(pm25Grade);
                }

                if (search == stationName)
                {
                    if (khaiGrade == "0")
                    {
                        return;
                    }
                    else if (khaiGrade == "1") // 통합대기환경지수가 '좋음' 등급이면,
                    {
                        BackgroundColor = Color.RoyalBlue;
                        Label_so2Value.BackgroundColor = Color.CornflowerBlue;
                        Label_coValue.BackgroundColor = Color.CornflowerBlue;
                        Label_o3Value.BackgroundColor = Color.CornflowerBlue;
                        Label_no2Value.BackgroundColor = Color.CornflowerBlue;
                        Label_pm10Value.BackgroundColor = Color.CornflowerBlue;
                        Label_pm25Value.BackgroundColor = Color.CornflowerBlue;
                        printlabel();
                    }
                    else if (khaiGrade == "2") // 통합대기환경지수가 '보통' 등급이면,
                    {
                        BackgroundColor = Color.MediumAquamarine;
                        Label_so2Value.BackgroundColor = Color.Aquamarine;
                        Label_coValue.BackgroundColor = Color.Aquamarine;
                        Label_o3Value.BackgroundColor = Color.Aquamarine;
                        Label_no2Value.BackgroundColor = Color.Aquamarine;
                        Label_pm10Value.BackgroundColor = Color.Aquamarine;
                        Label_pm25Value.BackgroundColor = Color.Aquamarine;
                        printlabel();
                    }
                    else if (khaiGrade == "3") // 통합대기환경지수가 '나쁨' 등급이면,
                    {
                        BackgroundColor = Color.OrangeRed;
                        Label_so2Value.BackgroundColor = Color.Coral;
                        Label_coValue.BackgroundColor = Color.Coral;
                        Label_o3Value.BackgroundColor = Color.Coral;
                        Label_no2Value.BackgroundColor = Color.Coral;
                        Label_pm10Value.BackgroundColor = Color.Coral;
                        Label_pm25Value.BackgroundColor = Color.Coral;
                        printlabel();
                    }
                    else // 통합대기환경지수가 '매우 나쁨' 등급이면,
                    {
                        BackgroundColor = Color.Red;
                        Label_so2Value.BackgroundColor = Color.Firebrick;
                        Label_coValue.BackgroundColor = Color.Firebrick;
                        Label_o3Value.BackgroundColor = Color.Firebrick;
                        Label_no2Value.BackgroundColor = Color.Firebrick;
                        Label_pm10Value.BackgroundColor = Color.Firebrick;
                        Label_pm25Value.BackgroundColor = Color.Firebrick;
                        printlabel();
                    }
                }

                if (search != stationName)
                {
                    count++;
                }
                if (40 == count) //마지막 노드까지 일치하는 측정소 명을 찾지 못했으면,
                {
                    DisplayAlert("error", "잘못된 측정소명입니다.", "OK");
                    count = 0;
                }
            }
        }

        async private void Button_검색창으로_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Label_so2Value_Clicked()
        {
            Label_so2Value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("아황산가스 농도범위 확인", "0~0.02: 좋음\r0.021~0.05: 보통\r0.051~0.15: 나쁨\r0.0151~: 매우나쁨", "OK");
                })
            });
        }

        private void Label_coValue_Clicked()
        {
            Label_coValue.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("일산화탄소 농도범위 확인", "0~2: 좋음\r2.01~9: 보통\r9.01~15: 나쁨\r15.01~: 매우나쁨", "OK");
                })
            });
        }

        private void Label_o3Value_Clicked()
        {
            Label_o3Value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("오존 농도범위 확인", "0~0.03: 좋음\r0.031~0.09: 보통\r0.091~0.15: 나쁨\r0.151~: 매우나쁨", "OK");
                })
            });
        }

        private void Label_no2Value_Clicked()
        {
            Label_no2Value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("이산화질소 농도범위 확인", "0~0.03: 좋음\r0.031~0.06: 보통\r0.061~0.2: 나쁨\r0.201~: 매우나쁨", "OK");
                })
            });
        }

        private void Label_pm10Value_Clicked()
        {
            Label_pm10Value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("미세먼지 농도범위 확인", "0~30: 좋음\r31~80: 보통\r81~150: 나쁨\r151~: 매우나쁨", "OK");
                })
            });
        }

        private void Label_pm25Value_Clicked()
        {
            Label_pm25Value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("초미세먼지 농도범위 확인", "0~15: 좋음\r16~35: 보통\r36~75: 나쁨\r76~: 매우나쁨", "OK");
                })
            });
        }

    }
}
