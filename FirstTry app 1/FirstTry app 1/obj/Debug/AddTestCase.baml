   M S B A M L   `   `   `  ����G  CWebDriverIDE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FirstTry_app_1FirstTry_app_1  5   	        FirstTry_app_1.AddTestCase   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:FirstTry_app_1     5   	     � 
ResizeMode$  NoResize=�5	   	    � Title$ AddTestCase��5
   	   $	��264��6   $	��564��6*    � WindowStyle$
 None=�66    � WindowStartupLocation$ CenterScreen=�6I   
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
BlurRadius$ 4Q�6]      	Direction$	 -90Q�6l   5      �� 5      $��12(�6   $��Name your new project��6"   $��20,26,32.6,0q�6B   $��Roboto Black)�6X   ���	#DDFFFFFF$��24��6�   $	��Top=�6�   5      �� 5      ���	#DDB4B4B4$ 5��6-   $��1q�6>   $��26,123,33,99q�6R   �� 5      -   5        $��
TestCaseTB��5   @   $��0��6   ���	#FF797979$��35,113,0,105q�6i   $��roboto)�6   $��12(�6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$   TEST CASE NAME ��5         
Background
 �����   
Foreground �	#FF106EBE.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �	#FF00DFFF %  !MaterialDesignFloatingHintTextBox#��[  5      $
��Left=�6L   $	��480��6g   
�������.��6�   5      �� 5      -   5        $��CancelTestButton��5   l   $��2��6   $��roboto medium)�6"   $��14(�6=    �� IsCancel . $��CANCEL��6[   $��433,188,0,0q�6�   ���	#FFF0F0F0���	#FF656565$��88��6�   $	��Top=�6�   $
��Left=�6  ��6�   Z� 6  �� 5      -   5        $��AddTestButton��5   u   $��1��6   $��roboto medium)�6"   $��14(�6=   $	��ADD��6g   $��330,188,0,0q�6�   ���	#FFF0F0F0���	#FF656565$��88��6�   $	��Top=�6�   $
��Left=�6  ��6�   Z� 6  �� 5      -   5         ��Name$ Splash��5      $��4��6%    J� Opacity$ .2Q�66   $y�	Collapsed=�6C   
���   �6Z   5       5!      5"      