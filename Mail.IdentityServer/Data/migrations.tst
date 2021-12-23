Add-Migration Initial -Context DataContext -OutputDir Data/ApplicationDb
Update-Database -Context DataContext
Add-Migration PersistedGrantDbMigration -Context PersistedGrantDbContext -OutputDir Data/PersistedGrantDb
Update-Database -Context PersistedGrantDbContext
Add-Migration ConfigurationDbMigration -Context ConfigurationDbContext -OutputDir Data/ConfigurationDb
Update-Database -Context ConfigurationDbContext

Drop-Database -Context DataContext

Update-Database -Context DataContext
Update-Database -Context PersistedGrantDbContext
Update-Database -Context ConfigurationDbContext

//Test1
//Test1@gmail.com
//12qw!Q