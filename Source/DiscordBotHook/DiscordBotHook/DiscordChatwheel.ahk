#SingleInstance force

url := "<<put your webhook url here>>"
token := "<<put your link token here>>"

!1::RunWait DiscordBotHook.exe %url% %token% ";;pause"
^1::RunWait DiscordBotHook.exe %url% %token% ";;unpause"
#1::RunWait DiscordBotHook.exe %url% %token% ";;skip"