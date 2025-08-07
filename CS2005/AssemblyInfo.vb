'******************************************************************
' Amendment   : LifeAsia - i.ulife and Universal fortune
' Date        : 12/20/2008
' Author      : Eric Shu (ES004)	
'******************************************************************
' Amendment   : Call center & fund switching information enhancement
' Date        : 1/8/2009
' Author      : Eric Shu (ES005)	
'******************************************************************
' Amendment   : Add underwriting worksheet
' Date        : 3/6/2009
' Author      : Eric Shu (ES006)	
'******************************************************************
' Amendment   : Life/Asia Phase 3b
' Date        : 1/26/2010
' Author      : Eric Shu (ES007)	
'******************************************************************
' Amendment   : Life/Asia Phase 3b part 2
' Date        : 9/24/2010
' Author      : Eric Shu (ES008)	
'******************************************************************
' Amendment   : Fix Surrender value for Medisaver
' Date        : 5/20/2011
' Author      : Eric Shu (ES009)	
'******************************************************************
' Amendment   : Add Optout NPS flag
' Date        : 6/14/2011
' Author      : Eric Shu (ES010)	
'******************************************************************
' Amendment   : 1) Add new combo box for searching, button for exporting pending cases
'               2) multiple user group in Login
' Date        : Jun 11, 2015
' Author      : Kay Tsang KT20150611
'******************************************************************
' Amendment   : 1) SMS mobile will become phonemobileareacode+phonemobile if customer use PRC number
'               2) Event Type in service log will only show the option with obsoleted flag='N' or null
' Assembly ver: 2.27.8
' Date        : Nov 25, 2018
' Author      : Cavour Poon
'******************************************************************
' Amendment   : 1) Add HK Non-Cust Log
'               2) Newly created BusinessLogic to centralize function commonly used
'               3) Add CRS Preference
' Assembly ver: 2.27.10
' Date        : Feb 05, 2019
' Author      : Cavour Poon
'******************************************************************
' Amendment   : 1) Show eService User name in customerbox
'               2) Add SMS template Management
'               3) Change Macau Service Log using ServiceLogBL instead of POSWS and fixed the problem of inputting "'"
' Assembly ver: 2.27.11
' Date        : Mar 15, 2019
' Author      : Cavour Poon
'******************************************************************
' Amendment   : 1) Open CRM by Chrome
'               2) Fix VP2540               
' Assembly ver: 2.27.13
' Date        : Jun 14, 2019
' Author      : Cavour Poon
'******************************************************************
' Amendment   : 1) VP 2584             
' Assembly ver: 2.27.14
' Date        : Sep 10, 2019
' Author      : Cavour Poon
'******************************************************************
' Amendment   : 1) GL29             
' Assembly ver: 2.27.15
' Date        : Apr 16, 2020
' Author      : Alex Lee
'******************************************************************
' Amendment   : 1) Claims Audit
' Assembly ver: 2.27.16
' Date        : Apr 27, 2020
' Author      : Alex Lee
'******************************************************************
' Amendment   : 1) eService Co-Browsing
' Assembly ver: 2.27.17
' Date        : Sep 18, 2020
' Author      : Alex TH Lee
'******************************************************************
' Amendment   : 1) ITSR1488, ITSR2281
' Assembly ver: 2.27.18
' Date        : Nov 20, 2020
' Author      : Keith Tong, Alex TH Lee
'******************************************************************
' Amendment   : 1) ITSR933
' Assembly ver: 2.27.19
' Date        : Jan 26, 2021
' Author      : Gary Lei
'******************************************************************

Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: CLSCompliant(True)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("E17211DB-F567-485D-8BC5-B169E932B8C0")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:

<Assembly: AssemblyVersion("2.27.27")>
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="CS2005.exe.log4net", Watch:=True)>