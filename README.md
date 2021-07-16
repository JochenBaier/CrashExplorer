# CrashExplorer

CrashExplorer is a tool to help analyze crashes of programs written with Visual Studio in C/C++.

After a crash matching symbol files (*.pdb) often do not exists or are not usable if the program is using a copy protection mechanism (e.g. hardware dongle).

In this case it is possible to use a combination of  [Map files (*.map)](https://docs.microsoft.com/en-us/cpp/build/reference/map-generate-mapfile?view=msvc-160) and a [listing files (*.cod)](https://docs.microsoft.com/en-us/cpp/build/reference/fa-fa-listing-file?view=msvc-160) files.
The map file lists all functions of the program with addresses. The listing files maps source code to assembler code per translation unit.
The manual way is described in these CodeProject articles '[How to Debug the Exception](https://www.codeproject.com/Articles/43064/How-to-Debug-the-Exception)' and '[Using Windows Event Viewer to debug crashes](https://www.codeproject.com/Articles/597856/Using-Windows-Event-Viewer-to-debug-crashes)'

###### Example Analyze output
<img  style="border:1px solid black;float: left;" src="https://github.com/JochenBaier/CrashExplorer/blob/master/readme_images/animation.gif">



#### Analyze a crash using Windows Event Viewer Log entry

- **Faulting module map file:** Select the generated map file of the faulting module from the Event Viewer Log entry.
- **Crash Address:** Copy 'Fault offset' from Event Viewer Log entry

<img  style="float: left;" src="https://github.com/JochenBaier/CrashExplorer/blob/master/readme_images/event_viewer.png">



#### Analyze a crash using a crash dump loaded in Visual Studio

- **Call Stack address:** Copy address from the 'Disassembly' window.
- **Module base address:** Copy module base address form the 'Modules' window from the faulting module (exe or dll)

<img width="800" style="float: left;width: 400;" src="https://github.com/JochenBaier/CrashExplorer/blob/master/readme_images/visual_studio_dmp_loaded.png">




#### Download

[Current Version 0.1.0.0](https://github.com/JochenBaier/CrashExplorer/releases/download/v0.1.0.0/CrashExplorer-0100.zip)

#### Installation

Unzip  CrashExplorer-0100.zip to any location. Start CrashExplorer.exe.

Necessary settings in Visual Studio Project:

Enable map file:  'Configuration Properties->Linker->Debugging->Generate Map File' ('/MAP') for the faulting module (exe or dll).
CMake: `target_link_options(project PRIVATE "/MAP")`

Enable Assembler output: 'Configuration Properties->C/C++->Output Files->Assembler Output':  'Assembly, Machine Code and Source (/FAcs)'
CMake: `add_compile_options(/FAcs)` in topmost CMakeLists.txt

#### Hints

- To get correct calls stacks in Visual Studio from loaded dump file [install WinDbg as an alternate debugging engine for Visual Studio](https://docs.microsoft.com/en-us/windows-hardware/drivers/download-the-wdk): Reference: [StackOverflow](https://stackoverflow.com/questions/1552788/why-dont-minidumps-give-good-call-stacks/33394299#33394299)
- In addition to Visual Studio crash dumps can be analyzed with [Debug Diagnostic Tool](http://www.microsoft.com/en-us/download/details.aspx?id=58210) from Microsoft.
- [Configure Windows to generate crash dumps](https://docs.microsoft.com/en-us/windows/win32/wer/collecting-user-mode-dumps)
- Tested with Visual Studio 10 and Visual Studio 2019

#### License

CrashExplorer is released under the [GNU General Public License 3](https://www.gnu.org/licenses/gpl-3.0.de.html)

The example_project uses code from www.geeksforgeeks.org. 

#### Changelog

- 0.1.0.0 (11 July 2021)
  -  Initial release

#### Contact/Copyright

Email: [Jochen Baier](mailto:email a.t. jochen-baier.de)

The algorithm used in CrashExplorer is based on the CodeProject articles '[How to Debug the Exception](https://www.codeproject.com/Articles/43064/How-to-Debug-the-Exception)' , '[Using Windows Event Viewer to debug crashes](https://www.codeproject.com/Articles/597856/Using-Windows-Event-Viewer-to-debug-crashes)' and the help of my workmate Martin Strobel.



