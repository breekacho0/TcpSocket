using System;
using System.Net.Sockets;
using System.Threading;
public class AsynchIOServer
{
    static TcpListener tcpListener = new TcpListener(10);

    //static void Listeners()
    //{

    //    Socket socketForClient = tcpListener.AcceptSocket();
    //    if (socketForClient.Connected)
    //    {
    //        Console.WriteLine("Client now connected to server.");
    //        NetworkStream networkStream = new NetworkStream(socketForClient);
    //        System.IO.StreamWriter streamWriter =
    //        new System.IO.StreamWriter(networkStream);
    //        System.IO.StreamReader streamReader =
    //        new System.IO.StreamReader(networkStream);

    //        //here we send message to client
    //        Console.WriteLine("type your message to be recieved by client:");
    //        string theString = GetData();
    //        streamWriter.WriteLine(theString);
    //        //Console.WriteLine(theString);
    //        streamWriter.Flush();

    //        //here we recieve client's text if any.
    //        theString = streamReader.ReadLine();
    //        Console.WriteLine("Message recieved by client:" + theString);
    //        streamReader.Close();
    //        networkStream.Close();
    //        streamWriter.Close();
    //    }
    //    socketForClient.Close();
    //    Console.WriteLine("Press any key to exit from server program");
    //    Console.ReadKey();
    //}


    static void Listeners()
    {

        Socket socketForClient = tcpListener.AcceptSocket();
        if (socketForClient.Connected)
        {
            Console.WriteLine("Client:"+socketForClient.RemoteEndPoint+" now connected to server.");
            NetworkStream networkStream = new NetworkStream(socketForClient);
            System.IO.StreamWriter streamWriter =
            new System.IO.StreamWriter(networkStream);
            System.IO.StreamReader streamReader =
            new System.IO.StreamReader(networkStream);

            ////here we send message to client
            //Console.WriteLine("type your message to be recieved by client:");
            //string theString = Console.ReadLine();
            //streamWriter.WriteLine(theString);
            ////Console.WriteLine(theString);
            //streamWriter.Flush();

            //while (true)
            //{
            //here we recieve client's text if any.
            while (true)
            {
                string theString = streamReader.ReadLine();
                Console.WriteLine("Message recieved by client:{0}   " + theString, socketForClient.RemoteEndPoint);
                char[] delimiterChars = { ' ', ',', '.', ':', '\t','(',')',';' };
                string[] commands = theString.Split(delimiterChars);
                if (commands[0] == "vector")
                {
                    int x=0, y=0;
                    if (Int32.TryParse(commands[1], out x) && Int32.TryParse(commands[2], out y))
                    {
                        double length = Math.Round(Math.Sqrt(x * x + y * y),2);
                        theString = "Vector length:" + length.ToString();
                    }
                    else theString = "Paramatres are not integer";
                }
                if (commands[0] == "commands")
                {
                    theString = "exit/vector(a;b)/time";
                   
                }
                if (commands[0] == "time")
                {
                    DateTime date = DateTime.Now;
                    theString = date.ToString();
                }
                if (commands[0] == "exit")
                    break;

                streamWriter.WriteLine(theString);
                streamWriter.Flush();
            }
            streamReader.Close();
            networkStream.Close();
            streamWriter.Close();
        }
        socketForClient.Close();
    }
   
    public static void Main()
    {
        //TcpListener tcpListener = new TcpListener(10);
        tcpListener.Start();
        Console.WriteLine("************This is Server program************");
        Console.WriteLine("Server supports only 10 connections");
        int numberOfClientsYouNeedToConnect = 10;
        for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
        {
            Thread newThread = new Thread(new ThreadStart(Listeners));
            newThread.Start();
        }

       
       
        //Socket socketForClient = tcpListener.AcceptSocket();
        //if (socketForClient.Connected)
        //{
        //    Console.WriteLine("Client now connected to server.");
        //    NetworkStream networkStream = new NetworkStream(socketForClient);
        //    System.IO.StreamWriter streamWriter =
        //    new System.IO.StreamWriter(networkStream);
        //    System.IO.StreamReader streamReader =
        //    new System.IO.StreamReader(networkStream);

        //    ////here we send message to client
        //    //Console.WriteLine("type your message to be recieved by client:");
        //    //string theString = Console.ReadLine();
        //    //streamWriter.WriteLine(theString);
        //    ////Console.WriteLine(theString);
        //    //streamWriter.Flush();

        //    //while (true)
        //    //{
        //        //here we recieve client's text if any.
        //    while (true)
        //    {
        //        string theString = streamReader.ReadLine();
        //        Console.WriteLine("Message recieved by client:" + theString);
        //        if (theString == "exit")
        //            break;
        //    }
        //        streamReader.Close();
        //        networkStream.Close();
        //        streamWriter.Close();
        //    //}
           
        //}
        //socketForClient.Close();
        //Console.WriteLine("Press any key to exit from server program");
        //Console.ReadKey();
    }
}
