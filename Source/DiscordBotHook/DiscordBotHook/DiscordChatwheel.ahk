#SingleInstance force

url := "<<put your webhook url here>>"

!1::RunWait DiscordBotHook.exe %url% ";;pause"
^1::RunWait DiscordBotHook.exe %url% ";;unpause"
#1::RunWait DiscordBotHook.exe %url% ";;skip"