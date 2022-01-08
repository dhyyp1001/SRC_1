using System;
using System.Windows.Forms;
using EasyModbus;
using System.Collections.Generic;
using System.Threading;

namespace SRC_1
{
    public partial class SRC : Form
    {
        readonly ModbusClient mc = new ModbusClient("192.168.123.102", 503);

        //사용색상
        System.Drawing.Color green = System.Drawing.Color.Green;
        System.Drawing.Color white = System.Drawing.Color.White;
        System.Drawing.Color black = System.Drawing.Color.Black;
        System.Drawing.Color red = System.Drawing.Color.Red;
        System.Drawing.Color yellow = System.Drawing.Color.Yellow;
        System.Drawing.Color control = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
        System.Drawing.Color activeCaption = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
        System.Drawing.Color c224 = System.Drawing.Color.FromArgb(224, 224, 224);
        System.Drawing.Color lgray = System.Drawing.Color.LightGray;

        public SRC()
        {
            InitializeComponent();
        }

        private void SRC_Load(object sender, EventArgs e)
        {
            EndSRC_button.Enabled = false;
            
            //가온
            heatting_start_button.Enabled = false;
            heatting_end_button.Enabled = false;
            //먼지제거
            remove_d_start_button.Enabled = false;
            remove_d_end_button.Enabled = false;
            //순환
            water_cycle_start_button.Enabled = false;
            water_cycle_end_button.Enabled = false;
            //그룹
            group_start_button.Enabled = false;
            //개별
            single_start_button.Enabled = false;
            //설정
            setting_button.Enabled = false;
            //탱크
            a_tank_button.Enabled = false;
            b_tank_button.Enabled = false;
            //긴급종료
            emergency_end_button.Enabled = false;
            emergency_end_button.BackColor = control;
            




            //modbus통신 연결
            mc.Connect();
        }

        private void timer1_Run(object sender, EventArgs e)
        {
            TimerFunction();
        }

        private void StartTimer()
        {
            timer1.Interval = 5000;
            timer1.Enabled = true;
        }

        private void StartSRC_button_Click(object sender, EventArgs e)
        {
            this.AcceptButton = StartSRC_button;
            StartTimer();

            StartSRC_button.Enabled = false;
            EndSRC_button.Enabled = true;
        }
        private void EndSRC_button_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            StartSRC_button.Enabled = true;
            EndSRC_button.Enabled = false;
           // Application.Restart();
        }

        //AllInfoRead
        private void TimerFunction()
        {
           
            
            //통신한 값 int 배열에 저장
            int[] rData0 = mc.ReadInputRegisters(0, 1);
            int[] rData1 = mc.ReadInputRegisters(1, 1);
            int[] rData2 = mc.ReadInputRegisters(2, 1);
            int[] rData3 = mc.ReadInputRegisters(3, 1);
            int[] rData4 = mc.ReadInputRegisters(4, 1);
            int[] rData5 = mc.ReadInputRegisters(5, 1);
            int[] rData6 = mc.ReadInputRegisters(6, 1);
            int[] rData8 = mc.ReadInputRegisters(8, 1);
            int[] rData9 = mc.ReadInputRegisters(9, 1);
            int[] rData10 = mc.ReadInputRegisters(10, 1);
            int[] rData11 = mc.ReadInputRegisters(11, 1);
            int[] rData12 = mc.ReadInputRegisters(12, 1);
            int[] rData13 = mc.ReadInputRegisters(13, 1);
            int[] rData14 = mc.ReadInputRegisters(14, 1);
            int[] rData15 = mc.ReadInputRegisters(15, 1);
            int[] rData16 = mc.ReadInputRegisters(16, 1);
            int[] rData17 = mc.ReadInputRegisters(17, 1);
            int[] rData19 = mc.ReadInputRegisters(19, 1);

            //주소별 변수명
            int startSRC = rData1[0];
            uint tank_A_level = (uint)rData2[0]; 
            uint tank_B_level = (uint)rData3[0];
            int nozzle_01_16 = rData4[0];
            int nozzle_17_32 = rData5[0];
            int nozzle_33_48 = rData6[0];
            uint to_stop_time = (uint)rData8[0];
            int air_temp = rData9[0];
            uint air_hum = (uint)rData10[0];
            uint dust = (uint)rData11[0];
            int road_temp = rData12[0];
            uint weather_name = (uint)rData13[0];
            uint precipitation_level = (uint)rData14[0];
            uint brine = (uint)rData15[0];
            uint seasonErr = (uint)rData16[0];
            uint s_alarm = (uint)rData17[0];

            int prame_start_code = rData0[0];
            int prame_end_code = rData19[0];

            //filter 
            int[] filter = new int[16];
            for (int i = 0; i <= filter.Length - 1; i++)
            {
                filter[i] = 1;
                if (i > 0) filter[i] = filter[i - 1] * 2;
            }

            //version
            if ((startSRC & filter[14]) == filter[14])
            {
                if ((startSRC & filter[15]) == filter[15])//11
                {
                    version_label.Text = "version error";
                    status_window.BackColor = System.Drawing.Color.Red;
                }
                else
                    version_label.Text = "Version " + "[V" + 1 + "]";
            }
            else
            {
                if ((startSRC & filter[15]) == filter[15])//10
                    version_label.Text = "Version " + "[V" + 2 + "]";
            }

            //status_label
             if ((startSRC & filter[13]) == filter[13])
             {
                status_window.ForeColor = white;
                status_window.BackColor = green;
                status_window.Text = "살포중";
             }
            else
            {
                status_window.ForeColor = black;
                status_window.BackColor = control;
                status_window.Text = "-";
            }
            
             //절기
            if ((startSRC & filter[12]) == filter[12])
             {
                season.Text = "동절기";
             }
            else
            {
                season.Text = "하절기";
            }

            //가온시작, 정지
            if ((startSRC & filter[11]) == filter[11])
            {
                heatting_status.Text = "정지";
                heatting_status.BackColor = lgray;
                heatting_status.ForeColor = black; ;
            }
            else
            {
                heatting_status.Text = "동작중";
                heatting_status.BackColor = green;
                heatting_status.ForeColor = white;
            }

            //이물질 제거
            if((startSRC & filter[7]) == filter[7])
            {
                remove_dust_status.Text = "정지";
                remove_dust_status.BackColor = lgray;
                remove_dust_status.ForeColor = black;
            }
            else
            {
                remove_dust_status.Text = "동작중";
                remove_dust_status.BackColor = green;
                remove_dust_status.ForeColor = white ;
            }

            //error
            if ((startSRC & filter[5]) == filter[5])
            {
                status_window.Text = "경보 *중고장";
                status_window.ForeColor = yellow;
                status_window.BackColor = red;
            }
            if ((startSRC & filter[3]) == filter[3])
            {
                status_window.Text = "에러 *탱크저수위";
                status_window.ForeColor = yellow;
                status_window.BackColor =red;
            }
            if ((startSRC & filter[2]) == filter[2])
            {
                status_window.Text = "에러 *모터과부하";
                status_window.ForeColor = yellow;
                status_window.BackColor = red;
            }

            //펌프, 벨브 상태
            if ((startSRC & filter[9]) == filter[9])
            {
                pump_b_label.Text = "B 펌프\n(동작중)";
                pump_b_label.ForeColor = white;
                pump_b_label.BackColor = green;
            }
            else
            {
                pump_b_label.Text = "B 펌프";
                pump_b_label.ForeColor = black;
                pump_b_label.BackColor = c224;
            }
           
            if ((startSRC & filter[8]) == filter[8])
            {
                pump_a_label.Text = "A 펌프\n(동작중)";
                pump_a_label.ForeColor = white;
                pump_a_label.BackColor = green;
            }
            else
            {
                pump_a_label.Text = "A 펌프";
                pump_a_label.ForeColor = black;
                pump_a_label.BackColor = c224;
            }

            //분사밸브
            if ((startSRC & filter[7]) == filter[7])
            {
                jet_label.Text = "분사밸브\n(열림)";
                jet_label.ForeColor = white;
                jet_label.BackColor = green;
            }
            else if ((startSRC & filter[6]) == filter[6])
            {
                jet_label.Text = "분사밸브\n(닫힘)";
                jet_label.ForeColor = black;
                jet_label.BackColor = c224;
            }

            //소금살포
            if ((startSRC & filter[5]) == filter[5])
            {
                soalt_jet_window.Text = "동작중";
                soalt_jet_window.ForeColor = white;
                soalt_jet_window.BackColor = green;
            }

            //제설경보발생
            if ((startSRC & filter[4]) == filter[4])
            {
                soalt_jet_window.Text = "제설경보 *자동제설필요";
                soalt_jet_window.ForeColor = yellow;
                soalt_jet_window.BackColor = red;
            }

            //PLC
            if ((startSRC & filter[1]) == filter[1])
            {
                plc_window.Text = "동작불가";
                plc_window.ForeColor = yellow;
                plc_window.BackColor = red;
            }
            else if((startSRC & filter[0]) == filter[0])
            {
                plc_window.Text = "정상동작";
                plc_window.ForeColor = black;
                plc_window.BackColor = activeCaption;
            }
            else
            {
                plc_window.Text = "-";
                plc_window.ForeColor = black;
                plc_window.BackColor = control;
            }

            //tank level
            //a
            t_a_progressBar.Minimum = 0;
            t_a_progressBar.Maximum = 100;
            t_a_progressBar.Value = (int)tank_A_level;
            tank_a_label.Text = Convert.ToString(tank_A_level)+"%";
            //b
            t_b_progressBar.Minimum = 0;
            t_b_progressBar.Maximum = 100;
            t_b_progressBar.Value = (int)tank_B_level;
            tank_b_label.Text = Convert.ToString(tank_B_level)+"%";

            //잔여시간
            to_stop_time_level.Text = Convert.ToString(to_stop_time) + "(초)";
            //대기온도
            air_temp_level.Text = Convert.ToString(air_temp) + "℃";
            //대기습도
            air_humidity_level.Text = Convert.ToString(air_hum) + "%";
            //먼지농도
            dust_level.Text = Convert.ToString(dust) + "ug/m3";
            //노면온도
            road_temp_level.Text = Convert.ToString(road_temp) + "℃";
            //날씨
            if (weather_name == 0) weather.Text = "눈/비 x"; 
            if (weather_name == 60) weather.Text = "비"; 
            if (weather_name == 70) weather.Text = "눈"; 
            if (weather_name == 67) weather.Text = "우박";
            //강수량
            precipitation.Text = Convert.ToString(precipitation_level) + "mm/h";
            //염수농도
            brine_level.Text = Convert.ToString(brine) + "%";

            if (prame_end_code == 0x0042)
            {
                timer1.Stop();
                StartSRC_button.Enabled = true;
                EndSRC_button.Enabled = false;
            }
            //절기 에러
            if (seasonErr == 10)
            {
                season.Text = "동절기\n 에러";
                season.BackColor = System.Drawing.Color.LightPink;
                season.ForeColor = black;
            }
            if (seasonErr == 11)
            {
                season.Text = "하절기\n 에러";
                season.BackColor = System.Drawing.Color.LightPink;
                season.ForeColor = black;
            }
            if (seasonErr == 12) season.Text =  "동절기\n 정상";
            if (seasonErr == 13) season.Text =  "하절기\n 정상";
            if (seasonErr == 0) season.Text = season.Text;
            //분사알림표
            if (s_alarm == 1) alarm1.BackColor = yellow; else alarm1.BackColor = lgray;
            if (s_alarm == 2) alarm2.BackColor = yellow; else alarm2.BackColor = lgray;
            if (s_alarm == 3) alarm3.BackColor = yellow; else alarm3.BackColor = lgray;
            if (s_alarm == 4) alarm4.BackColor = yellow; else alarm4.BackColor = lgray;
            
            //노즐
            if ((nozzle_01_16 & filter[0]) == filter[0]) n1.BackColor = green; else n1.BackColor = lgray;
            if ((nozzle_01_16 & filter[1]) == filter[1]) n2.BackColor = green; else n2.BackColor = lgray;
            if ((nozzle_01_16 & filter[2]) == filter[2]) n3.BackColor = green; else n3.BackColor = lgray;
            if ((nozzle_01_16 & filter[3]) == filter[3]) n4.BackColor = green; else n4.BackColor = lgray;
            if ((nozzle_01_16 & filter[4]) == filter[4]) n5.BackColor = green; else n5.BackColor = lgray;
            if ((nozzle_01_16 & filter[5]) == filter[5]) n6.BackColor = green; else n6.BackColor = lgray;
            if ((nozzle_01_16 & filter[6]) == filter[6]) n7.BackColor = green; else n7.BackColor = lgray;
            if ((nozzle_01_16 & filter[7]) == filter[7]) n8.BackColor = green; else n8.BackColor = lgray;
            if ((nozzle_01_16 & filter[8]) == filter[8]) n9.BackColor = green; else n9.BackColor = lgray;
            if ((nozzle_01_16 & filter[9]) == filter[9]) n10.BackColor = green; else n10.BackColor = lgray;
            if ((nozzle_01_16 & filter[10]) == filter[10]) n11.BackColor = green; else n11.BackColor = lgray;
            if ((nozzle_01_16 & filter[11]) == filter[11]) n12.BackColor = green; else n12.BackColor = lgray;
            if ((nozzle_01_16 & filter[12]) == filter[12]) n13.BackColor = green; else n13.BackColor = lgray;
            if ((nozzle_01_16 & filter[13]) == filter[13]) n14.BackColor = green; else n14.BackColor = lgray;
            if ((nozzle_01_16 & filter[14]) == filter[14]) n15.BackColor = green; else n15.BackColor = lgray;
            if ((nozzle_01_16 & filter[15]) == filter[15]) n16.BackColor = green; else n16.BackColor = lgray;

            if ((nozzle_17_32 & filter[0]) == filter[0]) n17.BackColor = green; else n17.BackColor = lgray;
            if ((nozzle_17_32 & filter[1]) == filter[1]) n18.BackColor = green; else n18.BackColor = lgray;
            if ((nozzle_17_32 & filter[2]) == filter[2]) n19.BackColor = green; else n19.BackColor = lgray;
            if ((nozzle_17_32 & filter[3]) == filter[3]) n20.BackColor = green; else n20.BackColor = lgray;
            if ((nozzle_17_32 & filter[4]) == filter[4]) n21.BackColor = green; else n21.BackColor = lgray;
            if ((nozzle_17_32 & filter[5]) == filter[5]) n22.BackColor = green; else n22.BackColor = lgray;
            if ((nozzle_17_32 & filter[6]) == filter[6]) n23.BackColor = green; else n23.BackColor = lgray;
            if ((nozzle_17_32 & filter[7]) == filter[7]) n24.BackColor = green; else n24.BackColor = lgray;
            if ((nozzle_17_32 & filter[8]) == filter[8]) n25.BackColor = green; else n25.BackColor = lgray;
            if ((nozzle_17_32 & filter[9]) == filter[9]) n26.BackColor = green; else n26.BackColor = lgray;
            if ((nozzle_17_32 & filter[10]) == filter[10]) n27.BackColor = green; else n27.BackColor = lgray;
            if ((nozzle_17_32 & filter[11]) == filter[11]) n28.BackColor = green; else n28.BackColor = lgray;
            if ((nozzle_17_32 & filter[12]) == filter[12]) n29.BackColor = green; else n29.BackColor = lgray;
            if ((nozzle_17_32 & filter[13]) == filter[13]) n30.BackColor = green; else n30.BackColor = lgray;
            if ((nozzle_17_32 & filter[14]) == filter[14]) n31.BackColor = green; else n31.BackColor = lgray;
            if ((nozzle_17_32 & filter[15]) == filter[15]) n32.BackColor = green; else n32.BackColor = lgray;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void season_Click(object sender, EventArgs e)
        {
            
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel57_Paint(object sender, PaintEventArgs e)
        {

        }


        //button click part
        private void heatting_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 55);
          
            if (== 55)
        }
    }
}
