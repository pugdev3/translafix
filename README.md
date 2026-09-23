# Translafix

A small utility designed to fix GameMaker WADs built with WinPack (e.g [Translatale](https://github.com/AlexWaveDiver/TranslaTale))

A lot of old Undertale mods (mostly translations) used now ancient tools during their creation

Those mods, while they worked in GameMaker, are unable to be parsed by modern tools like [UTMT](https://github.com/UnderminersTeam/UndertaleModTool), making modding them very difficult

So, ever got this error while trying to open one of those data.win files? This tool attempts to address just that.

<img width="460" height="286" alt="UTMT fails to open data.win file" src="https://github.com/user-attachments/assets/d072918a-c3cd-40c1-bffd-1756ac2489f0" />

## Usage

To begin, go to [Releases](https://github.com/pugdev3/translafix/releases), there are two types of files:

`translafix` / `translafix.exe` - These are single-file versions that should bundle everything you need, no need to install anything else (you can just put them anywhere)

(Linux users: make sure to run `chmod +x translafix` or to allow the file to be run as an executable)

`Translafix-Bundle-Linux.tar.gz` / `Translafix-Bundle-Windows.tar.gz` - A lot more lightweight (less disk space) but you may need to install the .NET Runtime (if you already have it, try using this one)

Download the one that applies to you and then follow instructions on screen.

## Credits

[UndertaleModTool](https://github.com/UnderminersTeam/UndertaleModTool) - The amazing tool that made this possible, this tool is basically just a heavily stripped down version of UndertaleModCli

[Eliandro4](https://github.com/Eliandro4) - For making the [UTMT fork](https://github.com/Eliandro4/UndertaleModToolFC/tree/butterparse) used for loading and saving the WinPack WAD files (its the real sauce in here)

