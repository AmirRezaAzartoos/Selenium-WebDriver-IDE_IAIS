   M S B A M L   `   `   `  ����G  CWebDriverIDE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FirstTry_app_1FirstTry_app_1  5   	   %    FirstTry_app_1.CreateNewProject   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:FirstTry_app_1     5   	     � 
ResizeMode$  NoResize=�5	   	    � Title$ CreateNewProject��5
   	   $	��264��6"   $	��564��6/    � WindowStyle$
 None=�6;    � WindowStartupLocation$ CenterScreen=�6N   
������  � AllowsTransparency . .��5      �� -   5        $��
MainBorder��5      
������� �� CornerRadius$ 5��67   ._�5   
   � $	��260��6   $	��560��6   
������� �� BitmapScalingMode$ HighQuality=�6;   .+�5      �� ���	#FF106EBE$ 5��6,   $��54��6=   $	��Top=�6I   $��
-2,-2,-2,0q�6a   $
��Left=�6u   $	��564��6�   �� 5      ���	#FF106EBE$��80��6)   $	��Top=�65   $��	-2,3,-2,0q�6M   $
��Left=�6`   $	��564��6{    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$	 0.6Q�6'   	   ShadowDepth$	 2Q�65   
   RenderingBias$
 Quality=�6E      
BlurRadius$ 4Q�6]      	Direction$	 -90Q�6l   5      �� 5      $��12(�6   $��Name your new project��6"   $
��Left=�6B   $��	26,30,0,0q�6]   $	��Top=�6p   $��Roboto Black)�6�   ���	#DDFFFFFF5      �� 5      $1��+Please provide a name for yout new project.��6   $
��Left=�6J   $��	26,94,0,0q�6e   $	��Top=�6x   $��roboto medium)�6�   ���	#DD3A3A3A��6�   Z� �� 5      $��11(�6   $a��[You can change the name of your project at any time by clicking it and entering a new name.��6"   $
��Left=�6�   $��
26,173,0,0q�6�   $	��Top=�6�   $��roboto medium)�6�   ���	#DD3A3A3A��6  Z� �� 5      ���	#DDB4B4B4$ 5��6-   $��1q�6>   $��26,127,33,95q�6R   �� 5      -   5        $��ProjectNameTB��5   #   $��0��6   ���	#FF797979$��35,117,0,101q�6O   $��roboto)�6e   $��12(�6y   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$   PROJECT NAME ��5         
Background
 �����   
Foreground �	#FF106EBE.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �	#FF00DFFF %  !MaterialDesignFloatingHintTextBox#��[  5      $
��Left=�6L   $	��480��6g   
�������.��6�   5      �� 5       -   5        $��CancelTestButton��5    l   $��2��6   $��roboto medium)�6"   $��14(�6=    �� IsCancel . $��CANCEL��6[   $��439,207,0,0q�6�   ���	#FFF0F0F0���	#FF656565$��88��6�   $	��Top=�6�   $
��Left=�6  ��6�   Z� 6  �� 5!      -   5        $��CreateProjectButton��5!   ~   $��1��6   $��roboto medium)�6"   $��14(�6=   $��CREATE��6m   $��336,207,0,0q�6�   ���	#FFF0F0F0���	#FF656565$��88��6�   $	��Top=�6�   $
��Left=�6  ��6�   Z� 6  �� 5"      -   5         ��Name$ Splash��5"      $��4��6%    J� Opacity$ .2Q�66   $y�	Collapsed=�6C   
���   �6Z   5#      5$      5%      