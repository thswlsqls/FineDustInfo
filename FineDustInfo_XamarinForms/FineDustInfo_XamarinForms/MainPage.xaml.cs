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

namespace FineDustInfo_Grid_Test
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string search = string.Empty;
        string StationName = string.Empty;
        List<string> StationNameList = new List<string>();
        string stationnamelist = string.Empty;

        public MainPage()
        {
            InitializeComponent();
            Label_updatetime_Clicked();
            Label_value_Clicked();
        }

        private void Button_검색_Clicked(object sender, EventArgs e)
        {
            string seoul_stationname_list = string.Empty;
            seoul_stationname_list = "영등포구 금천구 용산구 영등포로 홍릉로 강동구 천호대로 송파구 화랑로 도봉구 정릉로 신촌로 강변북로 동작대로 중앙차로 시흥대로 광진구 공항대로 서대문구 관악구 강남대로 종로구 양천구 중랑구 마포구 강서구 강남구 노원구 성동구 은평구 동작구 서초구 도산대로 성북구 한강대로 강북구 종로 구로구 중구 청계천로 동대문구";
            DisplayAlert("서울시 측정소 목록", seoul_stationname_list, "OK");
        }

        private void SearchBar_stationname_SearchButtonPressed(object sender, EventArgs e)
        {
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

            search = SearchBar_stationname.Text; //검색한 입력 값을 저장
            int count = 0;
            try
            {
                foreach (XmlNode xn in xnList1)
                {
                    StationName = xn["stationName"].InnerText; //측정소명
                    StationNameList.Add(StationName);
                    string KhaiGrade = xn["khaiGrade"].InnerText; //통합대기환경지수
                    string Pm25Value = xn["pm25Value"].InnerText; //미세먼지농도
                    string UpdateTime = xn["dataTime"].InnerText; //업데이트시간

                    void printlabel()
                    {
                        Label_value.Text = "미세먼지농도: " + Pm25Value;
                        Label_updatetime.Text = "updatetime: " + UpdateTime;
                    }

                    if (search == StationName) //파싱하여 얻은 측정소명과 비교
                    {
                        if (KhaiGrade == "1") // 통합대기환경지수가 '좋음' 등급이면,
                        {
                            BackgroundColor = Color.RoyalBlue;
                            Label_status.BackgroundColor = Color.CornflowerBlue;
                            Label_value.BackgroundColor = Color.CornflowerBlue;
                            Label_updatetime.BackgroundColor = Color.RoyalBlue;
                            Label_status.Text = "좋음";
                            printlabel();
                        }
                        else if (KhaiGrade == "2") // 통합대기환경지수가 '보통' 등급이면,
                        {
                            BackgroundColor = Color.MediumAquamarine;
                            Label_status.BackgroundColor = Color.Aquamarine;
                            Label_value.BackgroundColor = Color.Aquamarine;
                            Label_updatetime.BackgroundColor = Color.MediumAquamarine;
                            Label_status.Text = "보통";
                            printlabel();
                        }
                        else if (KhaiGrade == "3") // 통합대기환경지수가 '나쁨' 등급이면,
                        {
                            BackgroundColor = Color.OrangeRed;
                            Label_status.BackgroundColor = Color.Coral;
                            Label_value.BackgroundColor = Color.Coral;
                            Label_updatetime.BackgroundColor = Color.OrangeRed;
                            Label_status.Text = "나쁨";
                            printlabel();
                        }
                        else // 통합대기환경지수가 '매우 나쁨' 등급이면,
                        {
                            BackgroundColor = Color.Red;
                            Label_status.BackgroundColor = Color.Firebrick;
                            Label_value.BackgroundColor = Color.Firebrick;
                            Label_updatetime.BackgroundColor = Color.Red;
                            Label_status.Text = "매우 나쁨";
                            printlabel();
                        }
                    }
                    if (search != StationName)
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
            catch (NullReferenceException ex) { DisplayAlert("error", "NullReferenceException예외가 발생했습니다. 개체 참조가 개체의 인스턴스로 설정되지 않았습니다.", "OK"); }
            catch (ArgumentOutOfRangeException ex) { DisplayAlert("error", "System.ArgumentOutOfRangeException예외가 발생했습니다. 인덱스가 범위를 벗어났습니다. 인덱스는 음수가 아니어야 하며 컬렉션의 크기보다 작아야 합니다.", "OK"); }


        }

        private void Label_value_Focused(object sender, FocusEventArgs e)
        {
            DisplayAlert("error", "InvalidOperationException가 발생했습니다.", "OK");
        }

        private void Label_updatetime_Clicked()
        {
            Label_updatetime.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("updatetime안내", "측정치는 1시간마다 업데이트됩니다.", "OK");
                })
            });
        }

        async private void Label_value_Clicked()
        {
            Label_value.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Page1 page1 = new Page1();
                    page1.Passvalue = search;
                    Navigation.PushAsync(page1);
                })
            });
        }

        async private void Button_NextPage_Clicked(object sender, EventArgs e)
        {
            Page1 page1 = new Page1();
            page1.Passvalue = search;
            await Navigation.PushAsync(page1);

        }
    }
}
