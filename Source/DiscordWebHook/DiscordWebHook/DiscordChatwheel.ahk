#SingleInstance force

url := "<<put your webhook url here>>"

!1::RunWait DiscordWebHook.exe %url% ";;pause"
^1::RunWait DiscordWebHook.exe %url% ";;unpause"
#1::RunWait DiscordWebHook.exe %url% ";;skip"