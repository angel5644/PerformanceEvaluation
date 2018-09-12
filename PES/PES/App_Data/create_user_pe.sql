-- USER SQL
CREATE USER PE IDENTIFIED BY colima4 
DEFAULT TABLESPACE "SYSTEM"
TEMPORARY TABLESPACE "TEMP";

-- QUOTAS

-- ROLES
GRANT "DATAPUMP_EXP_FULL_DATABASE" TO PE ;
GRANT "DBA" TO PE ;
GRANT "HS_ADMIN_EXECUTE_ROLE" TO PE ;
GRANT "ADM_PARALLEL_EXECUTE_TASK" TO PE ;
GRANT "CTXAPP" TO PE ;
GRANT "SELECT_CATALOG_ROLE" TO PE ;
GRANT "CONNECT" TO PE ;
GRANT "DATAPUMP_IMP_FULL_DATABASE" TO PE ;
GRANT "OEM_MONITOR" TO PE ;
GRANT "APEX_ADMINISTRATOR_ROLE" TO PE ;
GRANT "AQ_USER_ROLE" TO PE ;
GRANT "SCHEDULER_ADMIN" TO PE ;
GRANT "XDB_SET_INVOKER" TO PE ;
GRANT "RECOVERY_CATALOG_OWNER" TO PE ;
GRANT "DBFS_ROLE" TO PE ;
GRANT "XDB_WEBSERVICES_OVER_HTTP" TO PE ;
GRANT "HS_ADMIN_SELECT_ROLE" TO PE ;
GRANT "PLUSTRACE" TO PE ;
GRANT "RESOURCE" TO PE ;
GRANT "AQ_ADMINISTRATOR_ROLE" TO PE ;
GRANT "DELETE_CATALOG_ROLE" TO PE ;
GRANT "XDB_WEBSERVICES_WITH_PUBLIC" TO PE ;
GRANT "XDB_WEBSERVICES" TO PE ;
GRANT "EXECUTE_CATALOG_ROLE" TO PE ;
GRANT "EXP_FULL_DATABASE" TO PE ;
GRANT "GATHER_SYSTEM_STATISTICS" TO PE ;
GRANT "LOGSTDBY_ADMINISTRATOR" TO PE ;
GRANT "AUTHENTICATEDUSER" TO PE ;
GRANT "OEM_ADVISOR" TO PE ;
GRANT "HS_ADMIN_ROLE" TO PE ;
GRANT "XDBADMIN" TO PE ;
GRANT "IMP_FULL_DATABASE" TO PE ;
ALTER USER PE DEFAULT ROLE "DATAPUMP_EXP_FULL_DATABASE","DBA","HS_ADMIN_EXECUTE_ROLE","ADM_PARALLEL_EXECUTE_TASK","CTXAPP","SELECT_CATALOG_ROLE","CONNECT","DATAPUMP_IMP_FULL_DATABASE","OEM_MONITOR","APEX_ADMINISTRATOR_ROLE","AQ_USER_ROLE","SCHEDULER_ADMIN","XDB_SET_INVOKER","RECOVERY_CATALOG_OWNER","DBFS_ROLE","XDB_WEBSERVICES_OVER_HTTP","HS_ADMIN_SELECT_ROLE","PLUSTRACE","RESOURCE","AQ_ADMINISTRATOR_ROLE","DELETE_CATALOG_ROLE","XDB_WEBSERVICES_WITH_PUBLIC","XDB_WEBSERVICES","EXECUTE_CATALOG_ROLE","EXP_FULL_DATABASE","GATHER_SYSTEM_STATISTICS","LOGSTDBY_ADMINISTRATOR","AUTHENTICATEDUSER","OEM_ADVISOR","HS_ADMIN_ROLE","XDBADMIN","IMP_FULL_DATABASE";

-- SYSTEM PRIVILEGES
GRANT UNLIMITED TABLESPACE TO PE ;
