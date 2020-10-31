using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace send_App1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private bool checkjson(string indata)
        {            
            if (!string.IsNullOrEmpty(indata))
            {
                string json = indata;

                if (JsonSplit.IsJson(json))//传入的json串
                {
                    MessageBox.Show("json格式合法");
                    return true;
                }
                else
                {
                    MessageBox.Show("json格式不合法");
                    return false;
                }
            }
            return false;
        }

        //Create the Connection Factory  
        
        IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616/");
        private void clicksenddata()
        {
            try
            {
                if (radioButton1.Checked)
                {
                    //未签收
                    string recivestatus = "1";

                    using (IConnection connection = factory.CreateConnection())
                    {
                        //Create the Session  
                        using (ISession session = connection.CreateSession())
                        {
                            //Create the Producer for the topic/queue  
                            IMessageProducer prod = session.CreateProducer(
                                new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("vdt_alarm"));
                            //Send Messages  
                            ITextMessage msg = prod.CreateTextMessage();
                            string jsonText = textBox1.Text.ToString();
                            //if(false == checkjson(jsonText))
                            //{
                            //    return;
                            //}
                            JObject jObj = JObject.Parse(jsonText);
                            jObj.Add(new JProperty("rstatus", recivestatus));
                            msg.Text = JsonConvert.SerializeObject(jObj);
                            prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);

                            //System.Threading.Thread.Sleep(5000);
                        }
                    }
                }
                else if (radioButton2.Checked)
                {
                    //已签收
                    string recivestatus = "2";

                    using (IConnection connection = factory.CreateConnection())
                    {
                        //Create the Session  
                        using (ISession session = connection.CreateSession())
                        {
                            //Create the Producer for the topic/queue  
                            IMessageProducer prod = session.CreateProducer(
                                new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("vdt_alarm"));
                            //Send Messages  
                            ITextMessage msg = prod.CreateTextMessage();
                            string jsonText = textBox1.Text.ToString();
                            //if (false == checkjson(jsonText))
                            //{
                            //    return;
                            //}
                            JObject jObj = JObject.Parse(jsonText);
                            jObj.Add(new JProperty("rstatus", recivestatus));
                            msg.Text = JsonConvert.SerializeObject(jObj);
                            prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);

                            //System.Threading.Thread.Sleep(5000);
                        }
                    }
                }
                else if (radioButton3.Checked)
                {
                    //未反馈
                    string recivestatus = "3";

                    using (IConnection connection = factory.CreateConnection())
                    {
                        //Create the Session  
                        using (ISession session = connection.CreateSession())
                        {
                            //Create the Producer for the topic/queue  
                            IMessageProducer prod = session.CreateProducer(
                                new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("vdt_alarm"));
                            //Send Messages  
                            ITextMessage msg = prod.CreateTextMessage();
                            string jsonText = textBox1.Text.ToString();
                            //if (false == checkjson(jsonText))
                            //{
                            //    return;
                            //}
                            JObject jObj = JObject.Parse(jsonText);
                            jObj.Add(new JProperty("rstatus", recivestatus));
                            msg.Text = JsonConvert.SerializeObject(jObj);
                            prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);

                            //System.Threading.Thread.Sleep(5000);
                        }
                    }
                }
                else if (radioButton4.Checked)
                {
                    //已反馈
                    string recivestatus = "4";

                    using (IConnection connection = factory.CreateConnection())
                    {
                        //Create the Session  
                        using (ISession session = connection.CreateSession())
                        {
                            //Create the Producer for the topic/queue  
                            IMessageProducer prod = session.CreateProducer(
                                new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("vdt_alarm"));
                            //Send Messages  
                            ITextMessage msg = prod.CreateTextMessage();
                            string jsonText = textBox1.Text.ToString();
                            //if (false == checkjson(jsonText))
                            //{
                            //    return;
                            //}
                            JObject jObj = JObject.Parse(jsonText);
                            jObj.Add(new JProperty("rstatus", recivestatus));
                            msg.Text = JsonConvert.SerializeObject(jObj);
                            prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);

                            //System.Threading.Thread.Sleep(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = string.Format("{0}", ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//生产者
            Task.Run(delegate {
                clicksenddata();
            });
            label1.Text = "发送线程启动";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(checkBox1.CheckState == CheckState.Checked)
            {
                label1.Text = "循环发送线程启动";
                Task.Run(delegate {
                    clicksenddata();
                });
                button1.Enabled = false; 
            }
            else
            {
                label1.Text = "循环发送线程停止";
                button1.Enabled = true;
            }
        }
    }
}
