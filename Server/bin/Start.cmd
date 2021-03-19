@echo off
:start
TITLE MMo-Network Server
echo %DATE% %TIME% MMo-Network Server is running! >> updater_is_running.tmp
echo Starting MMo-Network Server.
echo.
java -server -Dfile.encoding=UTF-8 -Xms32m -Xmx32m -cp libs/*; ru.mmo.server.ServerMMo
if ERRORLEVEL 2 goto restart
if ERRORLEVEL 1 goto error
goto end
:restart
echo.
echo %DATE% %TIME% MMo-Network Server is restarted >> updater_is_running.tmp
echo.
goto start
:error
echo.
echo %DATE% %TIME% MMo-Network Server terminated abnormaly>> updater_is_running.tmp
echo.
:end
echo.
echo %DATE% %TIME% MMo-Network Server terminated >> updater_is_running.tmp
echo.
pause
