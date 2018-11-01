function varargout = invKinematic(varargin)
% INVKINEMATIC MATLAB code for invKinematic.fig
%      INVKINEMATIC, by itself, creates a new INVKINEMATIC or raises the existing
%      singleton*.
%
%      H = INVKINEMATIC returns the handle to a new INVKINEMATIC or the handle to
%      the existing singleton*.
%
%      INVKINEMATIC('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in INVKINEMATIC.M with the given input arguments.
%
%      INVKINEMATIC('Property','Value',...) creates a new INVKINEMATIC or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before invKinematic_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to invKinematic_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help invKinematic

% Last Modified by GUIDE v2.5 01-Nov-2018 11:39:55

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @invKinematic_OpeningFcn, ...
                   'gui_OutputFcn',  @invKinematic_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before invKinematic is made visible.
function invKinematic_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to invKinematic (see VARARGIN)

% Choose default command line output for invKinematic
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes invKinematic wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = invKinematic_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on button press in btn_forward.
function btn_forward_Callback(hObject, eventdata, handles)
% hObject    handle to btn_forward (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
Th_1 = str2double(handles.Theta_1.String)*pi/180;
Th_2 = str2double(handles.Theta_2.String)*pi/180;
Th_3 = str2double(handles.Theta_3.String)*pi/180;
Th_4 = str2double(handles.Theta_4.String)*pi/180;
Th_5 = str2double(handles.Theta_5.String)*pi/180;

d_1 = 197;
d_3 = 233;
d_5 = 378;

% L (n) = Link([theta // d // a // alpha])
L(1) = Link([0 d_1 0 -pi/2]);
L(2) = Link([0 0 0 pi/2]);
L(3) = Link([0 d_3 0 -pi/2]);
L(4) = Link([0 0 0 pi/2]);
L(5) = Link([0 d_5 0 0]);


L(2).offset=-pi/2;
L(3).offset=pi/2;

L(1).qlim=[deg2rad(-90) deg2rad(15)];
L(2).qlim=[deg2rad(0) deg2rad(105)];
L(3).qlim=[deg2rad(-45) deg2rad(45)];
L(4).qlim=[deg2rad(0) deg2rad(130)];
L(5).qlim=[deg2rad(-45) deg2rad(45)];

Robot = SerialLink(L);
Robot.name = 'my robot';
Robot.plot([Th_1 Th_2 Th_3 Th_4 Th_5], 'tilesize', 120);

T = Robot.fkine([Th_1 Th_2 Th_3 Th_4 Th_5]);
T = T.double;
fprintf('**********\n');
fprintf('Fkine done\n');
fprintf('__________\n');

handles.Pos_X.String = num2str(floor(T(1,4)));
handles.Pos_Y.String = num2str(floor(T(2,4)));
handles.Pos_Z.String = num2str(floor(T(3,4)));

% --- Executes on button press in btn_inverse.
function btn_inverse_Callback(hObject, eventdata, handles)
% hObject    handle to btn_inverse (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
PX = str2double(handles.Pos_X.String);
PY = str2double(handles.Pos_Y.String);
PZ = str2double(handles.Pos_Z.String);


d_1 = 197;
d_3 = 233;
d_5 = 378;

% L (n) = Link([theta // d // a // alpha])
L (1) = Link([0 d_1 0 -pi/2]);
L (2) = Link([0 0 0 pi/2]);
L (3) = Link([0 d_3 0 -pi/2]);
L (4) = Link([0 0 0 pi/2]);
L (5) = Link([0 d_5 0 0]);

L(2).offset=-pi/2;
L(3).offset=pi/2;

L(1).qlim=[deg2rad(-90) deg2rad(15)];
L(2).qlim=[deg2rad(0) deg2rad(105)];
L(3).qlim=[deg2rad(-45) deg2rad(45)];
L(4).qlim=[deg2rad(0) deg2rad(130)];
L(5).qlim=[deg2rad(-45) deg2rad(45)];

Robot = SerialLink(L);
Robot.name = 'my robot';

% T0 define the start point, all theta 0
%q0 = [0, 0, 0, 0, 0];
%T_0 = Robot.fkine(q0);
%T_0 = T_0.double;

T_des = transl([PX PY PZ]); % and destination

% n = 50;
% T = ctraj(T_0, T_des, n); % compute a Cartesian path


% Inverse kinematics by optimization with joint limits
J = Robot.ikcon(T_des)*180/pi;
fprintf('**********\n');
fprintf('Ikine done\n');
fprintf('__________\n');

handles.Theta_1.String = num2str(J(1));
handles.Theta_2.String = num2str(J(2));
handles.Theta_3.String = num2str(J(3));
handles.Theta_4.String = num2str(J(4));
handles.Theta_5.String = num2str(J(5));

Robot.plot(J*pi/180, 'tilesize', 120);

 function slider1ContValCallback(hFigure,eventdata)
 % test it out - get the handles object and display the current value
 handles = guidata(hFigure);
% fprintf('slider value: %f\n',get(handles.Theta1_Slider,'Value'));
 sliderValue = get(handles.Theta1_Slider,'Value');
 set(handles.Theta_1,'String',num2str(floor(sliderValue)));
 
  function slider2ContValCallback(hFigure,eventdata)
 % test it out - get the handles object and display the current value
 handles = guidata(hFigure);
% fprintf('slider value: %f\n',get(handles.Theta2_Slider,'Value'));
 sliderValue = get(handles.Theta2_Slider,'Value');
 set(handles.Theta_2,'String',num2str(floor(sliderValue)));
 
  function slider3ContValCallback(hFigure,eventdata)
 % test it out - get the handles object and display the current value
 handles = guidata(hFigure);
% fprintf('slider value: %f\n',get(handles.Theta3_Slider,'Value'));
 sliderValue = get(handles.Theta3_Slider,'Value');
 set(handles.Theta_3,'String',num2str(floor(sliderValue)));

 function slider4ContValCallback(hFigure,eventdata)
 % test it out - get the handles object and display the current value
 handles = guidata(hFigure);
% fprintf('slider value: %f\n',get(handles.Theta4_Slider,'Value'));
 sliderValue = get(handles.Theta4_Slider,'Value');
 set(handles.Theta_4,'String',num2str(floor(sliderValue)));

 function slider5ContValCallback(hFigure,eventdata)
 % test it out - get the handles object and display the current value
 handles = guidata(hFigure);
% fprintf('slider value: %f\n',get(handles.Theta5_Slider,'Value'));
 sliderValue = get(handles.Theta5_Slider,'Value');
 set(handles.Theta_5,'String',num2str(floor(sliderValue)));

function Theta_1_Callback(hObject, eventdata, handles)
% hObject    handle to Theta_1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Theta_1 as text
%        str2double(get(hObject,'String')) returns contents of Theta_1 as a double

% --- Executes during object creation, after setting all properties.
function Theta_1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Theta_1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

function Theta_2_Callback(hObject, eventdata, handles)
% hObject    handle to Theta_2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Theta_2 as text
%        str2double(get(hObject,'String')) returns contents of Theta_2 as a double
handles.Theta_1.String = handles.Theta1_Slider.value;

% --- Executes during object creation, after setting all properties.
function Theta_2_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Theta_2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

function Theta_3_Callback(hObject, eventdata, handles)
% hObject    handle to Theta_3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Theta_3 as text
%        str2double(get(hObject,'String')) returns contents of Theta_3 as a double

% --- Executes during object creation, after setting all properties.
function Theta_3_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Theta_3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

function Theta_4_Callback(hObject, eventdata, handles)
% hObject    handle to Theta_4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Theta_4 as text
%        str2double(get(hObject,'String')) returns contents of Theta_4 as a double

% --- Executes during object creation, after setting all properties.
function Theta_4_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Theta_4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

function Theta_5_Callback(hObject, eventdata, handles)
% hObject    handle to Theta_5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Theta_5 as text
%        str2double(get(hObject,'String')) returns contents of Theta_5 as a double

% --- Executes during object creation, after setting all properties.
function Theta_5_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Theta_5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


function Pos_X_Callback(hObject, eventdata, handles)
% hObject    handle to Pos_X (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Pos_X as text
%        str2double(get(hObject,'String')) returns contents of Pos_X as a double


% --- Executes during object creation, after setting all properties.
function Pos_X_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Pos_X (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function Pos_Y_Callback(hObject, eventdata, handles)
% hObject    handle to Pos_Y (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Pos_Y as text
%        str2double(get(hObject,'String')) returns contents of Pos_Y as a double


% --- Executes during object creation, after setting all properties.
function Pos_Y_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Pos_Y (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function Pos_Z_Callback(hObject, eventdata, handles)
% hObject    handle to Pos_Z (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of Pos_Z as text
%        str2double(get(hObject,'String')) returns contents of Pos_Z as a double


% --- Executes during object creation, after setting all properties.
function Pos_Z_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Pos_Z (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes during object creation, after setting all properties.
function figure1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called


% --- Executes on button press in pushbutton3.
function pushbutton3_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
handles.Theta_1.String = 0;
handles.Theta_2.String = 0;
handles.Theta_3.String = 0;
handles.Theta_4.String = 0;
handles.Theta_5.String = 0;


% --- Executes on button press in pushbutton4.
function pushbutton4_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
