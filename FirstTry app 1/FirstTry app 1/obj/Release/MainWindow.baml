   M S B A M L   `   `   `  ���� G  CWebDriverIDE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FirstTry_app_1.PropertiesFirstTry_app_1.Properties  /clr-namespace:FirstTry_app_1FirstTry_app_1      FirstTry_app_1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     >
properties'clr-namespace:FirstTry_app_1.Properties     .localclr-namespace:FirstTry_app_1     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -   $��
mainWindow��$	��400��$	��443��  � 
ResizeMode$  	CanResize=� � WindowStyle$ ThreeDBorderWindow=� � Title$ 
MainWindow��$	��709��$
��1202�� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency .  '  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 0q�   CornerRadius$ 0��   CaptionHeight$ 0Q�	   UseAeroCaptionButtons	 .  
   ResizeBorderThickness$
 7q� � 	Resources  �� MergedDictionaries �� Source   myHeaderStyle �� 
TargetType �� Property �� Value  ScrollBarTrackThumb �� 
TargetType �Name ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName � RowDefinitions ]�Name ]� Thumb n�Name ]� IncreaseRepeatButton ��Name J� Opacity  ]� DecreaseRepeatButton! P� 
SourceName  ButtonSelectBrush  	DarkBrush" �� Angle��  �� $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?��� $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?��� $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?��� $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�%�  &	        &	 *     '�� �    �� " ��.���  � y�$ 	Collapsed=��� " n�.���  � �� �� " n�.R�� $
 Grid��.+��� $��Stretch=�$��Stretch=�$
��Auto��$
��Auto��
������ �� $ CornerScrollBarRectangle��$ 5��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��$��0,1,0,1q�#��z�� P�  �@�Tag$ 
Horizontal��. ��� $ CornerScrollBarRectangle�� � ��$
 Auto���� $ CornerScrollBarRectangle�� � ��$ 6���� " ��.���  �@��IsFlicksEnabled .  ��  � �� �#ADABAB��  � ��
 ���� ��  � ��$ 7����  � �� �� " ��.R�� $ GridRoot��$��7��#��z�� �� $��0.00001* �.+�]� $ 
PART_Track��$��0����. |�.   n� $ Thumb��#��z��#���   �� $ PageUp��$��ScrollBar.PageDownCommand��$ 0Q�|�.    �� $ PageDown��$��ScrollBar.PageUpCommand��$ 0Q�|�.   P� $! Thumb�� �@J�IsMouseOver . . ��� # �  $ Thumb�� � ��P� $! Thumb�� �@n�
IsDragging . . ��� # �  $ Thumb�� � ��P�  � {� .  . ��� $ Thumb�� � y�$ 	Collapsed=�P�  �@��Orientation$ 
Horizontal=�. ��� $ GridRoot�� �@�LayoutTransform �� $	" -90Q��� $ 
PART_Track�� �@�LayoutTransform �� $	" -90Q���  � ��$
 Auto����  � ��$ 8���� $ Thumb�� �@�Tag$ 
Horizontal���� $ PageDown�� � ��$ ScrollBar.PageLeftCommand���� $ PageUp�� � ��$  ScrollBar.PageRightCommand��.���� ���	#FF000B48._�� -   $ MainGrid��$	��1.2q�# �� BitmapScalingMode$# HighQuality=����	#ffe5e5e5.+�� ���	#FFE5E5E5$��Stretch=�$��0q�.+��� 
�������$��0 0 0 0q�$��0��$��
0,0,-0.4,0q�$��Stretch=�$	��Top=�$��30��._�� .+�� $��30�����	#FFEEEEEE$��Center=�$��Stretch=�$��0q�$ � ColumnDefinitions$ �� $
��922* ��� $��40 ��� $��40 ��� $��40 �.+��� 
���   �$��0 0 0 0q�$��0��$��0,0,0,0q�$��2��._��� $��Roboto Medium)�$��13(�$��Selenium WebDriver IDE��$��4,3,-4.4,-2.6q����	#FF282828�� 
���   �$��0 0 0 0q�$��1��$��0,0,183.6,0q�._��� -   % ��Name$% Minimize��$��10,2,-34,4.4q�& � Cursor$
& Hand����Z� ��Z� ���� " ��.���  � �� �� ' �� ImageSource$)' #Images/minimize-1779402-1512859.png����  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( �� Triggers( P�  �@J�IsMouseOver . . ���  � �� �� ' �� ) �� 	UriSource$*) $Images/minimize-1779402-15128592.png?��� 
���   �$��0 0 0 0q�$��2��$��0,0,183.6,0q�._��� -   $% Maximize��$��11,5,-31,5.4q�$
& Hand����Z� ��Z� ���� " ��.���  � �� �� $' Images/Resize-512 2.png����  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images/Resize-512.png?��� 
���   �$��0 0 0 0q�$��3��$��0,0,183.6,0q�._��� -   $% Close��$��10,5,-31,4.4q�$
& Hand����Z� ��Z� ���� " ��.���  � �� �� $' Images/icon-close-512.png����  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $$) Images/icon-close-512-Copy.png?��� -   $��0,0,113.6,-2.6q�$��Stretch=�
���   �$
 0.01Q��� $	��Top=�
�������$��0 0 0 0q�$��0,30,0,0q�$��Stretch=�$# HighQuality=�$��2��._�� $	��Top=�$��40�����	#FFEEEEEE$��Stretch=�$��0q�$ �� $��* �.+��� $	��Top=�
���   �$��0 0 0 0q�$��0��$��0 2 0 1q�._�� .+��� ���	#FFE7E7E7$��0,0,0,0q�* J� Effect* 3  -System.Windows.Media.Effects.DropShadowEffect  +   	Direction$+ 90Q�,   ShadowDepth$, 2Q�-   Color$- 	#FF828282��.   Opacity$	. 0.3Q�/   RenderingBias$/ Quality=�$# HighQuality=�0   
BlurRadius$0 3Q�� 
�������$��Stretch=�$��0q�$ �� $��73 ��� $��* ��� $��34 ��� $��34 ��� $��34 ��� $��34 ��� $��34 ��� $��34 �.+��� $��0��$��0,0,986.2,0q�$	��Top=�$��Stretch=�._��� $��Roboto Medium)�$��18(�$��Project:��$��	4,5,-80,0q����	#FF323232$	 0.8Q���Z� �� $��1��$��0,0,986.2,0q�$
��Left=�$��Stretch=�._��� -   1 ��Name$1 ProjectNameText��$��4��$��Roboto Medium)�$��16(�$��4,7,-204,3.8q����	#FF3232322 �� TextWrapping$2 NoWrap=�)  #MaterialDesignThemes.Wpf.HintAssist3   Hint$3 *��4 �� 
CaretBrush4 �	#FF73CFFF��Z� ��Z� �� $��3��$��0.2,0,0,-0.2q�$
��Left=�$��Stretch=�$��33��._��� -   $% Update��$��5,9,4.6,4.8q�$
& Hand��$	 0.8Q���Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\Update.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\Update2.png?�5 � ToolTip5 _� .��Create project�� $��4��$��0,0,986.2,0q�$
��Left=�$��Stretch=�._��� -	   $% Create��$��8,10,-32,7.8q�$��20��$
& Hand��$	 0.8Q�$��19����Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\create-icon-15.jpg?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $ ) Images\create-icon-152.png?�5 _� .��Create project�� $��5��$��0.2,0,0,-0.2q�$
��Left=�$��Stretch=�$��33��._�'  !MaterialDesignThemes.Wpf.PopupBox  -
   $��OpenFile��$
& Hand��$��0q�$��7,9,0,0q�$��4��6   PlacementMode$6 BottomAndAlignRightEdges=�7   	StaysOpen7 .  $��22��$	��Top=�$
��Left=�$��23�����	#FF1C1C1C8   ToggleContent8 '  !MaterialDesignThemes.Wpf.PackIcon  -   $��OpenFileIcon��9   Kind$9 
FolderOpen=����	#FF1C1C1C$��20��$��20��$	 0.8Q�5 _� .��	Open file:   PopupContent.: �� $	��250��$��1q�.+��� -   $��OpenTestCaseClick��$��Case form WebDriver IDE��$
& Hand��$5 Ctrl + O���� -   $��OpenTestSuitClick��$��Suit form WebDriver IDE��$
& Hand��$5 Ctrl + Alt + O���� -   $��OpenOldTestCaseClick��$"��Case form Selenium IDE (Old)��$
& Hand��$5 Ctrl + Alt + O���� -   $��OpenOldTestSuitClick��$"��Suit form Selenium IDE (Old)��$
& Hand��$5 Ctrl + Alt + O���� -   $��OpenNewTestSuitClick��$"��Suit form Selenium IDE (New)��$
& Hand��$5 Ctrl + Alt + O���� $��6��$��0,0,-0.4,-0.2q�$
��Left=�$��Stretch=�$��34��._�  -   $��
SaveAsFile��$
& Hand��$��0q�$��6,9,0,0q�$��5��$6 BottomAndAlignRightEdges=�7 .  $��24��$	��Top=�$
��Left=�$��22�����	#FF1C1C1C5 _� .��	Save As8   -   $��SaveAsFileIcon��$9 ContentSaveEditOutline=����	#FF1C1C1C$��20��$��20��$	 0.8Q�.: �� $	��150��$��5q�.+��� -   $��SaveAsTestCaseClick��$��	Test Case��$
& Hand��$5 Ctrl + Shift + S���� -   $��SaveAsTestSuitClick��$��	Test Suit��$
& Hand���� $��7��$��0q�$
��Left=�$��Stretch=�$��34��._�  -   $��SaveFile��$
& Hand��$��0q�$��5,10,0,0q�$��5��$6 BottomAndAlignRightEdges=�7 .  $��23��$	��Top=�$
��Left=�$��20�����	#FF1C1C1C5 _� .��Save8   -   $��SaveFileIcon��$9 ContentSaveOutline=����	#FF1C1C1C$��20��$��20��$	 0.8Q�.: �� $	��150��$��5q�.+��� -   $��SaveTestCaseClick��$��	Test Case��$
& Hand��$5 Ctrl + S���� -   $��SaveTestSuitClick��$��	Test Suit��$
& Hand���� -   $��280,0,207,1q�$��Stretch=�
���  ��$
 0.01Q��� -   $��0,0,1123.6,0q�$��Stretch=�
���  ��$
 0.01Q�� -   $
��PBar�����	#FF18DA21$ 0Q�$y�Hidden=�$
��auto��$
��auto��;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssist;   DisableTransitions; . ��Z� ��Z� � $��0,70,-0.4,-0.2q� �� $
��460* ��� $��5 ��� $
��160* �$��50��$	��270��.+��� $��0��
�������$��0 0 0 0q�$��Stretch=�$��2��._�� ���	#FFE5E5E5$��Stretch=�$ �� $
��198* �$	��100��$	��350���� $��5 ��� $��1097* �.+��� $��0��$��Stretch=�$��Stretch=�
���   �
�������$��0 0 0 0q�._��  �� $��35 ��� $��35 ��� $��* �.+��� $��0��._�� .+��� ���	#FFE5E5E5$��0,0,-1.4,0.2q��� $��Tests��$��Roboto Medium)�$��16(�$��5,4,28.2,7.2q����	#FF323232��Z� ��Z� �� -   $% AddTest��$��0,7,7.2,7.2q�$
��Auto��$
& Hand��$��Right=�$��21������ " ��.���  � �� �� ' �� $) Images\Add.jpg?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\Add2.png?�5 _� .��Add test case (Ctrl + N)�� $��1��$ 5��$��1q����	#FFE5E5E5$��5q�._�� .+��� -   $��SearchTB��$��2 2 22 0q�$	��Top=�$��0q�
������ $��Roboto Medium)�$��11(����	#FF464646$2 NoWrap=�$3  Search tests ...��4 �	#FF73CFFF$��21����Z� �� $��	0,0,0.6,0q�$��Right=�$��21��._��� $��21��$��
0,2,-0.2,0q�$��Images\Search.png��$
��Fill=�$	��Top=�$��Right=�$��21���� $��2��._��� -   < ��Name$< TestCaseListView��$��Stretch=�$��14(�$��Roboto)�$��0,0,-0.4,5.2q�= J� 	AllowDrop= . > %� IsVirtualizing> .  ��.  ? %� 
ScrollUnit$? Pixel=�@ �� SelectionMode$@ Single=����	#FF1C1C1CA � ContextMenuA �� -   $��TestCaseContextMenu��.@�m� -    $��
CopyTestCM��$
��Copy��m� -!   $��	CutTestCM��$	��Cut��m� -"   $��PasteTestCM��$��Paste��m� -#   $��
EditTestCM��$
��Edit��m� -$   $��RenameTestCM��$��Rename��m� -%   $��RemoveTestCM��$��Delete��B �� ViewB �� !C �� ColumnHeaderContainerStyleC �� " ��.���  � y�$ 	Collapsed=�.P��� -&   testNameRow��D �� DisplayMemberBindingD �� *
TestName+�� ���	#FFE5E5E5E �� ResizeDirection$E Columns=�$��Stretch=�$��Stretch=�$��0.4,0,0.4,5q�$ 1Q�$��1���� $��2��$��Stretch=�$��Stretch=�
�������$��0 0 0 0q�._�� $��Stretch=� �� $��31 ��� �� $	��139 �.+��� $	��Top=�$��	0,0,0.2,0q�$��0 0 0 1q����	#FFE5E5E5$��0��$��30��._�� $��0 0 0 0q�$��Stretch=�$ �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��* �.+��� $��0��$
��Left=�._��� -'   $% 
RunAllTest��$��8,3,-32,3.6q�$��24��$��23��$
& Hand��$	 0.8Q���Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\RunAllTest.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\RunAllTest2.png?�5 _� .��	Run all�� $��1��$
��Left=�._��� -(   $% 
RunCurrent��$��8,5,-32,4.6q�$��20��$
& Hand��$	 0.8Q���Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\RunCurrent.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\RunCurrent2.png?�5 _� .��Run current�� $��2��$
��Left=�._��� -)   $% Pause��$��8,3,-32,2.6q�$��26��$
& Hand��$	 0.8Q���Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\Pause3.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\Pause4.png?�5 _� .��Pause�� $��3��$
��Left=�._��� -*   $% Speed��$��8,5,-32,4.6q�$��20��$
& Hand��$	 0.8Q���Z� ��Z� ���� " ��.���  � �� �� ' �� $) Images\Speed.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\Speed2.png?�5 _� .��Speed�� $��4��._�� $��Right=�$��0,0,0.2,-0.4q�$	��847��$ �� $	��25* ��� $
��822* �.+��� -+   F ��Name$F TestCaseCounterLB��$��Right=�$��Test Case :��$��10(�$��Roboto)�$��	0,5,122,5q����	#FF3F3F3F$��1���� -,   G ��Name$G TestCaseCounterTB��$��Right=�$��0��$��10(�$��Roboto)�$��	0,9,95,10q�$��28�����	#FF3F3F3F$��1���� --   $F CommandCounterLB��$��Right=�$��
Commands :��$��10(�$��Roboto)�$��0,5,31,5q����	#FF3F3F3F$��1���� -.   $G CommandCounterTB��$��Right=�$��0��$��10(�$��Roboto)�$��0,9,5,10q�$��28�����	#FF3F3F3F$��1���� $��Stretch=�$��0,31,0.2,0.4q�$��2��._�� $��0,0,0.2,-0.2q�$ �� $��* �.+��� $��Stretch=�$��Stretch=�$��0,0,0.2,0.4q�._��� -/   $< listView��$@ Single=�$��Stretch=�$��12(�$��Roboto)�= . $��0,0,0.2,0.2q�$H �� IsSynchronizedWithCurrentItem$
H TrueY�$
��Auto=�$? Pixel=���.  ���	#FF1C1C1CI �� SelectedIndexI �� *SelectedItemIndex+J �� Mode$J TwoWay=�A �� -0   $��listViewContextMenu��.@�m� -1   $��
CopyItemCM��$
��Copy��m� -2   $��	CutItemCM��$	��Cut��m� -3   $��PasteItemCM��$��Paste��m� -4   $��
EditItemCM��$
��Edit��m� -5   $��RemoveItemCM��$��Delete��B �� -6   CommandsListView��K �� AllowsColumnReorderK .  .P��� $�� ��-7   row��L �� CellTemplateL �� .R��� $
��Wrap=����� *Number+�� $��Command��-8   command��L �� .R��� $
��Wrap=����� *	Command+�� $��Taregt��-9   
target��L �� .R��� $
��Wrap=����� *Target+�� $��Value��-:   	value��L �� .R��� $
��Wrap=����� *Value+�� $��Description��-;   description��L �� .R��� $��RightToLeft=�$
��Wrap=����� *Description+� -<   M �Name$M loadTestProgress�� % !MaterialDesignCircularProgressBar#��[ N � Value$N 50Q�O � IsIndeterminateO . $y�Hidden=��� $��Bottom=�$��2�����	#FFE5E5E5$��0 1 0 0q�$	��139��$��0,0,0,0q�._�� $��Bottom=�.+��� $��Bottom=�$	��139��$��0,0,0,0q�._�� $��0 5 0 0q�$ �� $
��200* ��� $
��200* �.+�� $��Stretch=�$��Stretch=�$��0��$��
0,2,0,-0.2q� �� $��40 ��� $��40 ��� $��45 �.+��� $��0��$��Stretch=�$��	0,0,0,0.8q�._�� $��0,0,5,0q�$ �� $	��101 ��� $
��349* ��� $��40 �.+��� $��0��$��Stretch=�$��	0,0,0.8,0q�._��� -=   $F 	CommandCB��$��Command��$��11,6,18.4,7q�$��12(�$��Roboto)����	#FF3F3F3F�� ->   $��CommandsComboBoxBorder��$��Stretch=�$��1�����	#FFE5E5E5$��1q�$ 5��$��5.2,3,0.6,3q�._��� -?   $��CommandsComboBox��$��0��$��14(�$��Roboto)�P �� 
IsEditableP . $��6.2,-9.8,6.6,2.8q�$3 Search�� & "MaterialDesignFloatingHintComboBox#��[ $��Stretch=�$
& Hand��Q   FloatingScale$Q 20Q����� R �� EndPoint$R 0.5,1&�S �� 
StartPoint$S 0.5,0&�.��� T � Color$T 	#FFE5E5E5��U � Offset$U 0.887Q�� $T White��$U 0.969Q�.@��� .��alert�� .��break�� .��	clearText�� .��click�� .��close�� .��end�� .��
failTest�� .��if�� .��open�� .��pause�� .��	refresh�� .��	replace�� .��	runScript�� .��
scrollInto�� .��select�� .��selectByIndex�� .��selectByValue�� .��selectByVisibleText�� .��
sendKeys�� .��storeElementPresent�� .��	storeEval�� .��	storeHref�� .��	storeId�� .��storeInnerHTML�� .��	storeName�� .��	storeText�� .��
storeValue�� .��storeWicketPath�� .��switch�� .��switchToDefault�� .��type�� .��waitForAttribute�� .��waitForElementPresent�� .��waitForElementNotPresent�� .��waitForElementVisible�� .��waitForNotText�� .��waitForNumberOfWindowPresent�� .��waitForText�� .��waitForValue�� .��waitForWindowPresent�� .��while�� $��2��$��Stretch=�$��	3.4,3,0,3q����	#FFE5E5E5$��1q�$ 5��$
��Left=�$��34��._��� -@   $��AddCommandButton��$��25��$��4.2,3.2,2.8,3.8q�$��2��V J� RenderTransformOrigin$V 0.5,0.5&�$	 0.8Q�$
& Hand������ " ��.���  � �� �� ' �� $) Images\AddCommand3.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\AddCommand4.png?�5 _� .��Add/Edit command�� $��1��$��Stretch=�._�� $��
0,0,5,-0.2q�$ �� $	��101 ��� $
��312* ��� $��39 ��� $��38 �.+��� $��0��$��Stretch=�$��0,0,0.8,-0.2q�._��� $��Target��$��11,6,18.4,4q�$��12(�$��Roboto)����	#FF3F3F3F�� -A   $��TargetTBBorder��$��Stretch=�$��1�����	#FFE5E5E5$��1q�$ 5��$��5.2,3,2.2,4q�$��3��._��� -B   $1 TargetTB��$��LeftToRight=�$��1��$��5.2,3.2,4.4,1.8q�$��1��$2 NoWrap=�$��14(�$��Roboto)���Z� ���� *CommandsListView+W �� UpdateSourceTrigger$W PropertyChanged=��� $��2��$��Stretch=�._�� $��
0,0,5,-0.2q�$ �� $	��101 ��� $
��389* �.+��� $��0��$��Stretch=�$��0,0,2.8,-0.2q�._��� $��Value��$��11,6,18.4,7q�$��12(�$��Roboto)����	#FF3F3F3F�� -C   $��ValueTBBorder��$��Stretch=�$��1�����	#FFE5E5E5$��1q�$ 5��$��5.2,3,1.6,10q�._��� -D   $1 ValueTB��$��2��$��5.2,3.2,4.6,1.8q�$��1��$2 NoWrap=�$��14(�$��Roboto)���Z� ���� *Value+$J TwoWay=�$W PropertyChanged=�.��� $��Stretch=�$��Stretch=�$��1��$��
0,2,0,-0.2q� �� $	��100 �.+��� $��0��$��Stretch=�$��0,0,0,5q�._�� $��0,0,14,0q�$ �� $	��101 ��� $
��389* �.+��� $��0��$��Stretch=�$��0,0,1,0q�._��� $��Description��$��11,6,18.4,7q����	#FF3F3F3F�� -E   $��DescriptionTBBorder��$��Stretch=�$��1�����	#FFE5E5E5$��1q�$ 5��$��5.2,3,1.6,10q�._��� -F   $1 DescriptionTB��$��RightToLeft=�$��3��$��5.2,3.2,4.6,1.8q�$��1��$
2 Wrap=�$��	B Nazanin)�$��14(���Z� ���� *Description+$J TwoWay=�$W PropertyChanged=��� ���	#FF767676$
E Rows=�$��4��$
��Auto��$��Stretch=�$��Stretch=�$��	0,0.6,0,0q�$��1��$ 1Q��� $��2��$��0q�$��0q�$��Stretch=�$V 0.5,1&�._�� $��0,0,0,6q�.+��� $��0 0 0 2q�
����������	#FFE5E5E5*  $MaterialDesignThemes.Wpf.ThemeAssistX   Theme$X Inherit=�$��Stretch=�$��Stretch=�$��
0,0,0,20.4q����	#FF4E4E4E.@��� $
��Left=�$	��Log��
�������$��70��$��0q�$��0,0,0,-1q�$
& Hand����Z� .���� -G   $	��Log��
���   �
�������$��0q�$��2��$& Arrow��A �� .@�m� -H   $��CopyLogItem��$
��Copy������ " ��  MaterialDesignListBoxItemY �� BasedOn#Y [ .���  � ��
 �������  � ��
 �   ���  � ��$ 28����  � ��$ roboto)���  � ��$ 11(���  � ��$ LeftToRight=���  � ��$ 0q��� $
��Left=�$��Stored��
�������$��70��$��0q�$��0,0,0,-1q�$
& Hand����Z� .���� -I   $��Stored��
���   �
�������$��0q�$��2��$& Arrow������ " ��#Y [ .���  � ��
 �������  � ��
 �   ���  � ��$ 28����  � ��$ roboto)���  � ��$ 11(���  � ��$ LeftToRight=���  � ��$ 0q��� $
��Left=�$��
Script Log��
�������$��70��$��0q�$��0,0,0,-1q�$
& Hand����Z� .���� -J   $��	ScriptLog��
���   �
�������$��0q�$��2��$& Arrow������ " ��#Y [ .���  � ��
 �������  � ��
 �   ���  � ��$ 28����  � ��$ roboto)���  � ��$ 11(���  � ��$ LeftToRight=���  � ��$ 0q��� $��Refrence��
�������$��70��$��0q�$��
0 0 0 -1.2q�$
& Hand����Z� .���� -K   $��Refrence��$��
0,0,-0.4,0q�$��2��$& Arrow��$
��Auto������ " ��  MaterialDesignTextBlock#Y [ .���  � ��
 �������  � ��
 �   ���  � ��$ 28����  � ��$ roboto)���  � ��$ 11(���  � ��$ LeftToRight=���  � ��$ 0q��� -L   $% ClearLog��$��	0,3,5.6,0q�$��17��$	 0.8Q�$	��Top=�$��Right=�$��19��$
& Hand������ " ��.���  � �� �� ' �� $) Images\Clear.png?���  � �� �� " ��.R��� #��z��$��1q���Z� ._��� $��Center=�$��Center=�( P�  �@J�IsMouseOver . . ���  � �� �� ' �� $) Images\Clear2.png?��� $# HighQuality=�$s��m©iais.ir .Intellectual property rights of this software belongs to University of Tehran Research Institute .��$��
3,0,6,-0.4q�$��roboto)�$��13(�$
��Bold"�$��Bottom=��� -M   $ Splash��$��4��$ .2Q�$y�	Collapsed=�
���   �