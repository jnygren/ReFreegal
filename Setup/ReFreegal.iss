; ReFreegal Setup.iss

; (See 'Help - Inno Setup Documentation' for Script format.)

;     Constants
; {src} - The directory in which the Setup files are located
; {app} - Application destination location. (e.g. 'C:\Program Files\progName')
; {pf} - Program Files directory.
; {group} - The path to the Start Menu folder

; Setup section
; SourceDir - Location of source (.exe, Readme) files. (relative to .iss location.)
; OutputBaseFilename - Name of setup (.exe) file.
[Setup]
AppName=ReFreegal
AppVersion=1.2
DefaultDirName={pf}\ReFreegal
DefaultGroupName=ReFreegal
SourceDir="..\ReFreegal\bin\Release"
; If you set 'SourceDir', you must force 'OutputDir' to be where you want it.
OutputDir="..\..\..\Setup\Output"
OutputBaseFilename="ReFreegal_Setup"

; [Types]  Type of installation. (e.g. "full", "custom", etc.)

; Files section (i.e. the .exe file)
[Files]
Source: "ReFreegal.exe"; DestDir: "{app}"
Source: "taglib-sharp.dll"; DestDir: "{app}"

; Icons section - Defines shortcuts to be created.
[Icons]
; (NO shortcuts are created by default. You need this.)
; 'Comment:' parameter is tooltip for icon.
; ({userdesktop} doesn't work!)
;Name: "{userdesktop}\ReFreegal"; Filename: "{app}\ReFreegal.exe"; Tasks: "desktopicon"; Comment: "Comment Comment Comment."
Name: "{commondesktop}\ReFreegal"; Filename: "{app}\ReFreegal.exe"; Tasks: "desktopicon"; Comment: "Rename Freegal downloaded files."
Name: "{group}\ReFreegal"; Filename: "{app}\ReFreegal.exe"; Comment: "Rename Freegal downloaded files."
Name: "{group}\Uninstall ReFreegal"; Filename: "{uninstallexe}"

; Tasks section - Defines user-customizable tasks
[Tasks]
Name: "desktopicon"; Description: "Create a desktop icon"

; Run section
[Run]
;Filename: "notepad.exe"; Parameters: "{app}\ReFreegal.exe.config"; Flags: shellexec postinstall; Description: "Edit config file."

; End of Script