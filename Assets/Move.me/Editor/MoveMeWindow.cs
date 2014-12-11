using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using PSMoveSharp;

public class MoveMeWindow : EditorWindow 
{
	public static MoveMeWindow window;
    public enum Modes{Connection,Controllers,Camera,About}
    Modes ToolbarMode = Modes.Connection;

    bool SquarePressed;
    bool CrossPressed;
    bool CirclePressed;
    bool TianglePressed;
    bool MoveButtonPressed;
    bool StartPressed;
    bool SelectPressed;
    float TriggerPressed;

    Vector3 CurrentMovePosition = Vector3.zero;
    Vector3 CurrentMoveVelocity = Vector3.zero;
    Vector3 CurrentMoveAcceleration = Vector3.zero;
    Vector3 CurrentMoveRotation = Vector3.zero;
    Vector3 CurrentMoveAngVelocity = Vector3.zero;
    Vector3 CurrentMoveAngAcceleration = Vector3.zero;
    Vector3 CurrentMoveHandlePosition = Vector3.zero;
    Vector3 CurrentMoveHandleVelocity = Vector3.zero;
    Vector3 CurrentMoveHandleAcceleration = Vector3.zero;

    public enum ClientCalibrationStep
    {
        Left = 0,
        Right = 1,
        Bottom = 2,
        Top = 3,
        Done = 4
    }

	[MenuItem( "Window/PlayStation Move.me" )]
	public static void Init()
	{
		window = (MoveMeWindow)EditorWindow.GetWindow( typeof( MoveMeWindow ), false, "Move.me" );
		window.minSize = new Vector2( 560, 600 );
		window.maxSize = new Vector2( 560, 1000 );

	}

	void OnEnable()
	{

	}

	void OnDisable()
	{

	}

    void OnGUI()
    {
        DisplayToolbar();
        switch(ToolbarMode)
        {
            case Modes.Connection:
                ConnectionGUI();
            break;

            case Modes.Controllers:
                ControllersGUI();
            break;

            case Modes.Camera:
                CameraGUI();
            break;

            case Modes.About:
                AboutGUI();
            break;
        }
    }    

	void ConnectionGUI()
	{
        GUILayout.Space (20);
        string buttontext = PSMove.connected ? "Disconnect" : "Connect";

        if(PSMove.connected)
            GUI.enabled = false;
		PSMove.server = EditorGUILayout.TextField("Server Address: ",PSMove.server);
        PSMove.port = EditorGUILayout.IntField("Server Port: ",PSMove.port);

        GUI.enabled = true;
        GUILayout.Space (15);
        if(GUILayout.Button (buttontext, GUILayout.Height(60)))
        {
            if(!PSMove.connected)
                PSMove.Connect();
            else
                PSMove.Disconnect();
        }
	}


    void ControllersGUI()
    {
        if(PSMove.state == null|| PSMove.connected == false)
            return;


        GUIStyle toolbar = new GUIStyle( EditorStyles.toolbarButton );
        toolbar.fixedWidth = 140;
        GUILayout.Space (5);
        EditorGUILayout.BeginHorizontal( EditorStyles.toolbar );
        {
            GUI.enabled = (PSMove.state.gemStatus[0].connected == 1);
            if( GUILayout.Toggle( PSMove.selectedMoveController == 0, "Controller 1", toolbar ) )
                PSMove.selectedMoveController = 0;

            GUI.enabled = (PSMove.state.gemStatus[1].connected == 1);
            if( GUILayout.Toggle( PSMove.selectedMoveController == 1, "Controller 2", toolbar ) )
                PSMove.selectedMoveController = 1;

            GUI.enabled = (PSMove.state.gemStatus[2].connected == 1);
            if( GUILayout.Toggle( PSMove.selectedMoveController == 2, "Controller 3", toolbar ) )
                PSMove.selectedMoveController = 2;
        
            GUI.enabled = (PSMove.state.gemStatus[3].connected == 1);       
            if( GUILayout.Toggle( PSMove.selectedMoveController == 3, "Controller 4", toolbar ) )
                PSMove.selectedMoveController = 3;;

        }
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();




        GUILayout.Space (20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space (70);
        EditorGUILayout.Toggle ("Square Pressed",SquarePressed);
        GUILayout.Space (100);
        EditorGUILayout.Toggle ("Tiangle Pressed",TianglePressed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space (20);
        EditorGUILayout.BeginVertical();
        GUILayout.Space (10);
        EditorGUILayout.Toggle ("Select Pressed",SelectPressed);
        EditorGUILayout.EndVertical();
        GUILayout.Space (30);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Toggle ("Move Pressed",MoveButtonPressed);
        EditorGUILayout.FloatField("Trigger Pressed",TriggerPressed, GUILayout.Width(132)); 
        EditorGUILayout.EndVertical();
        GUILayout.Space (20);
        EditorGUILayout.BeginVertical();
        GUILayout.Space (10);
        EditorGUILayout.Toggle ("Start Pressed",StartPressed);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space (70);
        EditorGUILayout.Toggle ("Cross Pressed",CrossPressed); 
        GUILayout.Space (100);
        EditorGUILayout.Toggle ("Circle Pressed",CirclePressed); 
        EditorGUILayout.EndHorizontal();


        GUILayout.Space (10);
        EditorGUILayout.Vector3Field("Position:",CurrentMovePosition);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Velocity:",CurrentMoveVelocity);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Acceleration:",CurrentMoveAcceleration);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Rotation:",CurrentMoveRotation);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Angular Velocity:",CurrentMoveAngVelocity);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Angular Acceleration:",CurrentMoveAngAcceleration);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Handle Position:",CurrentMoveHandlePosition);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Handle Velocity:",CurrentMoveHandleVelocity);
        GUILayout.Space (5);
        EditorGUILayout.Vector3Field("Handle Acceleration:",CurrentMoveHandleAcceleration);
    }

    void CameraGUI()
    {

    }

    void AboutGUI()
    {
        GUIStyle CenterLable = new GUIStyle(EditorStyles.largeLabel);
        GUIStyle littletext = new GUIStyle(EditorStyles.miniLabel) ;
        CenterLable.alignment = TextAnchor.MiddleCenter;
        GUILayout.Space (20);
        GUILayout.Label( "PlayStation Move.me Unity plugin",CenterLable);
        GUILayout.Label( "Version: alpha-0.1",CenterLable);
        GUILayout.Label( "By: Jacob Pennock",CenterLable);

        GUILayout.Space (20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space (20);
        GUILayout.Label( "Follow me for more Unity tips and tricks",littletext);
        GUILayout.Space (15);
        if(GUILayout.Button( "twitter"))
            Application.OpenURL("http://twitter.com/@JacobPennock");
        GUILayout.Space (20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space (10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space (20);
        GUILayout.Label( "Visit my site for more plugins and tutorials",littletext);
        if(GUILayout.Button( "JacobPennock.com"))
            Application.OpenURL("http://www.jacobpennock.com/Blog/?cat=19");
        GUILayout.Space (20);
        EditorGUILayout.EndHorizontal();
    }

    void DisplayToolbar()
    {
        GUIStyle toolbar = new GUIStyle( EditorStyles.toolbarButton );
        toolbar.fixedWidth = 140;
        GUILayout.Space (5);
        EditorGUILayout.BeginHorizontal( EditorStyles.toolbar );
        {
            if( GUILayout.Toggle( ToolbarMode == Modes.Connection, "Connection Settings", toolbar ) )
            {
                ToolbarMode = Modes.Connection;
            }
            if( GUILayout.Toggle( ToolbarMode == Modes.Controllers, "Controllers", toolbar ) )
            {
                ToolbarMode = Modes.Controllers;
            }
            if( GUILayout.Toggle( ToolbarMode == Modes.Camera, "Camera", toolbar ) )
            {
                ToolbarMode = Modes.Camera;
            }
            if( GUILayout.Toggle( ToolbarMode == Modes.About, "About", toolbar ) )
            {
                ToolbarMode = Modes.About;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void Update()
    {
        if(PSMove.connected)
        {
            PSMove.Update();
            lookForButtonPresses();
            FindPosition();
            this.Repaint();
        }
    }

    void lookForButtonPresses()
    {
        PSMoveSharpGemState selected_gem = PSMove.state.gemStates[PSMove.selectedMoveController];  
        SquarePressed = ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlSquare) != 0);
        CrossPressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlCross) != 0);
        CirclePressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlCircle) != 0);
        TianglePressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlTriangle) != 0);
        MoveButtonPressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlTick) != 0);
        StartPressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlStart) != 0);
        SelectPressed =  ((selected_gem.pad.digitalbuttons & PSMoveSharpConstants.ctrlSelect) != 0);
        TriggerPressed =  selected_gem.pad.analog_trigger;
    }

    void FindPosition()
    {
        PSMoveSharpGemState selected_gem = PSMove.state.gemStates[PSMove.selectedMoveController];  

        CurrentMovePosition.x = selected_gem.pos.x;
        CurrentMovePosition.y = selected_gem.pos.y;
        CurrentMovePosition.z = selected_gem.pos.z;

        CurrentMoveVelocity.x = selected_gem.vel.x;
        CurrentMoveVelocity.y = selected_gem.vel.y;
        CurrentMoveVelocity.z = selected_gem.vel.z;

        CurrentMoveAcceleration.x = selected_gem.accel.x;
        CurrentMoveAcceleration.y = selected_gem.accel.y;
        CurrentMoveAcceleration.z = selected_gem.accel.z;

        CurrentMoveRotation.x = selected_gem.quat.x;
        CurrentMoveRotation.y = selected_gem.quat.y;
        CurrentMoveRotation.z = selected_gem.quat.z;

        CurrentMoveAngVelocity.x = selected_gem.angvel.x;
        CurrentMoveAngVelocity.y = selected_gem.angvel.y;
        CurrentMoveAngVelocity.z = selected_gem.angvel.z;

        CurrentMoveAngAcceleration.x = selected_gem.angaccel.x;
        CurrentMoveAngAcceleration.y = selected_gem.angaccel.y;
        CurrentMoveAngAcceleration.z = selected_gem.angaccel.z;

        CurrentMoveHandlePosition.x = selected_gem.handle_pos.x;
        CurrentMoveHandlePosition.y = selected_gem.handle_pos.y;
        CurrentMoveHandlePosition.z = selected_gem.handle_pos.z;

        CurrentMoveHandleVelocity.x = selected_gem.handle_vel.x;
        CurrentMoveHandleVelocity.y = selected_gem.handle_vel.y;
        CurrentMoveHandleVelocity.z = selected_gem.handle_vel.z;

        CurrentMoveHandleAcceleration.x = selected_gem.handle_accel.x;
        CurrentMoveHandleAcceleration.y = selected_gem.handle_accel.y;
        CurrentMoveHandleAcceleration.z = selected_gem.handle_accel.z;
    }

}


public static class PSMove
{
    public static PSMoveClientThreadedRead connection;

    public static string server;
    public static int port;

    public static bool connected;
    public static bool paused;
    public static int selectedMoveController;
    public static int selectedNavController;
    public static uint updateDelay;
    public static bool imagePaused;
    public static uint imageUpdateDelay;
    public static bool slicedImagePaused;
    public static uint slicedImageUpdateDelay;

    public static bool[] resetEnabled;

    public static PSMoveSharpState state;

    static UInt32 processedPacketIndex;

    static PSMove()
    {
        server = "0.0.0.0";
        port = 7899;
        connected = false;
        paused = false;
        selectedMoveController = 0;
        selectedNavController = 0;
        updateDelay = 16;
        resetEnabled = new bool[PSMoveSharpState.PSMoveSharpNumMoveControllers];
        imagePaused = true;
        imageUpdateDelay = 32;
        slicedImagePaused = true;
        slicedImageUpdateDelay = 32;

        for (int i = 0; i < PSMoveSharpState.PSMoveSharpNumMoveControllers; i++)
        {
            resetEnabled[i] = true;
        }   

        processedPacketIndex = 0;	
    }

    public static void Connect()
    {

        connection = new PSMoveClientThreadedRead();

        try
        {
            connection.Connect(server, port);
            connection.StartThread();
        }

        catch
        {
            return;
        }
        connected = true;
    }

    public static void Disconnect()
    {
        try
        {
            paused = false;

            connection.StopThread();
            connection.Close();
        }
        catch
        {
            return;
        }

        connected = false;
    }

    public static void Update()
    {
        if (connection == null)
            return;

        state = connection.GetLatestState();

        if (processedPacketIndex == state.packet_index)
            return;

        processedPacketIndex = state.packet_index;
    }
}
