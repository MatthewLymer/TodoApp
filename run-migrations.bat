@echo off

Tools\Migrations\MigratorConsole.exe ^
	/up ^
	"/runner=SqlServerMigrator, SqlServerMigrator.RunnerFactory" ^
	"/connectionstring=server=(localdb)\v11.0;database=todoapp;trusted_connection=true" ^
	/scripts=Migrations

IF NOT %ERRORLEVEL% == 0 PAUSE