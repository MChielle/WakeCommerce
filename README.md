# WakeCommerce

DATABASE:
 add-migration:
	On package manager console:
		add-migration <MIGRATION_NAME> -o Database/Migrations
	On PowerShell
		dotnet ef migrations add CreateDatabase --project .\Catalog.Infrastructure\ --startup-project .\Catalog.Web.Api\ -o Database/Migrations
 