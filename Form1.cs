using System;
using System.Windows.Forms;
using EasyModbus;
using System.Collections.Generic;
using System.Threading;

namespace SRC_1
{
    public partial class SRC : Form
    {
        readonly ModbusClient mc = new ModbusClient("61.37.102.154", 502);

        //"61.37.102.154"
        //"192.168.123.102"

        //사용색상
        readonly System.Drawing.Color green = System.Drawing.Color.Green;
        readonly System.Drawing.Color white = System.Drawing.Color.White;
        readonly System.Drawing.Color black = System.Drawing.Color.Black;
        readonly System.Drawing.Color red = System.Drawing.Color.Red;
        readonly System.Drawing.Color yellow = System.Drawing.Color.Yellow;
        readonly System.Drawing.Color control = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
        readonly System.Drawing.Color activeCaption = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
        readonly System.Drawing.Color c224 = System.Drawing.Color.FromArgb(224, 224, 224);
        readonly System.Drawing.Color lgray = System.Drawing.Color.LightGray;

        public SRC()
        {
            InitializeComponent();
        }

        private void SRC_Load(object sender, EventArgs e)
        {
            //drop박스류
            speed_dropbox.SelectedIndex = 1;
            speed_dropbox.Enabled = false;
            season_dropbox.SelectedIndex = 0;
            season_dropbox.Enabled = false;
            starting_num_textbox.Text = "5";
            port_textbox.Text = "502";
            runningtime_textbox.Text = "180000";
            //text박스류
            IP1.Enabled = false;
            IP2.Enabled = false;
            IP3.Enabled = false;
            IP4.Enabled = false;
            port_textbox.Enabled = false;
            runningtime_textbox.Enabled = false;
            starting_num_textbox.Enabled = false;

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
            group_end_button.Enabled = false;
            //개별
            single_start_button.Enabled = false;
            single_end_button.Enabled = false;
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

            //text박스류
            IP1.Enabled = true;
            IP2.Enabled = true;
            IP3.Enabled = true;
            IP4.Enabled = true;
            port_textbox.Enabled = true;
            runningtime_textbox.Enabled = true;
            starting_num_textbox.Enabled = true;

            //드롭박스
            speed_dropbox.Enabled = true;
            season_dropbox.Enabled = true;
            //가온
            heatting_start_button.Enabled = true;
            //먼지제거
            remove_d_start_button.Enabled = true;
            //순환
            water_cycle_start_button.Enabled = true;
            //설정
            setting_button.Enabled = true;
            //탱크
            a_tank_button.Enabled = true;
            b_tank_button.Enabled = true;
            //살포
            group_start_button.Enabled = true;
            single_start_button.Enabled = true;
            //긴급종료
            emergency_end_button.Enabled = true;
            emergency_end_button.BackColor = System.Drawing.Color.Orange;
        }
        private void EndSRC_button_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            StartSRC_button.Enabled = true;
            EndSRC_button.Enabled = false;

            //text박스류
            IP1.Enabled = false;
            IP2.Enabled = false;
            IP3.Enabled = false;
            IP4.Enabled = false;
            port_textbox.Enabled = false;
            runningtime_textbox.Enabled = false;
            starting_num_textbox.Enabled = false;

            //드롭박스
            speed_dropbox.Enabled = false;
            season_dropbox.Enabled = false;
            //가온
            heatting_start_button.Enabled = false;
            heatting_end_button.Enabled = false;
            //먼지제거
            remove_d_start_button.Enabled = false;
            remove_d_end_button.Enabled = false;
            //순환
            water_cycle_start_button.Enabled = false;
            water_cycle_end_button.Enabled = false;
            //설정
            setting_button.Enabled = false;
            //탱크
            a_tank_button.Enabled = false;
            b_tank_button.Enabled = false;
            //살포
            group_start_button.Enabled = false;
            group_end_button.Enabled = false;
            single_start_button.Enabled = false;
            single_end_button.Enabled = false;
            //긴급종료
            emergency_end_button.Enabled = false;
            emergency_end_button.BackColor = control;
        }

        //AllInfoRead
        private void TimerFunction()
        {
            //통신한 값 int 배열에 저장
           /* try
            {*/
                int[] rData0 = mc.ReadHoldingRegisters(0, 1);
                int[] rData1 = mc.ReadHoldingRegisters(1, 1);
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

                int prame_start_code = rData0[0];
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
                    season.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlLight);
                }
                else
                {
                    season.Text = "하절기";
                    season.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlLight);
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

                //탱크 용액 상태
                if ((startSRC & filter[10]) == filter[10])
                {
                    wet_status.Text = "*세척수";
                    wet_status.BackColor = control;
                    wet_status.ForeColor = black; ;
                }
                else
                {
                    wet_status.Text = "*제설용액";
                    wet_status.BackColor = control;
                    wet_status.ForeColor = black;
                }

                //이물질 제거
                if ((startSRC & filter[7]) == filter[7])
                {
                    remove_dust_status.Text = "정지";
                    remove_dust_status.BackColor = lgray;
                    remove_dust_status.ForeColor = black;
                }
                else
                {
                    remove_dust_status.Text = "동작중";
                    remove_dust_status.BackColor = green;
                    remove_dust_status.ForeColor = white;
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
                    status_window.BackColor = red;
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
                else if ((startSRC & filter[0]) == filter[0])
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
                tank_a_label.Text = Convert.ToString(tank_A_level) + "%";
                //b
                t_b_progressBar.Minimum = 0;
                t_b_progressBar.Maximum = 100;
                t_b_progressBar.Value = (int)tank_B_level;
                tank_b_label.Text = Convert.ToString(tank_B_level) + "%";

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

                //절기 에러
                if (seasonErr == 10)
                {
                    season.Text = "동절기\n 에러";
                    season.BackColor = red;
                    season.ForeColor = black;
                }
                if (seasonErr == 11)
                {
                    season.Text = "하절기\n 에러";
                    season.BackColor = red;
                    season.ForeColor = black;
                }
                if (seasonErr == 12) season.Text = "동절기\n 정상";
                if (seasonErr == 13) season.Text = "하절기\n 정상";
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
           /* }
            catch (Exception ex) { }*/
            }

        //button click part
        private void heatting_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 55);
            int[] rData101 = mc.ReadInputRegisters(101, 1);
            int a101_code = rData101[0];

            if ( a101_code == 55)
            {
                heatting_status.BackColor = green;
                heatting_status.ForeColor = white;
                heatting_status.Text = "정상작동중";
                heatting_start_button.Enabled = false;
                heatting_end_button.Enabled = true;
            }
            else
            {
                heatting_status.BackColor = red;
                heatting_status.ForeColor = black;
                heatting_status.Text = "에러";
            }
        }

        private void heatting_end_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 136);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if (a102_code == 136)
            {
                heatting_status.BackColor = green;
                heatting_status.ForeColor = white;
                heatting_status.Text = "중지";
                heatting_end_button.Enabled = false;
                heatting_start_button.Enabled = true;
            }
            else
            {
                heatting_status.BackColor = red;
                heatting_status.ForeColor = black;
                heatting_status.Text = "에러";
            }

        }

        private void remove_d_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 53);
            int[] rData101 = mc.ReadInputRegisters(101, 1);
            int a101_code = rData101[0];

            if (a101_code == 53)
            {
                remove_dust_status.BackColor = green;
                remove_dust_status.ForeColor = white;
                remove_dust_status.Text = "정상작동중";
                remove_d_start_button.Enabled = false;
                remove_d_end_button.Enabled = true;
            }
            else
            {
                remove_dust_status.BackColor = red;
                remove_dust_status.ForeColor = black;
                remove_dust_status.Text = "에러";
            }
        }

        private void remove_d_end_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 187);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if (a102_code == 187)
            {
                remove_dust_status.BackColor = lgray;
                remove_dust_status.ForeColor = black;
                remove_dust_status.Text = "중지";
                remove_d_end_button.Enabled = false;
                remove_d_start_button.Enabled = true;
            }
            else
            {
                remove_dust_status.BackColor = red;
                remove_dust_status.ForeColor = black;
                remove_dust_status.Text = "에러";
            }
        }

        private void group_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 49);
            mc.WriteSingleRegister(202, 85);
            int[] rData101 = mc.ReadInputRegisters(101, 1);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a101_code = rData101[0];
            int a102_code = rData102[0];

            if (a101_code == 49 && a102_code == 85)
            {
                sparge_status.BackColor = green;
                sparge_status.ForeColor = white;
                sparge_status.Text = "그룹운전\n정상작동중";
                group_start_button.Enabled = false;
                group_end_button.Enabled = true;
                single_start_button.Enabled = false;
            }
            else
            {
                sparge_status.BackColor = red;
                sparge_status.ForeColor = black;
                sparge_status.Text = "에러";
            }
        }

        private void group_end_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 170);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if (a102_code == 170)
            {
                sparge_status.BackColor = lgray;
                sparge_status.ForeColor = black;
                sparge_status.Text = "중지";
                group_end_button.Enabled = false;
                group_start_button.Enabled = true;
                single_start_button.Enabled = true;
            }
            else
            {
                sparge_status.BackColor = red;
                sparge_status.ForeColor = black;
                sparge_status.Text = "에러";
            }
        }

        private void single_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 51);
            mc.WriteSingleRegister(202, 85);
            int[] rData101 = mc.ReadInputRegisters(101, 1);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a101_code = rData101[0];
            int a102_code = rData102[0];

            const int NOZZLE_SIZE = 31;
            CheckBox[] nozzleArr = new CheckBox[NOZZLE_SIZE];
            Label[] nozzleArr2 = new Label[NOZZLE_SIZE];
            //노즐 값 배열에 저장
            nozzleArr[0] = checkBox1;
            nozzleArr[1] = checkBox2;
            nozzleArr[2] = checkBox3;
            nozzleArr[3] = checkBox4;
            nozzleArr[4] = checkBox5;
            nozzleArr[5] = checkBox6;
            nozzleArr[6] = checkBox7;
            nozzleArr[7] = checkBox8;
            nozzleArr[8] = checkBox9;
            nozzleArr[9] = checkBox10;
            nozzleArr[10] = checkBox11;
            nozzleArr[11] = checkBox12;
            nozzleArr[12] = checkBox13;
            nozzleArr[13] = checkBox14;
            nozzleArr[14] = checkBox15;
            nozzleArr[15] = checkBox16;
            nozzleArr[16] = checkBox17;
            nozzleArr[17] = checkBox18;
            nozzleArr[18] = checkBox19;
            nozzleArr[19] = checkBox20;
            nozzleArr[20] = checkBox21;
            nozzleArr[21] = checkBox22;
            nozzleArr[22] = checkBox23;
            nozzleArr[23] = checkBox24;
            nozzleArr[24] = checkBox25;
            nozzleArr[25] = checkBox26;
            nozzleArr[26] = checkBox27;
            nozzleArr[27] = checkBox28;
            nozzleArr[28] = checkBox29;
            nozzleArr[29] = checkBox30;
            nozzleArr[30] = checkBox31;
            //색상 변경 위치 배열저장
            nozzleArr2[0] = n1;
            nozzleArr2[1] = n2;
            nozzleArr2[2] = n3;
            nozzleArr2[3] = n4;
            nozzleArr2[4] = n5;
            nozzleArr2[5] = n6;
            nozzleArr2[6] = n7;
            nozzleArr2[7] = n8;
            nozzleArr2[8] = n9;
            nozzleArr2[9] = n10;
            nozzleArr2[10] = n11;
            nozzleArr2[11] = n12;
            nozzleArr2[12] = n13;
            nozzleArr2[13] = n14;
            nozzleArr2[14] = n15;
            nozzleArr2[15] = n16;
            nozzleArr2[16] = n17;
            nozzleArr2[17] = n18;
            nozzleArr2[18] = n19;
            nozzleArr2[19] = n20;
            nozzleArr2[20] = n21;
            nozzleArr2[21] = n22;
            nozzleArr2[22] = n23;
            nozzleArr2[23] = n24;
            nozzleArr2[24] = n25;
            nozzleArr2[25] = n26;
            nozzleArr2[26] = n27;
            nozzleArr2[27] = n28;
            nozzleArr2[28] = n29;
            nozzleArr2[29] = n30;
            nozzleArr2[30] = n31;

            for (int i = 0; i < NOZZLE_SIZE - 1; i++)
            {
                if (nozzleArr[i].Checked == true && a101_code == 51 && a102_code == 85)
                {
                    mc.WriteSingleRegister(205, i + 1);
                    int[] rData105 = mc.ReadInputRegisters(105, 1);
                    int a105_code = rData105[0];
                    if(a105_code == i + 1)
                    {
                        nozzleArr2[i].BackColor = green;
                    }
                    else
                    {
                        nozzleArr2[i].BackColor = lgray;
                    }
                }
            }

            if (a101_code == 51 && a102_code == 85)
            {
                single_start_button.Enabled = false;
                single_end_button.Enabled = true;
                group_start_button.Enabled = false;

                sparge_status.BackColor = green;
                sparge_status.ForeColor = white;
                sparge_status.Text = "개별운전\n정상작동중";
                single_start_button.Enabled = false;
                single_end_button.Enabled = true;
                group_start_button.Enabled = false;
            }
            else
            {
                sparge_status.BackColor = red;
                sparge_status.ForeColor = black;
                sparge_status.Text = "에러";
            }
        }

        private void single_end_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 170);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if(a102_code == 170)
            {
                single_start_button.Enabled = true;
                single_end_button.Enabled = false;
                group_start_button.Enabled = true;

                sparge_status.BackColor = lgray;
                sparge_status.ForeColor = black;
                sparge_status.Text = "중지";
            }
            else
            {
                sparge_status.BackColor = red;
                sparge_status.ForeColor = black;
                sparge_status.Text = "에러";
            }
        }

        private void emergency_end_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 204);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if (a102_code == 204)
            {
                Application.Restart();
            }
        }

        private void a_tank_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(203, 1);
            int[] rData103 = mc.ReadInputRegisters(103, 1);
            int a103_code = rData103[0];
           
            if (a103_code == 1)
            {
                a_tank_button.Enabled = true;
                t_a_progressBar.Enabled = true;
                tank_a_label.Enabled = true;

                b_tank_button.Enabled = false;
                t_b_progressBar.Enabled = false;
                tank_b_label.Enabled = false;

                a_tank_button.BackColor = control;
                a_tank_button.ForeColor = black;
                a_tank_button.Text = "A탱크";
            }
            else
            {
                a_tank_button.BackColor = red;
                a_tank_button.ForeColor = black;
                a_tank_button.Text = "에러";
            }
        }

        private void b_tank_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(203, 2);
            int[] rData103 = mc.ReadInputRegisters(103, 1);
            int a103_code = rData103[0];

            if (a103_code == 2)
            {
                b_tank_button.Enabled = true;
                t_b_progressBar.Enabled = true;
                tank_b_label.Enabled = true;

                a_tank_button.Enabled = false;
                t_a_progressBar.Enabled = false;
                tank_a_label.Enabled = false;

                b_tank_button.BackColor = control;
                b_tank_button.ForeColor = black;
                b_tank_button.Text = "B탱크";
            }
            else
            {
                b_tank_button.BackColor = red;
                b_tank_button.ForeColor = black;
                b_tank_button.Text = "에러";
            }
        }

        private void setting_button_Click(object sender, EventArgs e)
        {
            //전송횟수
            try
            {
                int starting_num = Convert.ToInt32(starting_num_textbox.Text);
                if (starting_num >= 10)
                {
                    mc.WriteSingleRegister(204, starting_num);
                }
                else
                {
                    starting_num_textbox.Text = "에러";
                }
            }
            catch(Exception ex){ }

            //계절박스
            if (season_dropbox.SelectedIndex == 0)
            {
                mc.WriteSingleRegister(201, 52);
                int[] rData101 = mc.ReadInputRegisters(101, 1);
                int a101_code = rData101[0];

                if (a101_code == 52)
                {
                    season.BackColor = green;
                    season.ForeColor = white;
                    season.Text = "하절기 \n준비 완료";
                    season.Enabled = false;
                    season.Enabled = true;
                }
                else
                {
                    season.BackColor = red;
                    season.ForeColor = black;
                    season.Text = "에러";
                }
            }
            if (season_dropbox.SelectedIndex == 1)
            {
                mc.WriteSingleRegister(202, 102);
                int[] rData102 = mc.ReadInputRegisters(102, 1);
                int a102_code = rData102[0];

                if (a102_code == 102)
                {
                    season.BackColor = green;
                    season.ForeColor = white;
                    season.Text = "동절기 \n준비 완료";
                }
                else
                {
                    season.BackColor = red;
                    season.ForeColor = black;
                    season.Text = "에러";
                }
            }
        }

        private void sensor_use_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(201, 54);
            int[] rData101 = mc.ReadInputRegisters(101, 1);
            int a101_code = rData101[0];

            if (a101_code == 54)
            {
                sensor_use_button.BackColor = green;
                sensor_use_button.Text = "준비 완료";
            }
            else
            {
                sensor_use_button.BackColor = red;
                season.Text = "에러";
            }
        }

        private void sensor_use_start_button_Click(object sender, EventArgs e)
        {
            mc.WriteSingleRegister(202, 119);
            int[] rData102 = mc.ReadInputRegisters(102, 1);
            int a102_code = rData102[0];

            if (a102_code == 119)
            {
                sensor_use_start_button.BackColor = green;
            }
            else
            {
                sensor_use_start_button.BackColor = red;
                season.Text = "에러";
            }
        }
    }
}
