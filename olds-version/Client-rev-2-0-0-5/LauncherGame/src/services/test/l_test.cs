using System;
using System.Collections.Generic;
using System.Text;
//using Lidgren.Network;
using System.Windows.Forms;
using System.Threading;

namespace MMO_NETWORK_LAUNCHER.src.services.test
{
    //class l_test
    //{
    //    private static NetClient s_client;
    //    public void testConnectLigdren()
    //    {
    //        NetPeerConfiguration config = new NetPeerConfiguration("MMO-Network");
    //        config.AutoFlushSendQueue = false;
    //        s_client = new NetClient(config);
    //        NetOutgoingMessage hail = s_client.CreateMessage();
    //        hail.Write("This is the hail message");
    //        s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
    //        s_client.Connect("127.0.0.1", 11000, hail);


    //    }
    //    public void GotMessage(object peer)
    //    {
    //        NetIncomingMessage im;
    //        while ((im = s_client.ReadMessage()) != null)
    //        {
    //            // handle incoming message
    //            switch (im.MessageType)
    //            {
    //                case NetIncomingMessageType.DebugMessage:
    //                case NetIncomingMessageType.ErrorMessage:
    //                case NetIncomingMessageType.WarningMessage:
    //                case NetIncomingMessageType.VerboseDebugMessage:
    //                    string text = im.ReadString();
    //                    break;
    //            }
    //        }
    //    }

    //    // called by the UI
    //    public static void Shutdown()
    //    {
    //        s_client.Disconnect("Requested by user");
    //        s_client.Shutdown("Requested by user");
    //    }

    //    // called by the UI
    //    public static void Send(string text)
    //    {
    //        NetOutgoingMessage om = s_client.CreateMessage(text);
    //        s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
    //        //Output("Sending '" + text + "'");
    //        s_client.FlushSendQueue();
    //    }
    //}
}

