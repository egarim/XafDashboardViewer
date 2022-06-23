;#DropDB XafDashboardViewerEasyTest

#Application XafDashboardViewerWin
#Application XafDashboardViewerWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef XafDashboardViewerWin

*Action About...
 
*Action OK

#EndIf

#IfDef XafDashboardViewerWeb
*Action Navigation(My Details)
#EndIf
